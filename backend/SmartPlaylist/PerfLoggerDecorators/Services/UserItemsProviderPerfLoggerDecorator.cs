using System.Collections.Generic;
using System.Linq;
using MediaBrowser.Controller.Entities;
using SmartPlaylist.Infrastructure;
using SmartPlaylist.Services;

namespace SmartPlaylist.PerfLoggerDecorators.Services
{
    public class UserItemsProviderPerfLoggerDecorator : IUserItemsProvider
    {
        private readonly IUserItemsProvider _decorated;

        public UserItemsProviderPerfLoggerDecorator(IUserItemsProvider decorated)
        {
            _decorated = decorated;
        }

        public IEnumerable<BaseItem> GetItems(User user, string[] mediaTypes)
        {
            var items = new BaseItem[0];
            using (PerfLogger.Create("GetUserItems", () => new {userName = user.Name, itemsCount = items.Length}))
            {
                items = _decorated.GetItems(user, mediaTypes).ToArray();
                return items;
            }
        }
    }
}