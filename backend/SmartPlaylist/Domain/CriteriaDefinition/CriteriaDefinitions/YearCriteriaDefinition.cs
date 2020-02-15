using SmartPlaylist.Domain.Rule;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Domain.CriteriaDefinition.CriteriaDefinitions
{
    public class YearCriteriaDefinition : CriteriaDefinition
    {
        public override string Name => "Year";
        public override CriteriaDefinitionType Type => NumberDefinitionType.Instance;

        public override Value GetValue(UserItem item)
        {
            if (item.Item.ProductionYear.HasValue)
                return NumberValue.Create(item.Item.ProductionYear.Value);

            return Value.None;
        }
    }
}