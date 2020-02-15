using System.Linq;
using SmartPlaylist.Extensions;

namespace SmartPlaylist.Parsers.JsonValueParsers
{
    public static class DefinedJsonValueParsers
    {
        public static JsonValueParser[] All =>
            typeof(JsonValueParser).Assembly.FindAndCreateDerivedTypes<JsonValueParser>().ToArray();
    }
}