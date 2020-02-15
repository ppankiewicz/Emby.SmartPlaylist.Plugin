using System;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Contracts
{
    [Serializable]
    public class OperatorDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public Value DefaultValue { get; set; }
    }
}