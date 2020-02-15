using System;

namespace SmartPlaylist.Infrastructure.Queue
{
    public class AutoDequeueQueueConfig
    {
        public TimeSpan InactiveDequeueTime { get; set; } = TimeSpan.FromSeconds(30);
        public uint MaxItemsLimit { get; set; } = 10000;
        public TimeSpan AbsoluteDequeueTime { get; set; } = TimeSpan.FromMinutes(2);
    }
}