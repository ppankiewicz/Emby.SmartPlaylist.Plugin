using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaBrowser.Controller.Entities;
using SmartPlaylist.Handlers.Commands;
using SmartPlaylist.Infrastructure.MesssageBus;
using SmartPlaylist.Services;
using SmartPlaylist.Services.SmartPlaylist;

namespace SmartPlaylist.Handlers.CommandHandlers
{
    public class
        UpdateAllSmartPlaylistsCommandHandler : IMessageHandlerAsync<UpdateAllSmartPlaylistsCommand>
    {
        private readonly MessageBus _messageBus;
        private readonly PlaylistItemsUpdater _playlistItemsUpdater;
        private readonly PlaylistRepository _playlistRepository;
        private readonly SmartPlaylistProvider _smartPlaylistProvider;

        public UpdateAllSmartPlaylistsCommandHandler(MessageBus messageBus,
            SmartPlaylistProvider smartPlaylistProvider, PlaylistRepository playlistRepository,
            PlaylistItemsUpdater playlistItemsUpdater)
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
            for (var i = 0; i < (int) Math.Ceiling(smartPlaylists.Length / (decimal) Const.ForEachMaxDegreeOfParallelism); i++)
            {
                var tasks = smartPlaylists.Take(Const.ForEachMaxDegreeOfParallelism)
                    .Skip(i * Const.ForEachMaxDegreeOfParallelism).Select(x => GetTasks(x, items));
                await Task.WhenAll(tasks).ConfigureAwait(false);
            }
        }

        private async Task GetTasks(Domain.SmartPlaylist smartPlaylist, BaseItem[] items)
        {
            var playlist = _playlistRepository.GetUserPlaylist(smartPlaylist.UserId, smartPlaylist.Name);
            var newItems = smartPlaylist.FilterPlaylistItems(playlist, items).ToArray();
            await _playlistItemsUpdater.UpdateAsync(playlist, newItems).ConfigureAwait(false);
        }


        private void UpdateSmartPlaylistsWithAllUserItems(IEnumerable<Domain.SmartPlaylist> smartPlaylists)
        {
            smartPlaylists.ToList().ForEach(x => _messageBus.Publish(new UpdateSmartPlaylistCommand(x.Id)));
        }
    }
}