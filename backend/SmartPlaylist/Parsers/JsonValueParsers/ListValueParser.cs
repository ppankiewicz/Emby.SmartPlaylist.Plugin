using System.Text.RegularExpressions;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Parsers.JsonValueParsers
{
    public class ListValueParser : JsonValueParser
    {
        public static Regex ParseRegEx = new Regex("{kind:listValue,value:(.*)}", RegexOptions.IgnoreCase);

        public override bool TryParse(string value, out Value val)
        {
            val = null;
            var match = ParseRegEx.Match(value);
            if (match.Success && match.Groups[1].Value is string strValue)
            {
                val = new ListValue(strValue);
                return true;
            }

            return false;
        }
    }
}