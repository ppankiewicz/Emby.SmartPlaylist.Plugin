using SmartPlaylist.Domain.Rule;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Domain.CriteriaDefinition.CriteriaDefinitions
{
    public class CommunityRatingCriteriaDefinition : CriteriaDefinition
    {
        public override string Name => "Community rating";
        public override CriteriaDefinitionType Type => NumberDefinitionType.Instance;

        public override Value GetValue(UserItem item)
        {
            if (item.Item.CommunityRating.HasValue)
                return NumberValue.Create(item.Item.CommunityRating.GetValueOrDefault());

            return Value.None;
        }
    }
}