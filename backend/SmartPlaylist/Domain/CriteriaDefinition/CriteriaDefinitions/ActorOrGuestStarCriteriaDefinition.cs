using MediaBrowser.Model.Entities;

namespace SmartPlaylist.Domain.CriteriaDefinition.CriteriaDefinitions
{
    public class ActorOrGuestStarCriteriaDefinition : PeopleCriteriaDefinition
    {
        public override string Name => "ActorOrGuestStar";
        public override PersonType[] PersonTypes => new[] {PersonType.Actor, PersonType.GuestStar};
    }
}