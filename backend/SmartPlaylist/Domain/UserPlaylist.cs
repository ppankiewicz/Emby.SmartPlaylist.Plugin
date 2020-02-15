using System;
using System.Collections.Generic;
using System.Linq;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Playlists;

namespace SmartPlaylist.Domain
{
    public class UserPlaylist
    {
        public Playlist Playlist { get; }
        public User User { get;  }

        public UserPlaylist(User user, Playlist playlist)
        {
            User = user;
            Playlist = playlist;
        }

        public BaseItem[] GetItems()
        {
            return Playlist.GetChildren(User).ToArray();
        }
    }

    public class UserPlaylists
    {
        public User User { get; }
        public Playlist[] Playlists { get; }

        public UserPlaylists(User user, params Playlist[] playlists)
        {
            if (!playlists.Any())
            {
                throw new Exception($"{nameof(playlists)} cannot be empty");
            }
            User = user;
            Playlists = playlists;
        }
    }


    public class UserPlaylistsList
    {
        private readonly Dictionary<User, Playlist[]> _playlists;

        public UserPlaylistsList(Dictionary<User, Playlist[]> playlists)
        {
            _playlists = playlists;
        }

        public UserPlaylist SingleByName(Guid userId, string playlistName)
        {
            var user = _playlists.Keys.Single(x => x.Id == userId);
            var playlist = _playlists[user].FirstOrDefault(x => x.Name.Equals(playlistName));
            return new UserPlaylist(user, playlist);
        }

        public User GetUser(Guid userId)
        {
            return _playlists.Keys.Single(x => x.Id == userId);
        }

        public string[] GetPlaylistsNames(Guid userId)
        {
            return _playlists.FirstOrDefault(x => x.Key.Id == userId).Value?.Select(x => x.Name).ToArray();
        }

        public void AddPlaylists(UserPlaylists userPlaylists)
        {
            _playlists[userPlaylists.User] = _playlists[userPlaylists.User].Concat(userPlaylists.Playlists).ToArray();
        }
    }
}