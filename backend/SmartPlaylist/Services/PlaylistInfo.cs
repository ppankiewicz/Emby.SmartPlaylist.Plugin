using System.Linq;

namespace SmartPlaylist.Services
{
    public class PlaylistInfo
    {
        public static PlaylistInfo[] Create(Domain.SmartPlaylist[] smartPlaylists)
        {
            return smartPlaylists.Select(x => new PlaylistInfo(x.Name, x.MediaType)).ToArray();
        }

        public PlaylistInfo(string playlistName, string mediaType = null)
        {
            PlaylistName = playlistName;
            MediaType = mediaType ?? MediaBrowser.Model.Entities.MediaType.Audio;
        }

        public string PlaylistName { get; }

        public string MediaType { get; }
    }
}