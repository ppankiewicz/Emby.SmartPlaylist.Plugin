using System;
using System.Text.RegularExpressions;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Parsers.JsonValueParsers
{
    public class DateRangeValueParser : JsonValueParser
    {
        public static Regex ParseRegEx = new Regex("{kind:dateRange,from:(.*),to:(.*)}", RegexOptions.IgnoreCase);

        public override bool TryParse(string value, out Value val)
        {
            val = null;
            var match = ParseRegEx.Match(value);
            if (match.Success && DateTimeOffset.TryParse(match.Groups[1].Value, out var fromDate) &&
                DateTimeOffset.TryParse(match.Groups[2].Value, out var toDate))
            {
                val = DateRangeValue.Create(fromDate, toDate);
                return true;
            }

            return false;
        }
    }
}