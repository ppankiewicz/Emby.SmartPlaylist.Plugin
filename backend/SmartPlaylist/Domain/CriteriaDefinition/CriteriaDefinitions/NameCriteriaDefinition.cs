using SmartPlaylist.Domain.Rule;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Domain.CriteriaDefinition.CriteriaDefinitions
{
    public class NameCriteriaDefinition : CriteriaDefinition
    {
        public override string Name => "Name";
        public override CriteriaDefinitionType Type => StringDefinitionType.Instance;

        public override Value GetValue(UserItem item)
        {
            return StringValue.Create(item.Item.Name);
        }
    }
}