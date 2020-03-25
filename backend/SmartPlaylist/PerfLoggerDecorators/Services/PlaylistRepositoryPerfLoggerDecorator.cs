using System;
using SmartPlaylist.Domain;
using SmartPlaylist.Infrastructure;
using SmartPlaylist.Services;

namespace SmartPlaylist.PerfLoggerDecorators.Services
{
    public class PlaylistRepositoryPerfLoggerDecorator : IPlaylistRepository
    {
        private readonly IPlaylistRepository _decorated;

        public PlaylistRepositoryPerfLoggerDecorator(IPlaylistRepository decorated)
        {
            _decorated = decorated;
        }

        public UserPlaylist GetUserPlaylist(Guid userId, string playlistName)
        {
            using (PerfLogger.Create("GetUserPlaylist", () => new {smartPlaylistName = playlistName}))
            {
                return _decorated.GetUserPlaylist(userId, playlistName);
            }
        }
    }
}