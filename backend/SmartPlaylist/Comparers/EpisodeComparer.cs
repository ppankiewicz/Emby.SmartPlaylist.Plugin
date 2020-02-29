using System;
using System.Collections.Generic;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Entities.TV;

namespace SmartPlaylist.Comparers
{
    public class EpisodeComparer : IComparer<BaseItem>
    {
        public int Compare(BaseItem x, BaseItem y)
        {
            var xEpisode = x as Episode;
            var yEpisode = y as Episode;

            if (xEpisode != null && yEpisode != null)
                return CompareEpisodes(xEpisode, yEpisode);

            if (xEpisode != null) return -1;

            if (yEpisode != null) return 1;

            return 0;
        }

        private static int CompareEpisodes(Episode xEpisode, Episode yEpisode)
        {
            return string.Compare(GetSortName(xEpisode), GetSortName(yEpisode),
                StringComparison.InvariantCultureIgnoreCase);
        }

        private static string GetSortName(Episode episode)
        {
            return $"{episode.SeriesName} - {episode.SortName}";
        }
    }
}