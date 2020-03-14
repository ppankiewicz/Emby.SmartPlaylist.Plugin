using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaBrowser.Controller.Entities;
using SmartPlaylist.Handlers.Commands;
using SmartPlaylist.Infrastructure;
using SmartPlaylist.Infrastructure.MesssageBus;
using SmartPlaylist.Services;
using SmartPlaylist.Services.SmartPlaylist;

namespace SmartPlaylist.Handlers.CommandHandlers
{
    public class
        UpdateAllSmartPlaylistsCommandHandler : IMessageHandlerAsync<UpdateAllSmartPlaylistsCommand>
    {
        private readonly MessageBus _messageBus;
        private readonly IPlaylistItemsUpdater _playlistItemsUpdater;
        private readonly IPlaylistRepository _playlistRepository;
        private readonly ISmartPlaylistProvider _smartPlaylistProvider;

        public UpdateAllSmartPlaylistsCommandHandler(MessageBus messageBus,
            ISmartPlaylistProvider smartPlaylistProvider, IPlaylistRepository playlistRepository,
            IPlaylistItemsUpdater playlistItemsUpdater)
        {
            _messageBus = messageBus;
            _smartPlaylistProvider = smartPlaylistProvider;
            _playlistRepository = playlistRepository;
            _playlistItemsUpdater = playlistItemsUpdater;
        }


        public async Task HandleAsync(UpdateAllSmartPlaylistsCommand message)
        {
            var smartPlaylists =
                await _smartPlaylistProvider.GetAllUpdateableSmartPlaylistsAsync().ConfigureAwait(false);

            var smartPlaylistToUpdateWithNewItems = GetSmartPlaylistToUpdateWithNewItems(message, smartPlaylists);

            UpdateSmartPlaylistsWithAllUserItems(smartPlaylists.Except(smartPlaylistToUpdateWithNewItems));

            if (smartPlaylistToUpdateWithNewItems.Any())
                await UpdateSmartPlaylistsWithNewItemsAsync(message.Items, smartPlaylistToUpdateWithNewItems)
                    .ConfigureAwait(false);
        }

        private static Domain.SmartPlaylist[] GetSmartPlaylistToUpdateWithNewItems(
            UpdateAllSmartPlaylistsCommand message, Domain.SmartPlaylist[] smartPlaylists)
        {
            return message.HasItems
                ? smartPlaylists.Where(x => x.CanUpdatePlaylistWithNewItems).ToArray()
                : new Domain.SmartPlaylist[0];
        }

        private async Task UpdateSmartPlaylistsWithNewItemsAsync(BaseItem[] items,
            Domain.SmartPlaylist[] smartPlaylists)
        {
            foreach (var smartPlaylist in smartPlaylists) await GetTasks(smartPlaylist, items).ConfigureAwait(false);
        }

        private async Task GetTasks(Domain.SmartPlaylist smartPlaylist, BaseItem[] items)
        {
            BaseItem[] newItems;
            var playlist = _playlistRepository.GetUserPlaylist(smartPlaylist.UserId, smartPlaylist.Name);
            using (PerfLogger.Create("FilterPlaylistItems",
                () => new {playlistName = playlist.Name, itemsCount = items.Length}))
            {
                newItems = smartPlaylist.FilterPlaylistItems(playlist, items).ToArray();
            }

            await _playlistItemsUpdater.UpdateAsync(playlist, newItems).ConfigureAwait(false);
        }


        private void UpdateSmartPlaylistsWithAllUserItems(IEnumerable<Domain.SmartPlaylist> smartPlaylists)
        {
            smartPlaylists.ToList().ForEach(x => _messageBus.Publish(new UpdateSmartPlaylistCommand(x.Id)));
        }
    }
}