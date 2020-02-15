using System.Linq;
using SmartPlaylist.Domain.Rule;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Domain.CriteriaDefinition.CriteriaDefinitions
{
    public class MediaTypeCriteriaDefinition : CriteriaDefinition
    {
        public override string Name => "Media Type";
        public override CriteriaDefinitionType Type => ListValueDefinitionType.Instance;

        public override Value[] Values => Const.SupportedItemTypeNames.Select(ListValue.Create).Cast<Value>().ToArray();

        public override Value GetValue(UserItem item)
        {
            return ListValue.Create(item.Item.GetType().Name);
        }
    }
}