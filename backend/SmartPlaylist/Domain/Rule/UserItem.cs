using MediaBrowser.Controller.Entities;

namespace SmartPlaylist.Domain.Rule
{
    public class UserItem
    {
        public UserItem(User user, BaseItem item)
        {
            User = user;
            Item = item;
        }

        public User User { get; }
        public BaseItem Item { get; }

        public bool TryGetUserItemData(out UserItemData userItemData)
        {
            userItemData = BaseItem.UserDataManager.GetUserData(User, Item);
            return userItemData != null;
        }
    }
}