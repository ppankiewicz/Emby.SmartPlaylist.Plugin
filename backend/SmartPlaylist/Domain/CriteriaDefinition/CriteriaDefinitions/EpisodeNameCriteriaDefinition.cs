using MediaBrowser.Controller.Entities.TV;
using SmartPlaylist.Domain.Rule;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Domain.CriteriaDefinition.CriteriaDefinitions
{
    public class EpisodeNameCriteriaDefinition : CriteriaDefinition
    {
        public override string Name => "Episode Name";
        public override CriteriaDefinitionType Type => StringDefinitionType.Instance;

        public override Value GetValue(UserItem item)
        {
            if (item.Item is Episode)
                return StringValue.Create(item.Item.Name);

            return Value.None;
        }
    }
}