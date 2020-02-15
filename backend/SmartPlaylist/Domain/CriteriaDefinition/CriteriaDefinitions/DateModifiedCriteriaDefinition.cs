using SmartPlaylist.Domain.Rule;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Domain.CriteriaDefinition.CriteriaDefinitions
{
    public class DateModifiedCriteriaDefinition : CriteriaDefinition
    {
        public override string Name => "Date Modified";
        public override CriteriaDefinitionType Type => DateDefinitionType.Instance;

        public override Value GetValue(UserItem item)
        {
            return DateValue.Create(item.Item.DateLastSaved);
        }
    }
}