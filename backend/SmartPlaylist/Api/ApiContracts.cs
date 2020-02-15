using MediaBrowser.Model.Services;
using SmartPlaylist.Contracts;
using SmartPlaylist.Domain;
using SmartPlaylist.Domain.CriteriaDefinition;

namespace SmartPlaylist.Api
{
    [Route("/smartplaylist", "POST", Summary = "")]
    public class SaveSmartPlaylist : SmartPlaylistDto, IReturnVoid
    {
    }

    [Route("/smartplaylist/{Id}", "DELETE", Summary = "")]
    public class DeleteSmartPlaylist : IReturnVoid
    {
        public string Id { get; set; }
    }

    public class GetAppDataResponse
    {
        public SmartPlaylistDto[] Playlists { get; set; }

        public CriteriaDefinition[] RulesCriteriaDefinitions => DefinedCriteriaDefinitions.All;

        public string[] LimitOrdersBy => DefinedLimitOrders.AllNames;
    }

    [Route("/smartplaylist/appData", "GET", Summary = "")]
    public class GetAppData : IReturn<GetAppDataResponse>
    {
    }
}