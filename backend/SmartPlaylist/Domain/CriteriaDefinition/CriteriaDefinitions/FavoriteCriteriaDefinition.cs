using SmartPlaylist.Domain.Rule;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Domain.CriteriaDefinition.CriteriaDefinitions
{
    public class FavoriteCriteriaDefinition : CriteriaDefinition
    {
        private static readonly ListValue FavoriteListValue = ListValue.Create("Favorite");
        private static readonly ListValue LikedListValue = ListValue.Create("Liked");
        private static readonly ListValue NoneListValue = ListValue.Create("None");
        public override string Name => "Favorite";
        public override CriteriaDefinitionType Type => ListValueDefinitionType.Instance;

        public override Value[] Values
        {
            get { return new Value[] {FavoriteListValue, LikedListValue, NoneListValue}; }
        }

        public override Value GetValue(UserItem item)
        {
            if (item.TryGetUserItemData(out var userData))
            {
                if (userData.IsFavorite) return FavoriteListValue;

                if (userData.Likes.GetValueOrDefault()) return LikedListValue;
            }

            return NoneListValue;
        }
    }
}