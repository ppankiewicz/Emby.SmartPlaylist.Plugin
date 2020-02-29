using MediaBrowser.Model.Plugins;

namespace SmartPlaylist.Configuration
{
    public class PluginConfiguration : BasePluginConfiguration
    {
        public string Version =>
            typeof(Plugin).Assembly.GetName().Version.ToString();
    }
}