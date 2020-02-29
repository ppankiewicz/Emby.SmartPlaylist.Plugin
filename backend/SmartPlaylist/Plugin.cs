using System;
using System.Collections.Generic;
using System.IO;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Controller;
using MediaBrowser.Controller.Library;
using MediaBrowser.Controller.Playlists;
using MediaBrowser.Controller.Session;
using MediaBrowser.Model.Drawing;
using MediaBrowser.Model.Logging;
using MediaBrowser.Model.Plugins;
using MediaBrowser.Model.Serialization;
using SmartPlaylist.Configuration;
using SmartPlaylist.Handlers.CommandHandlers;
using SmartPlaylist.Infrastructure.MesssageBus;
using SmartPlaylist.Infrastructure.MesssageBus.Decorators;
using SmartPlaylist.Infrastructure.MesssageBus.Decorators.DebugDecorators;
using SmartPlaylist.Services;
using SmartPlaylist.Services.SmartPlaylist;

namespace SmartPlaylist
{
    public class Plugin : BasePlugin<PluginConfiguration>, IHasWebPages, IHasThumbImage
    {
        private readonly ILogger _logger;
        private readonly ISessionManager _sessionManager;

        public Plugin(IApplicationPaths applicationPaths, IXmlSerializer xmlSerializer,
            IPlaylistManager playlistManager,
            ILibraryManager libraryManager,
            ILogger logger, IUserManager userManager, IJsonSerializer jsonSerializer,
            IServerApplicationPaths serverApplicationPaths, ISessionManager sessionManager)
            : base(applicationPaths, xmlSerializer)
        {
            _logger = logger;
            _sessionManager = sessionManager;
            Instance = this;
            var smartPlaylistFileSystem =
                new EnsureBaseDirSmartPlaylistFileSystemDecorator(new SmartPlaylistFileSystem(serverApplicationPaths));
            var smartPlaylistStore =
                new CacheableSmartPlaylistStore(
                    new CleanupOldCriteriaDecorator(new SmartPlaylistStore(jsonSerializer, smartPlaylistFileSystem)));
            var userItemsProvider = new UserItemsProvider(libraryManager);
            var smartPlaylistProvider = new SmartPlaylistProvider(smartPlaylistStore);
            var playlistRepository = new PlaylistRepository(userManager, libraryManager);
            var playlistItemsUpdater = new PlaylistItemsUpdater(playlistManager);

            MessageBus = new MessageBus();

            SubscribeMessageHandlers(smartPlaylistProvider, userItemsProvider, playlistRepository,
                playlistItemsUpdater, smartPlaylistStore);

            SmartPlaylistStore = smartPlaylistStore;
            SmartPlaylistValidator = new SmartPlaylistValidator();
        }

        public SmartPlaylistValidator SmartPlaylistValidator { get; }

        public override Guid Id => Guid.Parse("3C96F5BC-4182-4B86-B05D-F730F2611E45");

        public override string Name => "SmartPlaylist";

        public override string Description => "Allow to define smart playlist rules.";

        public static Plugin Instance { get; private set; }

        public MessageBus MessageBus { get; }
        public ISmartPlaylistStore SmartPlaylistStore { get; }

        public Stream GetThumbImage()
        {
            var type = GetType();
            return type.Assembly.GetManifestResourceStream(type.Namespace + ".Images.plugin.png");
        }

        public ImageFormat ThumbImageFormat => ImageFormat.Png;

        public IEnumerable<PluginPageInfo> GetPages()
        {
            return new[]
            {
                new PluginPageInfo
                {
                    Name = "smartplaylist.html",
                    EmbeddedResourcePath = GetType().Namespace + ".Configuration.smartplaylist.html",
                    EnableInMainMenu = true
                },
                new PluginPageInfo
                {
                    Name = "smartplaylist.css",
                    EmbeddedResourcePath = GetType().Namespace + ".Configuration.smartplaylist.css"
                },
                new PluginPageInfo
                {
                    Name = "smartplaylist.js",
                    EmbeddedResourcePath = GetType().Namespace + ".Configuration.smartplaylist.js"
                }
            };
        }

        private void SubscribeMessageHandlers(SmartPlaylistProvider smartPlaylistProvider,
            UserItemsProvider userItemsProvider, PlaylistRepository playlistRepository,
            PlaylistItemsUpdater playlistItemsUpdater, ISmartPlaylistStore smartPlaylistStore)
        {
            var updateSmartPlaylistCommandHandler =
                new UpdateSmartPlaylistCommandHandler(userItemsProvider, smartPlaylistProvider,
                    playlistRepository, playlistItemsUpdater, smartPlaylistStore);
            var updateAllSmartPlaylistsWithItemsCommandHandler =
                new UpdateAllSmartPlaylistsCommandHandler(MessageBus, smartPlaylistProvider,
                    playlistRepository, playlistItemsUpdater);

            MessageBus.Subscribe(Decorate(updateSmartPlaylistCommandHandler));
            MessageBus.Subscribe(Decorate(updateAllSmartPlaylistsWithItemsCommandHandler));
        }

        private IMessageHandler<T> Decorate<T>(IMessageHandler<T> messageHandler) where T : IMessage
        {
            return new SuppressExceptionDecorator<T>(messageHandler, _logger);
        }

        private IMessageHandlerAsync<T> Decorate<T>(IMessageHandlerAsync<T> messageHandler) where T : IMessage
        {
#if DEBUG
            return new SuppressAsyncExceptionDecorator<T>(
                new DebugShowErrorMessageDecorator<T>(
                    new DebugShowDurationMessageDecorator<T>(messageHandler, _sessionManager), _sessionManager),
                _logger);
#else
            return new SuppressAsyncExceptionDecorator<T>(messageHandler, _logger);

#endif
        }
    }
}