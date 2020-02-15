using System;
using System.Threading.Tasks;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Net;
using MediaBrowser.Model.Services;
using SmartPlaylist.Handlers.Commands;
using SmartPlaylist.Infrastructure.MesssageBus;
using SmartPlaylist.Services.SmartPlaylist;

namespace SmartPlaylist.Api
{
    public class SmartPlaylistApi : IService, IRequiresRequest
    {
        private readonly MessageBus _messageBus;
        private readonly ISessionContext _sessionContext;
        private readonly ISmartPlaylistStore _smartPlaylistStore;
        private readonly SmartPlaylistValidator _smartPlaylistValidator;


        public SmartPlaylistApi(ISessionContext sessionContext
        )
        {
            _sessionContext = sessionContext;

            _messageBus = Plugin.Instance.MessageBus;
            _smartPlaylistStore = Plugin.Instance.SmartPlaylistStore;
            _smartPlaylistValidator = Plugin.Instance.SmartPlaylistValidator;
        }

        public IRequest Request { get; set; }

        public void Post(SaveSmartPlaylist request)
        {
            var playlist = request;
            var user = GetUser();

            playlist.UserId = user.Id;
            playlist.LastShuffleUpdate = DateTimeOffset.UtcNow.Date;
            _smartPlaylistValidator.Validate(playlist);
            _smartPlaylistStore.Save(playlist);

            _messageBus.Publish(new UpdateSmartPlaylistCommand(Guid.Parse(playlist.Id)));
        }

        public void Delete(DeleteSmartPlaylist request)
        {
            var user = GetUser();
            _smartPlaylistStore.Delete(user.Id, request.Id);
        }

        public async Task<object> Get(GetAppData request)
        {
            var user = GetUser();
            var playlists = await _smartPlaylistStore.LoadPlaylistsAsync(user.Id).ConfigureAwait(false);

            return new GetAppDataResponse
            {
                Playlists = playlists
            };
        }

        private User GetUser()
        {
            return _sessionContext.GetUser(Request);
        }
    }
}