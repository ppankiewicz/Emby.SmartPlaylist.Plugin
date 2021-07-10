﻿using System.Linq;
using System.Threading.Tasks;
using MediaBrowser.Controller.Entities;
using SmartPlaylist.Handlers.Commands;
using SmartPlaylist.Infrastructure;
using SmartPlaylist.Infrastructure.MesssageBus;
using SmartPlaylist.Services;
using SmartPlaylist.Services.SmartPlaylist;

namespace SmartPlaylist.Handlers.CommandHandlers
{
    public class UpdateSmartPlaylistCommandHandler : IMessageHandlerAsync<UpdateSmartPlaylistCommand>
    {
        private readonly IPlaylistItemsUpdater _playlistItemsUpdater;
        private readonly IPlaylistRepository _playlistRepository;
        private readonly ISmartPlaylistProvider _smartPlaylistProvider;
        private readonly ISmartPlaylistStore _smartPlaylistStore;

        private readonly IUserItemsProvider _userItemsProvider;

        public UpdateSmartPlaylistCommandHandler(
            IUserItemsProvider userItemsProvider, ISmartPlaylistProvider smartPlaylistProvider,
            IPlaylistRepository playlistRepository, IPlaylistItemsUpdater playlistItemsUpdater,
            ISmartPlaylistStore smartPlaylistStore)
        {
            _userItemsProvider = userItemsProvider;
            _smartPlaylistProvider = smartPlaylistProvider;
            _playlistRepository = playlistRepository;
            _playlistItemsUpdater = playlistItemsUpdater;
            _smartPlaylistStore = smartPlaylistStore;
        }

        public async Task HandleAsync(UpdateSmartPlaylistCommand message)
        {
            var smartPlaylist = await _smartPlaylistProvider.GetSmartPlaylistAsync(message.SmartPlaylistId)
                .ConfigureAwait(false);

            var playlist = _playlistRepository.GetUserPlaylist(smartPlaylist.UserId, smartPlaylist.Name);

            var items = _userItemsProvider.GetItems(playlist.User, Const.SupportedItemTypeNames).ToArray();

            BaseItem[] newItems;
            using (PerfLogger.Create("FilterPlaylistItems",
                () => new { playlistName = playlist.Name, itemsCount = items.Length }))
            {
                newItems = smartPlaylist.FilterPlaylistItems(playlist, items).ToArray();
            }

            await _playlistItemsUpdater.UpdateAsync(playlist, newItems).ConfigureAwait(false);
            var smDto = smartPlaylist.ToDto();

            if (!_smartPlaylistStore.Exists(smDto.UserId, smDto.Id) || smartPlaylist.IsShuffleUpdateType)
            {
                smartPlaylist.UpdateLastShuffleTime();
                _smartPlaylistStore.Save(smDto);
            }
        }
    }
}