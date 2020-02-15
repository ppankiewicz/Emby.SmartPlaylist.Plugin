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
            if (match.Success && int.TryParse(match.Groups[1].Value, out var from) &&
                int.TryParse(match.Groups[2].Value, out var to))
            {
                val = NumberRangeValue.Create(from, to);
                return true;
            }

            return false;
        }
    }
}