using System.Linq;
using System.Threading.Tasks;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Playlists;
using MediaBrowser.Model.Playlists;
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

        public async Task UpdateAsync(UserPlaylist playlist, BaseItem[] newItems)
        {
            var playlistItems = playlist.GetItems();
            if (playlist is LibraryUserPlaylist libraryUserPlaylist)
            {
                RemoveFromPlaylist(libraryUserPlaylist, playlistItems);
                AddToPlaylist(libraryUserPlaylist, newItems);
            }
            else if(newItems.Any())
            {
                await _playlistManager.CreatePlaylist(new PlaylistCreationRequest
                {
                    ItemIdList = newItems.Select(x => x.InternalId).ToArray(),
                    Name = playlist.Name,
                    UserId = playlist.User.InternalId
                }).ConfigureAwait(false);
            }
        }

        private void RemoveFromPlaylist(LibraryUserPlaylist playlist, BaseItem[] itemsToRemove)
        {
            _playlistManager.RemoveFromPlaylist(playlist.InternalId,
                itemsToRemove.Select(x => x.ListItemEntryId).ToArray());
        }

        private void AddToPlaylist(LibraryUserPlaylist playlist, BaseItem[] itemsToAdd)
        {
            _playlistManager.AddToPlaylist(playlist.InternalId,
                itemsToAdd.Select(x => x.InternalId).ToArray(), playlist.User);
        }
    }
}