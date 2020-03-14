using System.Threading.Tasks;
using MediaBrowser.Controller.Entities;
using SmartPlaylist.Domain;
using SmartPlaylist.Infrastructure;
using SmartPlaylist.Services;

namespace SmartPlaylist.PerfLoggerDecorators.Services
{
    public class PlaylistItemsUpdaterPerfLoggerDecorator : IPlaylistItemsUpdater
    {
        private readonly IPlaylistItemsUpdater _decorated;

        public PlaylistItemsUpdaterPerfLoggerDecorator(IPlaylistItemsUpdater decorated)
        {
            _decorated = decorated;
        }

        public async Task UpdateAsync(UserPlaylist playlist, BaseItem[] newItems)
        {
            using (PerfLogger.Create("UpdatePlaylistItems",
                () => new {playlistName = playlist.Name, newItemsCount = newItems.Length}))

            {
                await _decorated.UpdateAsync(playlist, newItems).ConfigureAwait(false);
            }
        }
    }
}