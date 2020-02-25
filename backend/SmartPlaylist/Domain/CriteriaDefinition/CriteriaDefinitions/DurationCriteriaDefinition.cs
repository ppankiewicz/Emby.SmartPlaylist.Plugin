using System;
using SmartPlaylist.Domain.Rule;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Domain.CriteriaDefinition.CriteriaDefinitions
{
    public class DurationCriteriaDefinition : CriteriaDefinition
    {
        public override string Name => "Duration(minutes)";
        public override CriteriaDefinitionType Type => NumberDefinitionType.Instance;

        public override Value GetValue(UserItem item)
        {
            if (item.Item.RunTimeTicks.HasValue)
            {
                return NumberValue.Create(GetMinutes(item.Item.RunTimeTicks.GetValueOrDefault()));
            }

            return Value.None;
        }

        private static int GetMinutes(long ticks)
        {
            return (int) Math.Ceiling(new TimeSpan(ticks).TotalMinutes);
        }
    }
}