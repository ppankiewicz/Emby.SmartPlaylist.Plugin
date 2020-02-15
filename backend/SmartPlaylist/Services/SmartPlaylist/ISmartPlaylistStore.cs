using System;
using System.Threading.Tasks;
using SmartPlaylist.Contracts;

namespace SmartPlaylist.Services.SmartPlaylist
{
    public interface ISmartPlaylistStore
    {
        Task<SmartPlaylistDto> GetSmartPlaylistAsync(Guid smartPlaylistId);
        Task<SmartPlaylistDto[]> LoadPlaylistsAsync(Guid userId);
        Task<SmartPlaylistDto[]> GetAllSmartPlaylistsAsync();
        void Save(SmartPlaylistDto smartPlaylist);
        void Delete(Guid userId, string smartPlaylistId);
    }
}