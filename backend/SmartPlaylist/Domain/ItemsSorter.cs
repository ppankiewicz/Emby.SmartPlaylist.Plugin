using System.Collections.Generic;
using System.Linq;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.Querying;
using SmartPlaylist.Comparers;

namespace SmartPlaylist.Domain
{
    public static class ItemsSorter
    {
        public static IEnumerable<BaseItem> OrderItems((string, SortOrder)[] orderBy, IEnumerable<BaseItem> items)
        {
            orderBy.ToList().ForEach(x => items = OrderItems(items, x));
            return items;
        }

        private static IEnumerable<BaseItem> OrderItems(IEnumerable<BaseItem> items, (string, SortOrder) orderTuple)
        {
            var (orderBy, orderType) = orderTuple;
            switch (orderBy)
            {
                case ItemSortBy.IsFavorite:
                {
                    return orderType == SortOrder.Ascending
                        ? items.OrderBy(x => x.IsFavorite)
                        : items.OrderByDescending(x => x.IsFavorite);
                }
                case ItemSortBy.PlayCount:
                {
                    return orderType == SortOrder.Ascending
                        ? items.OrderBy(x => x.PlayCount)
                        : items.OrderByDescending(x => x.PlayCount);
                }
                case ItemSortBy.Album:
                {
                    return orderType == SortOrder.Ascending
                        ? items.OrderBy(x => x.Album)
                        : items.OrderByDescending(x => x.Album);
                }
                case ItemSortBy.Artist:
                {
                    return orderType == SortOrder.Ascending
                        ? items.OrderBy(x => x, new ArtistsComparer(a => a.Artists))
                        : items.OrderByDescending(x => x, new ArtistsComparer(a => a.Artists));
                }
                case ItemSortBy.AlbumArtist:
                {
                    return orderType == SortOrder.Ascending
                        ? items.OrderBy(x => x, new ArtistsComparer(a => a.AlbumArtists))
                        : items.OrderByDescending(x => x, new ArtistsComparer(a => a.AlbumArtists));
                }
                case ItemSortBy.Name:
                {
                    return orderType == SortOrder.Ascending
                        ? items.OrderBy(x => x.Name)
                        : items.OrderByDescending(x => x.Name);
                }
                case ItemSortBy.DateCreated:
                {
                    return orderType == SortOrder.Ascending
                        ? items.OrderBy(x => x.DateCreated)
                        : items.OrderByDescending(x => x.DateCreated);
                }
                case ItemSortBy.DatePlayed:
                {
                    return orderType == SortOrder.Ascending
                        ? items.OrderBy(x => x.LastPlayedDate)
                        : items.OrderByDescending(x => x.LastPlayedDate);
                }
                case ItemSortBy.AiredEpisodeOrder:
                {
                    return orderType == SortOrder.Ascending
                        ? items.OrderBy(x => x, new EpisodeComparer())
                        : items.OrderByDescending(x => x, new EpisodeComparer());
                }
                default:
                    return items;
            }
        }
    }
}