using System;
using MediaBrowser.Model.Serialization;

namespace SmartPlaylist.Domain.Values
{
    public abstract class Value
    {
        public abstract string Kind { get; }

        public static NoneValue None = new NoneValue();

        [IgnoreDataMember]
        public bool IsNone => this == None;

        public bool IsType(Type type)
        {
            return GetType() == type;
        }
    }
}