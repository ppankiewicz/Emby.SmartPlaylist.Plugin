using System.Linq;
using SmartPlaylist.Domain.Rule;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Domain.CriteriaDefinition.CriteriaDefinitions
{
    public class TagCriteriaDefinition : CriteriaDefinition
    {
        public override string Name => "Tag";
        public override CriteriaDefinitionType Type => StringDefinitionType.Instance;

        public override Value GetValue(UserItem item)
        {
            return ArrayValue<StringValue>.Create(item.Item.Tags?.Select(StringValue.Create).ToArray());
        }
    }
}