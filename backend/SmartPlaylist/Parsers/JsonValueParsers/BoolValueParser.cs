using System.Text.RegularExpressions;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Parsers.JsonValueParsers
{
    public class BoolValueParser : JsonValueParser
    {
        public static Regex ParseRegEx = new Regex("{kind:bool,value:(.*)}", RegexOptions.IgnoreCase);

        public override bool TryParse(string value, out Value val)
        {
            val = null;
            var match = ParseRegEx.Match(value);
            if (match.Success && bool.TryParse(match.Groups[1].Value, out var boolVal))
            {
                val = BoolValue.Create(boolVal);
                return true;
            }

            return false;
        }
    }
}