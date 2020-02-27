using MediaBrowser.Controller.Entities;

namespace SmartPlaylist.Domain
{
    public class UserPlaylist
    {
        public UserPlaylist(User user, string playlistName)
        {
            User = user;
            Name = playlistName;
        }

        public string Name { get; }
        public User User { get; }

        public virtual BaseItem[] GetItems()
        {
            return new BaseItem[0];
        }
    }
}