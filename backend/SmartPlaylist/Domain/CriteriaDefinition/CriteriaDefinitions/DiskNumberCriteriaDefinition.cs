using MediaBrowser.Controller.Entities.Audio;
using SmartPlaylist.Domain.Rule;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Domain.CriteriaDefinition.CriteriaDefinitions
{
    public class DiskNumberCriteriaDefinition : CriteriaDefinition
    {
        public override string Name => "Disk Number";
        public override CriteriaDefinitionType Type => NumberDefinitionType.Instance;

        public override Value GetValue(UserItem item)
        {
            if (item.Item is Audio && item.Item.ParentIndexNumber.HasValue)
                return NumberValue.Create(item.Item.ParentIndexNumber.Value);

            return Value.None;
        }
    }
}