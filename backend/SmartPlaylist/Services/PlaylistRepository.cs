using System;
using System.Linq;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Library;
using MediaBrowser.Controller.Playlists;
using SmartPlaylist.Domain;

namespace SmartPlaylist.Services
{
    public class PlaylistRepository
    {
        private readonly ILibraryManager _libraryManager;
        private readonly IUserManager _userManager;

        public PlaylistRepository(IUserManager userManager, ILibraryManager libraryManager)
        {
            _userManager = userManager;
            _libraryManager = libraryManager;
        }

        public UserPlaylist GetUserPlaylist(Guid userId, string playlistName)
        {
            var user = GetUser(userId);
            var playlist = FindPlaylist(user, playlistName);
            return playlist != null ? new LibraryUserPlaylist(user, playlist) : new UserPlaylist(user, playlistName);
        }

        private User GetUser(Guid userId)
        {
            return _userManager.GetUserById(userId);
        }

        private Playlist FindPlaylist(User user, string playlistName)
        {
            return _libraryManager.GetItemsResult(new InternalItemsQuery(user)
            {
                IncludeItemTypes = new[] {typeof(Playlist).Name},
                Name = playlistName,
                Recursive = true
            }).Items.OfType<Playlist>().FirstOrDefault();
        }
    }
}