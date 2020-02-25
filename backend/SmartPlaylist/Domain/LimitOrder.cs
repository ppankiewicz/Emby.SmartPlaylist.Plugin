using System;
using System.Collections.Generic;
using System.Linq;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.Querying;
using SmartPlaylist.Comparers;
using SmartPlaylist.Extensions;

namespace SmartPlaylist.Domain
{
    public class SmartPlaylistLimit
    {
        public static SmartPlaylistLimit None => new SmartPlaylistLimit
        {
            MaxItems = 100000,
            OrderBy = new RandomLimitOrder()
        };

        public int MaxItems { get; set; }

        public LimitOrder OrderBy { get; set; }

        public bool HasLimit => this != None;
    }

    public static class DefinedLimitOrders
    {
        public static readonly LimitOrder[] All = typeof(LimitOrder).Assembly.FindAndCreateDerivedTypes<LimitOrder>().ToArray();

        public static readonly string[] AllNames = All.Select(x => x.Name).ToArray();
    }

    public abstract class LimitOrder
    {
        public abstract string Name { get; }

        public virtual ValueTuple<string, SortOrder>[] OrderBy => new (string, SortOrder)[0];

        public virtual IEnumerable<BaseItem> Order(IEnumerable<BaseItem> items)
        {
            return items;
        }
    }

    public class RandomLimitOrder : LimitOrder
    {
        public override string Name => "Random";

        public override (string, SortOrder)[] OrderBy => new (string, SortOrder)[]
            {(ItemSortBy.Random, SortOrder.Ascending)};
    }

    public class AlbumLimitOrder : LimitOrder
    {
        public override string Name => "Album";

        public override (string, SortOrder)[] OrderBy =>
            new (string, SortOrder)[] {(ItemSortBy.Album, SortOrder.Ascending)};

        public override IEnumerable<BaseItem> Order(IEnumerable<BaseItem> items)
        {
            return items.OrderBy(x => x.Album);
        }
    }


    public class ArtistLimitOrder : LimitOrder
    {
        public override string Name => "Artist";

        public override (string, SortOrder)[] OrderBy => new (string, SortOrder)[]
            {(ItemSortBy.Artist, SortOrder.Ascending)};

        public override IEnumerable<BaseItem> Order(IEnumerable<BaseItem> items)
        {
            return items.OrderBy(x => x, new ArtistsComparer(a => a.Artists));
        }
    }

    public class AlbumArtistLimitOrder : LimitOrder
    {
        public override string Name => "Album artist";

        public override (string, SortOrder)[] OrderBy => new (string, SortOrder)[]
            {(ItemSortBy.AlbumArtist, SortOrder.Ascending)};

        public override IEnumerable<BaseItem> Order(IEnumerable<BaseItem> items)
        {
            return items.OrderBy(x => x, new ArtistsComparer(a => a.AlbumArtists));
        }
    }

    public class MostFavoriteLimitOrder : LimitOrder
    {
        public override string Name => "Most favorite";

        public override (string, SortOrder)[] OrderBy => new (string, SortOrder)[]
            {(ItemSortBy.IsFavorite, SortOrder.Descending)};

        public override IEnumerable<BaseItem> Order(IEnumerable<BaseItem> items)
        {
            return items.OrderByDescending(x => x.IsFavorite);
        }
    }

    public class LessFavoriteLimitOrder : LimitOrder
    {
        public override string Name => "Less favorite";

        public override (string, SortOrder)[] OrderBy => new (string, SortOrder)[]
            {(ItemSortBy.IsFavorite, SortOrder.Ascending)};

        public override IEnumerable<BaseItem> Order(IEnumerable<BaseItem> items)
        {
            return items.OrderBy(x => x.IsFavorite);
        }
    }


    public class AddedDateDescLimitOrder : LimitOrder
    {
        public override string Name => "Added date desc";

        public override (string, SortOrder)[] OrderBy => new (string, SortOrder)[]
            {(ItemSortBy.DateCreated, SortOrder.Descending)};

        public override IEnumerable<BaseItem> Order(IEnumerable<BaseItem> items)
        {
            return items.OrderByDescending(x => x.DateCreated);
        }
    }

    public class AddedDateAscLimitOrder : LimitOrder
    {
        public override string Name => "Added date asc";

        public override (string, SortOrder)[] OrderBy => new (string, SortOrder)[]
            {(ItemSortBy.DateCreated, SortOrder.Ascending)};

        public override IEnumerable<BaseItem> Order(IEnumerable<BaseItem> items)
        {
            return items.OrderBy(x => x.DateCreated);
        }
    }

    public class MostPlayedLimitOrder : LimitOrder
    {
        public override string Name => "Most played";

        public override (string, SortOrder)[] OrderBy => new (string, SortOrder)[]
            {(ItemSortBy.PlayCount, SortOrder.Descending)};

