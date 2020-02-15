using System.Linq;
using System.Threading.Tasks;
using SmartPlaylist.Handlers.Commands;
using SmartPlaylist.Infrastructure.MesssageBus;
using SmartPlaylist.Services;
using SmartPlaylist.Services.SmartPlaylist;

namespace SmartPlaylist.Handlers.CommandHandlers
{
    public class UpdateSmartPlaylistCommandHandler : IMessageHandlerAsync<UpdateSmartPlaylistCommand>
    {
        private readonly PlaylistItemsUpdater _playlistItemsUpdater;
        private readonly PlaylistRepository _playlistRepository;
        private readonly SmartPlaylistProvider _smartPlaylistProvider;
        private readonly ISmartPlaylistStore _smartPlaylistStore;

        private readonly UserItemsProvider _userItemsProvider;

        public UpdateSmartPlaylistCommandHandler(
            UserItemsProvider userItemsProvider, SmartPlaylistProvider smartPlaylistProvider,
            PlaylistRepository playlistRepository, PlaylistItemsUpdater playlistItemsUpdater,
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
            var playlist = await _playlistRepository
                .GetOrCreateUserPlaylistAsync(smartPlaylist.UserId, smartPlaylist.Name)
                .ConfigureAwait(false);

            var items = _userItemsProvider.GetItems(playlist.User, Const.SupportedItemTypeNames);
            var newItems = smartPlaylist.FilterPlaylistItems(playlist, items.ToArray()).ToArray();

            _playlistItemsUpdater.Update(playlist, newItems);

            if (smartPlaylist.IsShuffleUpdateType)
            {
                smartPlaylist.UpdateLastShuffleTime();
                _smartPlaylistStore.Save(smartPlaylist.ToDto());
            }
        }
    }
}