using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Library;
using MediaBrowser.Controller.Playlists;
using MediaBrowser.Model.Playlists;
using SmartPlaylist.Domain;

namespace SmartPlaylist.Services
{
    public class PlaylistRepository
    {
        private readonly ILibraryManager _libraryManager;
        private readonly IPlaylistManager _playlistManager;
        private readonly IUserManager _userManager;

        public PlaylistRepository(IUserManager userManager, ILibraryManager libraryManager,
            IPlaylistManager playlistManager)
        {
            _userManager = userManager;
            _libraryManager = libraryManager;
            _playlistManager = playlistManager;
        }

        public UserPlaylistsList GetUserPlaylists(Dictionary<Guid, string[]> playlistNames)
        {
            var users = playlistNames.Keys.ToDictionary(x => x, GetUser);
            var playlists = playlistNames.ToDictionary(x => users[x.Key],
                y => GetUserPlaylists(users[y.Key], y.Value).ToArray());

            return new UserPlaylistsList(playlists);
        }

        public async Task<UserPlaylist> GetOrCreateUserPlaylistAsync(Guid userId, string playlistName)
        {
            return (await GetOrCreateUserPlaylistsAsync(new Dictionary<Guid, string[]>
            {
                {userId, new[] {playlistName}}
            }).ConfigureAwait(false)).SingleByName(userId, playlistName);
        }


        public async Task<UserPlaylistsList> GetOrCreateUserPlaylistsAsync(
            Dictionary<Guid, string[]> playlistNames)
        {
            var playlists = GetUserPlaylists(playlistNames);

            var missingPlaylistNames = GetMissingPlaylistNames(playlistNames, playlists);

            var newPlaylists = await CreatePlaylistsAsync(missingPlaylistNames);
            newPlaylists
                .ToList()
                .ForEach(p => playlists.AddPlaylists(p));

            return playlists;
        }

        private static Dictionary<User, string[]> GetMissingPlaylistNames(Dictionary<Guid, string[]> playlistNames, UserPlaylistsList playlists)
        {
            var missingPlaylistNames = playlistNames.ToDictionary(x => playlists.GetUser(x.Key),
                y => GetMissingPlaylistNames(playlists.GetPlaylistsNames(y.Key), playlistNames[y.Key]));
            return missingPlaylistNames;
        }

        private async Task<IEnumerable<UserPlaylists>> CreatePlaylistsAsync(Dictionary<User, string[]> missingPlaylistNames)
        {
            var missingPlaylistsTasks =
                missingPlaylistNames.Select(x => CreatePlaylistsAsync(x.Key, x.Value)).ToArray();
            await Task.WhenAll(missingPlaylistsTasks).ConfigureAwait(false);

            return missingPlaylistsTasks.Select(result => result.Result);
        }

        private async Task<UserPlaylists> CreatePlaylistsAsync(User user, string[] playlistNames)
        {
            var createPlaylistTasks =
                playlistNames.Select(playlistName => CreatePlaylistAsync(playlistName, user)).ToArray();
            await Task.WhenAll(createPlaylistTasks).ConfigureAwait(false);

            var playlists = GetPlaylists(createPlaylistTasks.Select(x => long.Parse(x.Result.Id)).ToArray());

            return new UserPlaylists(user, playlists);
        }

        private Playlist[] GetPlaylists(long[] playlistIds)
        {
            return _libraryManager.GetItemsResult(new InternalItemsQuery
            {
                IncludeItemTypes = new[] {"Playlist"},
                ItemIds = playlistIds
            }).Items.OfType<Playlist>().ToArray();
        }


        private async Task<PlaylistCreationResult> CreatePlaylistAsync(string name, User user)
        {
            return await _playlistManager.CreatePlaylist(new PlaylistCreationRequest
            {
                Name = name,
                UserId = user.InternalId
            }).ConfigureAwait(false);
        }

        private User GetUser(Guid userId)
        {
            return _userManager.GetUserById(userId);
        }

        private Playlist[] GetUserPlaylists(User user, string[] namesFilter)
        {
            return _libraryManager.GetItemsResult(new InternalItemsQuery(user)
                {
                    IncludeItemTypes = new[] {typeof(Playlist).Name},
                    Recursive = true
                }).Items.OfType<Playlist>()
                .Where(x => namesFilter.Contains(x.Name))
                .ToArray();
        }

        private static string[] GetMissingPlaylistNames(string[] playlistNames, string[] allPlaylistNames)
        {
            return allPlaylistNames.Except(playlistNames).ToArray();
        }
    }
}