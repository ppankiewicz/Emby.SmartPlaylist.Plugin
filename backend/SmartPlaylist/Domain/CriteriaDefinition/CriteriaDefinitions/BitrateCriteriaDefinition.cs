using System;
using SmartPlaylist.Domain.Rule;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Domain.CriteriaDefinition.CriteriaDefinitions
{
    public class BitrateCriteriaDefinition : CriteriaDefinition
    {
        public override string Name => "Bitrate (kbps)";
        public override CriteriaDefinitionType Type => NumberDefinitionType.Instance;

        public override Value GetValue(UserItem item)
        {
            var kbps = (int) Math.Round((double) item.Item.TotalBitrate / 1000);
            return NumberValue.Create(kbps);
        }
    }
}