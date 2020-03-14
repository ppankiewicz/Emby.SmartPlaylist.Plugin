using System;
using System.Threading.Tasks;
using SmartPlaylist.Contracts;
using SmartPlaylist.Infrastructure;
using SmartPlaylist.Services.SmartPlaylist;

namespace SmartPlaylist.PerfLoggerDecorators.Services
{
    public class SmartPlaylistStorePerfLoggerDecorator : ISmartPlaylistStore
    {
        private readonly ISmartPlaylistStore _decorated;

        public SmartPlaylistStorePerfLoggerDecorator(ISmartPlaylistStore decorated)
        {
            _decorated = decorated;
        }

        public async Task<SmartPlaylistDto> GetSmartPlaylistAsync(Guid smartPlaylistId)
        {
            SmartPlaylistDto smartPlaylistDto = null;
            using (PerfLogger.Create("GetSmartPlaylistFromStore", () => new {smartPlaylistName = smartPlaylistDto?.Name}))
            {
                smartPlaylistDto = await _decorated.GetSmartPlaylistAsync(smartPlaylistId).ConfigureAwait(false);
                return smartPlaylistDto;
            }
        }

        public async Task<SmartPlaylistDto[]> LoadPlaylistsAsync(Guid userId)
        {
            using (PerfLogger.Create("LoadPlaylistsFromStore"))
            {
                return await _decorated.LoadPlaylistsAsync(userId).ConfigureAwait(false);
            }
        }

        public async Task<SmartPlaylistDto[]> GetAllSmartPlaylistsAsync()
        {
            using (PerfLogger.Create("GetAllSmartPlaylistsFromStore"))
            {
                return await _decorated.GetAllSmartPlaylistsAsync().ConfigureAwait(false);
            }
        }

        public void Save(SmartPlaylistDto smartPlaylist)
        {
            using (PerfLogger.Create("SaveSmartPlaylist", () => new {smartPlaylistName = smartPlaylist.Name}))
            {
                _decorated.Save(smartPlaylist);
            }
        }

        public void Delete(Guid userId, string smartPlaylistId)
        {
            using (PerfLogger.Create("DeleteSmartPlaylist"))
            {
                _decorated.Delete(userId, smartPlaylistId);
            }
        }
    }
}