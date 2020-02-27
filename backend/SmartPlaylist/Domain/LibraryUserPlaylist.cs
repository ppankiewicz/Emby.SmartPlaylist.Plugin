using System.Linq;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Playlists;

namespace SmartPlaylist.Domain
{
    public class LibraryUserPlaylist : UserPlaylist
    {
        private readonly Playlist _playlist;

        public LibraryUserPlaylist(User user, Playlist playlist) : base(user, playlist.Name)
        {
            _playlist = playlist;
        }

        public long InternalId => _playlist.InternalId;

        public override BaseItem[] GetItems()
        {
            return _playlist.GetChildren(User).ToArray();
        }
    }
}