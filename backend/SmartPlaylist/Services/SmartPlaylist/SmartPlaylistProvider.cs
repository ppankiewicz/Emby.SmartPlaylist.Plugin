using System;
using System.Linq;
using System.Threading.Tasks;
using SmartPlaylist.Adapters;

namespace SmartPlaylist.Services.SmartPlaylist
{
    public class SmartPlaylistProvider
    {
        private readonly ISmartPlaylistStore _smartPlaylistStore;

        public SmartPlaylistProvider(ISmartPlaylistStore smartPlaylistStore)
        {
            _smartPlaylistStore = smartPlaylistStore;
        }

        public async Task<Domain.SmartPlaylist[]> GetAllUpdateableSmartPlaylistsAsync()
        {
            var smartPlaylistDtos = await _smartPlaylistStore.GetAllSmartPlaylistsAsync().ConfigureAwait(false);
            return SmartPlaylistAdapter.Adapt(smartPlaylistDtos).Where(x => x.CanUpdatePlaylist).ToArray();
        }


        public async Task<Domain.SmartPlaylist> GetSmartPlaylistAsync(Guid smartPlaylistId)
        {
            var smartPlaylistDto =
                await _smartPlaylistStore.GetSmartPlaylistAsync(smartPlaylistId).ConfigureAwait(false);
            return SmartPlaylistAdapter.Adapt(smartPlaylistDto);
        }
    }
}