using System;
using SmartPlaylist.Infrastructure.MesssageBus;

namespace SmartPlaylist.Handlers.Commands
{
    public class UpdateSmartPlaylistCommand : IMessage
    {
        public UpdateSmartPlaylistCommand(Guid smartPlaylistId)
        {
            SmartPlaylistId = smartPlaylistId;
        }

        public Guid SmartPlaylistId { get; }
    }
}