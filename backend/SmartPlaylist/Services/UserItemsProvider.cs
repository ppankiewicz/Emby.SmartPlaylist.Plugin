using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaBrowser.Controller.Dto;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Library;

namespace SmartPlaylist.Services
{
    public interface IUserItemsProvider
    {
        IEnumerable<BaseItem> GetItems(User user, string[] mediaTypes);
    }

    public class UserItemsProvider : IUserItemsProvider
    {
        private readonly ILibraryManager _libraryManager;

        public UserItemsProvider(ILibraryManager libraryManager)
        {
            _libraryManager = libraryManager;
        }

        public IEnumerable<BaseItem> GetItems(User user, string[] mediaTypes)
        {
            var items = new ConcurrentBag<(int, IEnumerable<BaseItem>)>();
            var count = GetItemsCount(user, mediaTypes);
            var partsCount = (int) Math.Ceiling((decimal) count / Const.MaxGetUserItemsCount);
            Parallel.ForEach(Enumerable.Range(0, partsCount), new ParallelOptions
            {
                MaxDegreeOfParallelism = Const.ForEachMaxDegreeOfParallelism
            }, x =>
            {
                var startIndex = x * Const.MaxGetUserItemsCount;
                var partItems = GetItems(user, mediaTypes, Const.MaxGetUserItemsCount, startIndex);
                items.Add((x, partItems));
            });

            return items.OrderBy(x => x.Item1).SelectMany(x => x.Item2);
        }

        private BaseItem[] GetItems(User user, string[] mediaTypes, int? limit = null, int? startIndex = null)
        {
            var items = _libraryManager.GetUserRootFolder().GetItems(new InternalItemsQuery(user)
            {
                Recursive = true,
                Limit = limit,
                StartIndex = startIndex,
                IncludeItemTypes = mediaTypes
            });

            return items.Items;
        }


        private int GetItemsCount(User user, string[] itemTypes)
        {
            var query = new InternalItemsQuery(user)
            {
                IncludeItemTypes = itemTypes,
                Limit = 0,
                Recursive = true,
                IsVirtualItem = false,
                DtoOptions = new DtoOptions(false)
                {
                    EnableImages = false
                }
            };

            var result = _libraryManager.GetItemsResult(query);
            return result.TotalRecordCount;
        }
    }
}