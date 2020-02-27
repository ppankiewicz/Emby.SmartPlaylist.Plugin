using System.Globalization;
using System.Text.RegularExpressions;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Parsers.JsonValueParsers
{
    public class NumberRangeValueParser : JsonValueParser
    {
        public static Regex ParseRegEx = new Regex("{kind:numberRange,from:(.*),to:(.*)}", RegexOptions.IgnoreCase);

        public override bool TryParse(string value, out Value val)
        {
            val = null;
            var match = ParseRegEx.Match(value);
            if (match.Success && float.TryParse(match.Groups[1].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out var from) &&
                float.TryParse(match.Groups[2].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out var to))
            {
                val = NumberRangeValue.Create(from, to);
                return true;
            }

            return false;
        }
    }
}