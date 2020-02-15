using MediaBrowser.Controller.Entities.TV;
using SmartPlaylist.Domain.Rule;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Domain.CriteriaDefinition.CriteriaDefinitions
{
    public class SeasonCriteriaDefinition : CriteriaDefinition
    {
        public override string Name => "Season";
        public override CriteriaDefinitionType Type => NumberDefinitionType.Instance;

        public override Value GetValue(UserItem item)
        {
            if (item.Item is Episode && item.Item.ParentIndexNumber.HasValue)
                return NumberValue.Create(item.Item.ParentIndexNumber.Value);

            return Value.None;
        }
    }
}