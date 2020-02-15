using System.Collections.Generic;
using MediaBrowser.Controller.Entities;

namespace SmartPlaylist.Comparers
{
    public class BaseItemEqualByInternalId : IEqualityComparer<BaseItem>
    {
        public bool Equals(BaseItem x, BaseItem y)
        {
            return x.InternalId == y.InternalId;
        }

        public int GetHashCode(BaseItem obj)
        {
            return obj.InternalId.GetHashCode();
        }
    }
}