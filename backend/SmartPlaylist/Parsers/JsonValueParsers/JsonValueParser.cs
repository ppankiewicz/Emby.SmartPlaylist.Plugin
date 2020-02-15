using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Parsers.JsonValueParsers
{
    public abstract class JsonValueParser
    {
        public abstract bool TryParse(string value, out Value val);
    }
}