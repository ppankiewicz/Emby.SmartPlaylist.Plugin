using System;
using System.Collections.Generic;
using System.Globalization;
using MediaBrowser.Controller.Entities;

namespace SmartPlaylist.Comparers
{
    public class PremiereDateComparer : IComparer<BaseItem>
    {
        public int Compare(BaseItem x, BaseItem y)
        {
            return GetPremiereDate(x).CompareTo(GetPremiereDate(y));
        }

        private DateTimeOffset GetPremiereDate(BaseItem baseItem)
        {
            return baseItem.PremiereDate.GetValueOrDefault(GetDate(baseItem.ProductionYear.GetValueOrDefault(0)));
        }

        private DateTimeOffset GetDate(int year)
        {
            if (DateTimeOffset.TryParseExact(year.ToString(), "yyyy", new NumberFormatInfo(), DateTimeStyles.None,
                out var dateTime)) return dateTime;
            return DateTimeOffset.MinValue;
        }
    }
}