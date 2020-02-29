using System.Linq;
using SmartPlaylist.Extensions;

namespace SmartPlaylist.Domain.CriteriaDefinition
{
    public static class DefinedCriteriaDefinitions
    {
        public static readonly CriteriaDefinition[] All = typeof(CriteriaDefinition).Assembly
            .FindAndCreateDerivedTypes<CriteriaDefinition>().ToArray();
    }
}