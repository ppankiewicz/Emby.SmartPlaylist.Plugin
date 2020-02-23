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

        public UserPlaylistsList GetUserPlaylists(UserPlaylistInfo[] userPlaylists)
        {
            var users = userPlaylists.Select(x => x.UserId).Distinct().ToDictionary(x => x, GetUser);
            var playlists = userPlaylists.ToDictionary(x => users[x.UserId],
                y => GetUserPlaylists(userPlaylists.FirstOrDefault(x => x.UserId == y.UserId)));

            return new UserPlaylistsList(playlists);
        }

        public async Task<UserPlaylist> GetOrCreateUserPlaylistAsync(UserPlaylistInfo userPlaylistInfo)
        {
            return (await GetOrCreateUserPlaylistsAsync(new[] {userPlaylistInfo}).ConfigureAwait(false))
                .SingleByName(userPlaylistInfo.UserId, userPlaylistInfo.FirstPlaylistName);
        }


        public async Task<UserPlaylistsList> GetOrCreateUserPlaylistsAsync(
            UserPlaylistInfo[] userPlaylists)
        {
            var playlists = GetUserPlaylists(userPlaylists);

            var missingPlaylistNames = GetMissingPlaylistNames(userPlaylists, playlists);

            var newPlaylists = await CreatePlaylistsAsync(missingPlaylistNames);
            newPlaylists
                .ToList()
                .ForEach(p => playlists.AddPlaylists(p));

            return playlists;
        }

        private static UserPlaylistInfo[] GetMissingPlaylistNames(UserPlaylistInfo[] userPlaylists,
            UserPlaylistsList playlists)
        {
            return userPlaylists.Select(x => new UserPlaylistInfo(x.UserId,
                    x.FilterPlaylists(GetMissingPlaylistNames(playlists.GetPlaylistsNames(x.UserId), x.PlaylistNames))))
                .ToArray();
        }

        private async Task<IEnumerable<UserPlaylists>> CreatePlaylistsAsync(
            UserPlaylistInfo[] missingPlaylists)
        {
            var missingPlaylistsTasks =
                missingPlaylists.Select(CreatePlaylistsAsync).ToArray();
            await Task.WhenAll(missingPlaylistsTasks).ConfigureAwait(false);

            return missingPlaylistsTasks.Select(result => result.Result);
        }

        private async Task<UserPlaylists> CreatePlaylistsAsync(UserPlaylistInfo userPlaylist)
        {
            var user = GetUser(userPlaylist.UserId);
            var createPlaylistTasks =
                userPlaylist.PlaylistInfos.Select(p => CreatePlaylistAsync(p, user)).ToArray();
            await Task.WhenAll(createPlaylistTasks).ConfigureAwait(false);

            var playlists = GetPlaylists(createPlaylistTasks.Select(x => long.Parse(x.Result.Id)).ToArray());

            return new UserPlaylists(user, playlists);
        }

        private Playlist[] GetPlaylists(long[] playlistIds)
        {
            return _libraryManager.GetItemsResult(new InternalItemsQuery
            {
                IncludeItemTypes = new[] {nameof(Playlist)},
                ItemIds = playlistIds
            }).Items.OfType<Playlist>().ToArray();
        }


        private async Task<PlaylistCreationResult> CreatePlaylistAsync(PlaylistInfo playlistInfo, User user)
        {
            return await _playlistManager.CreatePlaylist(new PlaylistCreationRequest
            {
                Name = playlistInfo.PlaylistName,
                UserId = user.InternalId,
                MediaType = playlistInfo.MediaType
            }).ConfigureAwait(false);
        }

        private User GetUser(Guid userId)
        {
            return _userManager.GetUserById(userId);
        }

        private Playlist[] GetUserPlaylists(UserPlaylistInfo userPlaylist)
        {
            var user = GetUser(userPlaylist.UserId);
            return _libraryManager.GetItemsResult(new InternalItemsQuery(user)
                {
                    IncludeItemTypes = new[] {typeof(Playlist).Name},
                    Recursive = true
                }).Items.OfType<Playlist>()
                .Where(x => userPlaylist.PlaylistNames.Contains(x.Name))
                .ToArray();
        }

        private static string[] GetMissingPlaylistNames(string[] playlistNames, string[] allPlaylistNames)
        {
            return allPlaylistNames.Except(playlistNames).ToArray();
        }
    }
}