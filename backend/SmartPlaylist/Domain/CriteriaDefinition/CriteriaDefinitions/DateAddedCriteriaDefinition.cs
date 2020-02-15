using SmartPlaylist.Domain.Rule;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Domain.CriteriaDefinition.CriteriaDefinitions
{
    public class DateAddedCriteriaDefinition : CriteriaDefinition
    {
        public override string Name => "Date Added";
        public override CriteriaDefinitionType Type => DateDefinitionType.Instance;

        public override Value GetValue(UserItem item)
        {
            return DateValue.Create(item.Item.DateCreated);
        }
    }
}