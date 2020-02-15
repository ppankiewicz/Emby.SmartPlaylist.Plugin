using System.Linq;
using SmartPlaylist.Domain.Rule;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Domain.CriteriaDefinition.CriteriaDefinitions
{
    public class GenreCriteriaDefinition : CriteriaDefinition
    {
        public override string Name => "Genre";
        public override CriteriaDefinitionType Type => StringDefinitionType.Instance;

        public override Value GetValue(UserItem item)
        {
            return ArrayValue<StringValue>.Create(item.Item.Genres?.Select(StringValue.Create).ToArray());
        }
    }
}