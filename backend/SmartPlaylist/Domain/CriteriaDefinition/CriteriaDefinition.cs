using SmartPlaylist.Domain.Rule;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Domain.CriteriaDefinition
{
    public abstract class CriteriaDefinition
    {
        public abstract string Name { get; }

        public abstract CriteriaDefinitionType Type { get; }

        public virtual Value[] Values { get; } = new Value[0];

        public abstract Value GetValue(UserItem item);
    }
}