        public override IEnumerable<BaseItem> Order(IEnumerable<BaseItem> items)
        {
            return items.OrderByDescending(x => x.PlayCount);
        }
    }

    public class LeastPlayedLimitOrder : LimitOrder
    {
        public override string Name => "Least played";

        public override (string, SortOrder)[] OrderBy => new (string, SortOrder)[]
            {(ItemSortBy.PlayCount, SortOrder.Ascending)};

        public override IEnumerable<BaseItem> Order(IEnumerable<BaseItem> items)
        {
            return items.OrderBy(x => x.PlayCount);
        }
    }

    public class PlayedDateDescLimitOrder : LimitOrder
    {
        public override string Name => "Played date desc";

        public override (string, SortOrder)[] OrderBy => new (string, SortOrder)[]
            {(ItemSortBy.DatePlayed, SortOrder.Descending)};

        public override IEnumerable<BaseItem> Order(IEnumerable<BaseItem> items)
        {
            return items.OrderByDescending(x => x.LastPlayedDate);
        }
    }

    public class PlayedDateAscLimitOrder : LimitOrder
    {
        public override string Name => "Played date asc";

        public override (string, SortOrder)[] OrderBy => new (string, SortOrder)[]
            {(ItemSortBy.DatePlayed, SortOrder.Ascending)};

        public override IEnumerable<BaseItem> Order(IEnumerable<BaseItem> items)
        {
            return items.OrderBy(x => x.LastPlayedDate);
        }
    }

    public class NameLimitOrder : LimitOrder
    {
        public override string Name => "Name";

        public override (string, SortOrder)[] OrderBy =>
            new (string, SortOrder)[] {(ItemSortBy.Name, SortOrder.Ascending)};

        public override IEnumerable<BaseItem> Order(IEnumerable<BaseItem> items)
        {
            return items.OrderBy(x => x.Name);
        }
    }

    public class EpisodeLimitOrder : LimitOrder
    {
        public override string Name => "Episode";

        public override (string, SortOrder)[] OrderBy =>
            new (string, SortOrder)[] {(ItemSortBy.AiredEpisodeOrder, SortOrder.Ascending)};

        public override IEnumerable<BaseItem> Order(IEnumerable<BaseItem> items)
        {
            return items.OrderBy(x => x, new EpisodeComparer());
        }
    }

    public class SortNameLimitOrder : LimitOrder
    {
        public override string Name => "SortName asc";

        public override (string, SortOrder)[] OrderBy =>
            new (string, SortOrder)[] {(ItemSortBy.SortName, SortOrder.Ascending)};

        public override IEnumerable<BaseItem> Order(IEnumerable<BaseItem> items)
        {
            return items.OrderBy(x => x.SortName);
        }
    }

    public class SortNameDescLimitOrder : LimitOrder
    {
        public override string Name => "SortName desc";

        public override (string, SortOrder)[] OrderBy =>
            new (string, SortOrder)[] {(ItemSortBy.SortName, SortOrder.Descending)};

        public override IEnumerable<BaseItem> Order(IEnumerable<BaseItem> items)
        {
            return items.OrderByDescending(x => x.SortName);
        }
    }

    public class ReleaseDateLimitOrder : LimitOrder
    {
        public override string Name => "Release date asc";

        public override (string, SortOrder)[] OrderBy =>
            new (string, SortOrder)[] {(ItemSortBy.PremiereDate, SortOrder.Ascending)};

        public override IEnumerable<BaseItem> Order(IEnumerable<BaseItem> items)
        {
            return items.OrderBy(x => x, new ReleaseDateComparer());
        }
    }

    public class ReleaseDateDescLimitOrder : LimitOrder
    {
        public override string Name => "Release date desc";

        public override (string, SortOrder)[] OrderBy =>
            new (string, SortOrder)[] {(ItemSortBy.PremiereDate, SortOrder.Descending)};

        public override IEnumerable<BaseItem> Order(IEnumerable<BaseItem> items)
        {
            return items.OrderByDescending(x => x, new ReleaseDateComparer());
        }
    }

    public class DurationLimitOrder : LimitOrder
    {
        public override string Name => "Duration asc";

        public override (string, SortOrder)[] OrderBy =>
            new (string, SortOrder)[] { (ItemSortBy.Runtime, SortOrder.Ascending) };

        public override IEnumerable<BaseItem> Order(IEnumerable<BaseItem> items)
        {
            return items.OrderBy(x => x.RunTimeTicks);
        }
    }

    public class DurationDescLimitOrder : LimitOrder
    {
        public override string Name => "Duration desc";

        public override (string, SortOrder)[] OrderBy =>
            new (string, SortOrder)[] { (ItemSortBy.Runtime, SortOrder.Descending) };

        public override IEnumerable<BaseItem> Order(IEnumerable<BaseItem> items)
        {
            return items.OrderByDescending(x => x.RunTimeTicks);
        }
    }
}