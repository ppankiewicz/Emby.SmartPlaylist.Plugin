using MediaBrowser.Controller.Entities.Audio;
using SmartPlaylist.Domain.Rule;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Domain.CriteriaDefinition.CriteriaDefinitions
{
    public class AlbumFavoriteCriteriaDefinition : FavoriteCriteriaDefinition
    {
        public override string Name => "Album Favorite";

        public override Value GetValue(UserItem item)
        {
            if (item.Item is Audio audio)
            {
                var album = audio.AlbumEntity;
                if (album != null) return base.GetValue(new UserItem(item.User, album));
            }

            return Value.None;
        }
    }
}