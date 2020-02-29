using MediaBrowser.Controller.Entities.Movies;
using SmartPlaylist.Domain.Rule;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Domain.CriteriaDefinition.CriteriaDefinitions
{
    public class MovieNameCriteriaDefinition : CriteriaDefinition
    {
        public override string Name => "Movie Name";
        public override CriteriaDefinitionType Type => StringDefinitionType.Instance;

        public override Value GetValue(UserItem item)
        {
            if (item.Item is Movie)
                return StringValue.Create(item.Item.Name);

            return Value.None;
        }
    }
}