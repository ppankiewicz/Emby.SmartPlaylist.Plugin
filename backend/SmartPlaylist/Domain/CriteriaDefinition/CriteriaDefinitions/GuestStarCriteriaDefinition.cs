using MediaBrowser.Model.Entities;

namespace SmartPlaylist.Domain.CriteriaDefinition.CriteriaDefinitions
{
    public class GuestStarCriteriaDefinition : PeopleCriteriaDefinition
    {
        public override string Name => "GuestStar";
        public override PersonType[] PersonTypes => new[] { PersonType.GuestStar };
    }
}