using MediaBrowser.Controller.Entities.TV;
using SmartPlaylist.Domain.Rule;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Domain.CriteriaDefinition.CriteriaDefinitions
{
    public class SeriesCriteriaDefinition : CriteriaDefinition
    {
        public override string Name => "Series";
        public override CriteriaDefinitionType Type => StringDefinitionType.Instance;

        public override Value GetValue(UserItem item)
        {
            if (item.Item is Episode episode)
                return StringValue.Create(episode.SeriesName);

            return Value.None;
        }
    }
}