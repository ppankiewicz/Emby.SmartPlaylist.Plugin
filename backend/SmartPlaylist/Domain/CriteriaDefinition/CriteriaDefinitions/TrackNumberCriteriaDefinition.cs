using MediaBrowser.Controller.Entities.Audio;
using SmartPlaylist.Domain.Rule;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Domain.CriteriaDefinition.CriteriaDefinitions
{
    public class TrackNumberCriteriaDefinition : CriteriaDefinition
    {
        public override string Name => "Track Number";
        public override CriteriaDefinitionType Type => NumberDefinitionType.Instance;

        public override Value GetValue(UserItem item)
        {
            if (item.Item is Audio && item.Item.IndexNumber.HasValue)
                return NumberValue.Create(item.Item.IndexNumber.Value);

            return Value.None;
        }
    }
}