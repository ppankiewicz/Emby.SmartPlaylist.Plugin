using System.Linq;
using SmartPlaylist.Domain.Rule;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Domain.CriteriaDefinition.CriteriaDefinitions
{
    public class MediaTypeCriteriaDefinition : CriteriaDefinition
    {
        private static readonly Value[] ListValues = Const.SupportedItemTypeNames.Select(s => ListValue.Create(s)).Cast<Value>().ToArray();

        public override string Name => "Media Type";
        public override CriteriaDefinitionType Type => new ListValueDefinitionType(ListValues.First() as ListValue);

        public override Value[] Values => ListValues;

        public override Value GetValue(UserItem item)
        {
            return ListValue.Create(item.Item.GetType().Name);
        }
    }
}