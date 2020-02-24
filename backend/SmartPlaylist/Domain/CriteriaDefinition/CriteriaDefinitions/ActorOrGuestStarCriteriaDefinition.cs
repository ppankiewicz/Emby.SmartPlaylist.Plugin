using MediaBrowser.Model.Entities;

namespace SmartPlaylist.Domain.CriteriaDefinition.CriteriaDefinitions
{
    public class ActorOrGuestStarCriteriaDefinition : PeopleCriteriaDefinition
    {
        public override string Name => @"Actor\GuestStar";
        public override PersonType[] PersonTypes => new[] {PersonType.Actor, PersonType.GuestStar};
    }
}