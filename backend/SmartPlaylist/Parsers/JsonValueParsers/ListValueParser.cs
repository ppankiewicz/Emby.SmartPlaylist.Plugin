using System.Globalization;
using System.Text.RegularExpressions;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Parsers.JsonValueParsers
{
    public class ListValueParser : JsonValueParser
    {
        public static Regex ParseRegEx =
            new Regex("{numValue:(.*),kind:listValue,value:(.*)}", RegexOptions.IgnoreCase);

        public static Regex ParseRegEx2 =
            new Regex("{kind:listValue,value:(.*)}", RegexOptions.IgnoreCase);

        public override bool TryParse(string value, out Value val)
        {
            val = null;
            var match = ParseRegEx.Match(value);
            if (match.Success && match.Groups[2].Value is string strValue && float.TryParse(match.Groups[1].Value,
                    NumberStyles.Any, CultureInfo.InvariantCulture, out var numValue))
            {
                val = new ListValue(strValue, numValue);
                return true;
            }

            var match2 = ParseRegEx2.Match(value);
            if (match2.Success && match2.Groups[1].Value is string strValue2)
            {
                val = new ListValue(strValue2);
                return true;
            }

            return false;
        }
    }
}