using System.Linq;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Playlists;
using SmartPlaylist.Domain;

namespace SmartPlaylist.Services
{
    public class PlaylistItemsUpdater
    {
        private readonly IPlaylistManager _playlistManager;

        public PlaylistItemsUpdater(IPlaylistManager playlistManager)
        {
            _playlistManager = playlistManager;
        }

        public void Update(UserPlaylist playlist, BaseItem[] newItems)
        {
            var playlistItems = playlist.GetItems();

            RemoveFromPlaylist(playlist, playlistItems);
            AddToPlaylist(playlist, newItems);
        }

        private void RemoveFromPlaylist(UserPlaylist playlist, BaseItem[] itemsToRemove)
        {
            _playlistManager.RemoveFromPlaylist(playlist.Playlist.InternalId,
                itemsToRemove.Select(x => x.ListItemEntryId).ToArray());
        }

        private void AddToPlaylist(UserPlaylist playlist, BaseItem[] itemsToAdd)
        {
            _playlistManager.AddToPlaylist(playlist.Playlist.InternalId,
                itemsToAdd.Select(x => x.InternalId).ToArray(), playlist.User);
        }
    }
}