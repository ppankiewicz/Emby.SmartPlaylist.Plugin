using SmartPlaylist.Domain.Rule;
using SmartPlaylist.Domain.Values;
using SmartPlaylist.Services;

namespace SmartPlaylist.Domain.CriteriaDefinition.CriteriaDefinitions
{
    public class ReleaseDateCriteriaDefinition : CriteriaDefinition
    {
        public override string Name => "Release date";
        public override CriteriaDefinitionType Type => DateDefinitionType.Instance;

        public override Value GetValue(UserItem item)
        {
            return DateValue.Create(ReleaseDateGetter.Get(item.Item));
        }
    }
}