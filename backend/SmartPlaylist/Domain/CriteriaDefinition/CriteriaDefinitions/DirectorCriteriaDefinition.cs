using MediaBrowser.Model.Entities;

namespace SmartPlaylist.Domain.CriteriaDefinition.CriteriaDefinitions
{
    public class DirectorCriteriaDefinition : PeopleCriteriaDefinition
    {
        public override string Name => "Director";
        public override PersonType[] PersonTypes => new[] {PersonType.Director};
    }
}