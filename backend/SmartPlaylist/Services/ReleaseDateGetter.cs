using System;
using System.Globalization;
using MediaBrowser.Controller.Entities;

namespace SmartPlaylist.Services
{
    public static class ReleaseDateGetter
    {
        public static DateTimeOffset Get(BaseItem item)
        {
            return item.PremiereDate.GetValueOrDefault(GetDate(item.ProductionYear.GetValueOrDefault(0)));
        }
        private static DateTimeOffset GetDate(int year)
        {
            if (DateTimeOffset.TryParseExact(year.ToString(), "yyyy", new NumberFormatInfo(), DateTimeStyles.None,
                out var dateTime)) return dateTime;
            return DateTimeOffset.MinValue;
        }
    }
}