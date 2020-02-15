using System;
using MediaBrowser.Model.Plugins;
using SmartPlaylist.Contracts;
using SmartPlaylist.Domain.CriteriaDefinition;

namespace SmartPlaylist.Configuration
{
    public class PluginConfiguration : BasePluginConfiguration
    {
        public SmartPlaylistDto[] Playlists { get; set; } = new SmartPlaylistDto[0];

        public Guid UserId { get; set; }

        public CriteriaDefinition[] RulesCriteriaDefinitions => DefinedCriteriaDefinitions.All;
    }
}