using System;
using System.Text.RegularExpressions;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Parsers.JsonValueParsers
{
    public class LastPeriodValueParser : JsonValueParser
    {
        public static Regex ParseRegEx =
            new Regex("{kind:lastPeriod,value:(.*),periodType:(.*)}", RegexOptions.IgnoreCase);

        public override bool TryParse(string value, out Value val)
        {
            val = null;
            var match = ParseRegEx.Match(value);
            if (match.Success && int.TryParse(match.Groups[1].Value, out var number))
            {
                val = LastPeriodValue.Create(number, ParsePeriodType(match.Groups[2].Value));
                return true;
            }

            return false;
        }

        private PeriodType ParsePeriodType(string value)
        {
            if (Enum.TryParse(value, true, out PeriodType periodType)) return periodType;

            return PeriodType.Weeks;
        }
    }
}