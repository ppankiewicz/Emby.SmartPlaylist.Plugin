using System;
using System.Text.RegularExpressions;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Parsers.JsonValueParsers
{
    public class DateValueParser : JsonValueParser
    {
        public static Regex ParseRegEx = new Regex("{kind:date,value:(.*)}", RegexOptions.IgnoreCase);

        public override bool TryParse(string value, out Value val)
        {
            val = null;
            var match = ParseRegEx.Match(value);
            if (match.Success && DateTimeOffset.TryParse(match.Groups[1].Value, out var date))
            {
                val = DateValue.Create(date);
                return true;
            }

            return false;
        }
    }
}