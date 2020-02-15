using MediaBrowser.Controller.Entities.TV;
using SmartPlaylist.Domain.Rule;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Domain.CriteriaDefinition.CriteriaDefinitions
{
    public class EpisodeCriteriaDefinition : CriteriaDefinition
    {
        public override string Name => "Episode";
        public override CriteriaDefinitionType Type => NumberDefinitionType.Instance;

        public override Value GetValue(UserItem item)
        {
            if (item.Item is Episode && item.Item.IndexNumber.HasValue)
                return NumberValue.Create(item.Item.IndexNumber.Value);

            return Value.None;
        }
    }
}