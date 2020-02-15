using System;
using System.Linq;
using MediaBrowser.Controller.Entities.Audio;
using MediaBrowser.Controller.Entities.Movies;
using MediaBrowser.Controller.Entities.TV;
using MediaBrowser.Model.Tasks;
using SmartPlaylist.Infrastructure.Queue;

namespace SmartPlaylist
{
    public static class Const
    {
        public const int MaxGetUserItemsCount = 10000;
        public const int ForEachMaxDegreeOfParallelism = 3;


        public static readonly Type[] SupportedItemTypes = {typeof(Audio), typeof(Movie), typeof(Episode)};
        public static readonly string[] SupportedItemTypeNames = SupportedItemTypes.Select(x => x.Name).ToArray();

        public static readonly Type[] ListenForChangeItemTypes =
            SupportedItemTypes.Concat(new[] {typeof(MusicAlbum)}).ToArray();


        public static TimeSpan GetAllSmartPlaylistsCacheExpiration = TimeSpan.FromHours(2);
        public static TimeSpan DefaultSemaphoreSlimTimeout = TimeSpan.FromSeconds(30);

        public static AutoDequeueQueueConfig UpdatedItemsQueueConfig => new AutoDequeueQueueConfig
        {
            InactiveDequeueTime = TimeSpan.FromSeconds(2),
            AbsoluteDequeueTime = TimeSpan.FromSeconds(3),
            MaxItemsLimit = 4
        };


        public static TaskTriggerInfo[] RefreshAllSmartPlaylistsTaskTriggers
        {
            get
            {
                return new[]
                {
                    new TaskTriggerInfo
                        {Type = TaskTriggerInfo.TriggerDaily, TimeOfDayTicks = TimeSpan.FromHours(1).Ticks}
                };
            }
        }
    }
}