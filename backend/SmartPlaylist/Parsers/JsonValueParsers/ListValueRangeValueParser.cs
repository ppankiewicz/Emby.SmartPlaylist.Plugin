using System.Text.RegularExpressions;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Parsers.JsonValueParsers
{
    public class ListValueRangeValueParser : JsonValueParser
    {
        public static Regex ParseRegEx = new Regex("{kind:listValueRange,from:(.*),to:(.*)}", RegexOptions.IgnoreCase);
        private readonly ListValueParser _listValueParser;

        public ListValueRangeValueParser()
        {
            _listValueParser = new ListValueParser();
        }

        public override bool TryParse(string value, out Value val)
        {
            val = null;
            var match = ParseRegEx.Match(value);
            if (match.Success && _listValueParser.TryParse(match.Groups[1].Value, out var from) &&
                _listValueParser.TryParse(match.Groups[2].Value, out var to))
            {
                val = ListValueRange.Create(from as ListValue, to as ListValue);
                return true;
            }

            return false;
        }
    }
}