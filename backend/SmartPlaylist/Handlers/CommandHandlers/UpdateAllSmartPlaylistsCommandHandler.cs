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
                await UpdateSmartPlaylistsWithNewItemsAsync(message.Items, smartPlaylistToUpdateWithNewItems);
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
            var playlists = await _playlistRepository
                .GetOrCreateUserPlaylistsAsync(GroupPlaylistNamesByUser(smartPlaylists)).ConfigureAwait(false);

            Parallel.ForEach(smartPlaylists, new ParallelOptions
            {
                MaxDegreeOfParallelism = Const.ForEachMaxDegreeOfParallelism
            }, smartPlaylist =>
            {
                var playlist = playlists.SingleByName(smartPlaylist.UserId, smartPlaylist.Name);

                var newItems = smartPlaylist.FilterPlaylistItems(playlist, items).ToArray();
                _playlistItemsUpdater.Update(playlist, newItems);
            });
        }

        private void UpdateSmartPlaylistsWithAllUserItems(IEnumerable<Domain.SmartPlaylist> smartPlaylists)
        {
            smartPlaylists.ToList().ForEach(x => _messageBus.Publish(new UpdateSmartPlaylistCommand(x.Id)));
        }

        private static Dictionary<Guid, string[]> GroupPlaylistNamesByUser(
            IEnumerable<Domain.SmartPlaylist> smartPlaylists)
        {
            return smartPlaylists.GroupBy(x => x.UserId, y => y.Name)
                .ToDictionary(x => x.Key, y => y.ToArray());
        }
    }
}