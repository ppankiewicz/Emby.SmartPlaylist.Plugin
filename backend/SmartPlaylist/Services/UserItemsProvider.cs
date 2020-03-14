using System.Collections.Generic;
using MediaBrowser.Controller.Dto;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Library;

namespace SmartPlaylist.Services
{
    public interface IUserItemsProvider
    {
        IEnumerable<BaseItem> GetItems(User user, string[] itemTypes);
    }

    public class UserItemsProvider : IUserItemsProvider
    {
        private readonly ILibraryManager _libraryManager;

        public UserItemsProvider(ILibraryManager libraryManager)
        {
            _libraryManager = libraryManager;
        }

        public IEnumerable<BaseItem> GetItems(User user, string[] itemTypes)
        {
            var query = GetItemsQuery(user, itemTypes);
            var items = _libraryManager.GetUserRootFolder().GetItems(query).Items;

            return items;
        }

        private static InternalItemsQuery GetItemsQuery(User user, string[] itemTypes)
        {
            return new InternalItemsQuery(user)
            {
                IncludeItemTypes = itemTypes,
                Recursive = true,
                IsVirtualItem = false,
                DtoOptions = new DtoOptions(true)
                {
                    EnableImages = false
                }
            };
        }
    }
}