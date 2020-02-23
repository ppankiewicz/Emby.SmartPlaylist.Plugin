using MediaBrowser.Model.Entities;

namespace SmartPlaylist.Domain.CriteriaDefinition.CriteriaDefinitions
{
    public class ActorCriteriaDefinition : PeopleCriteriaDefinition
    {
        public override string Name => "Actor";

        public override PersonType[] PersonTypes => new[] {PersonType.Actor};
    }
}