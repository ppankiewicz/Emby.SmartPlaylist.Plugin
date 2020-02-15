using System;
using System.Linq;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.Querying;
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
        public static LimitOrder[] All = typeof(LimitOrder).Assembly.FindAndCreateDerivedTypes<LimitOrder>().ToArray();

        public static string[] AllNames = All.Select(x => x.Name).ToArray();
    }

    public abstract class LimitOrder
    {
        public abstract string Name { get; }

        public virtual ValueTuple<string, SortOrder>[] OrderBy => new (string, SortOrder)[0];
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
    }


    public class ArtistLimitOrder : LimitOrder
    {
        public override string Name => "Artist";

        public override (string, SortOrder)[] OrderBy => new (string, SortOrder)[]
            {(ItemSortBy.Artist, SortOrder.Ascending)};
    }

    public class AlbumArtistLimitOrder : LimitOrder
    {
        public override string Name => "Album artist";

        public override (string, SortOrder)[] OrderBy => new (string, SortOrder)[]
            {(ItemSortBy.AlbumArtist, SortOrder.Ascending)};
    }

    public class MostFavoriteLimitOrder : LimitOrder
    {
        public override string Name => "Most favorite";

        public override (string, SortOrder)[] OrderBy => new (string, SortOrder)[]
            {(ItemSortBy.IsFavorite, SortOrder.Descending)};
    }

    public class LessFavoriteLimitOrder : LimitOrder
    {
        public override string Name => "Less favorite";

        public override (string, SortOrder)[] OrderBy => new (string, SortOrder)[]
            {(ItemSortBy.IsFavorite, SortOrder.Ascending)};
    }


    public class AddedDateDescLimitOrder : LimitOrder
    {
        public override string Name => "Added date desc";

        public override (string, SortOrder)[] OrderBy => new (string, SortOrder)[]
            {(ItemSortBy.DateCreated, SortOrder.Descending)};
    }

    public class AddedDateAscLimitOrder : LimitOrder
    {
        public override string Name => "Added date asc";

        public override (string, SortOrder)[] OrderBy => new (string, SortOrder)[]
            {(ItemSortBy.DateCreated, SortOrder.Ascending)};
    }

    public class MostPlayedLimitOrder : LimitOrder
    {
        public override string Name => "Most played";

        public override (string, SortOrder)[] OrderBy => new (string, SortOrder)[]
            {(ItemSortBy.PlayCount, SortOrder.Descending)};
    }

    public class LeastPlayedLimitOrder : LimitOrder
    {
        public override string Name => "Least played";

        public override (string, SortOrder)[] OrderBy => new (string, SortOrder)[]
            {(ItemSortBy.PlayCount, SortOrder.Ascending)};
    }

    public class PlayedDateDescLimitOrder : LimitOrder
    {
        public override string Name => "Played date desc";

        public override (string, SortOrder)[] OrderBy => new (string, SortOrder)[]
            {(ItemSortBy.DatePlayed, SortOrder.Descending)};
    }

    public class PlayedDateAscLimitOrder : LimitOrder
    {
        public override string Name => "Played date asc";

        public override (string, SortOrder)[] OrderBy => new (string, SortOrder)[]
            {(ItemSortBy.DatePlayed, SortOrder.Ascending)};
    }

    public class NameLimitOrder : LimitOrder
    {
        public override string Name => "Name";

        public override (string, SortOrder)[] OrderBy =>
            new (string, SortOrder)[] {(ItemSortBy.Name, SortOrder.Ascending)};
    }
}