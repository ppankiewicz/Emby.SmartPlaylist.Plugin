using System.Text.RegularExpressions;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Parsers.JsonValueParsers
{
    public class NumberValueParser : JsonValueParser
    {
        public static Regex ParseRegEx = new Regex("{kind:number,value:(.*)}", RegexOptions.IgnoreCase);

        public override bool TryParse(string value, out Value val)
        {
            val = null;
            var match = ParseRegEx.Match(value);
            if (match.Success && int.TryParse(match.Groups[1].Value, out var number))
            {
                val = NumberValue.Create(number);
                return true;
            }

            return false;
        }
    }
}