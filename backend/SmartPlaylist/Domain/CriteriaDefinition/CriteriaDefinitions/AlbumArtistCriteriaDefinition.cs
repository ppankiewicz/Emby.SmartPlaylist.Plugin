using System.Linq;
using MediaBrowser.Controller.Entities.Audio;
using SmartPlaylist.Domain.Rule;
using SmartPlaylist.Domain.Values;

namespace SmartPlaylist.Domain.CriteriaDefinition.CriteriaDefinitions
{
    public class AlbumArtistCriteriaDefinition : CriteriaDefinition
    {
        public override string Name => "Album Artist";
        public override CriteriaDefinitionType Type => StringDefinitionType.Instance;

        public override Value GetValue(UserItem item)
        {
            if (item.Item is Audio audio)
                return ArrayValue<StringValue>.Create(audio.AlbumArtists.Select(StringValue.Create).ToArray());
            return Value.None;
        }
    }
}