using System;
using MediaBrowser.Model.Serialization;

namespace SmartPlaylist.Domain.Values
{
    public abstract class Value
    {
        public static readonly NoneValue None = new NoneValue();
        public abstract string Kind { get; }

        [IgnoreDataMember] public bool IsNone => this == None;

        public bool IsType(Type type)
        {
            return GetType() == type;
        }
    }
}