using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Model.Plugins;
using SmartPlaylist.Contracts;
using SmartPlaylist.Domain.CriteriaDefinition;
using SmartPlaylist.Domain.Operator;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Configuration
{
    public class PluginConfiguration : BasePluginConfiguration
    {
        public SmartPlaylistDto[] Playlists { get; set; } = new SmartPlaylistDto[0];

        public Guid UserId { get; set; }

        public CriteriaDefinition[] RulesCriteriaDefinitions => DefinedCriteriaDefinitions.All;
    }




}
