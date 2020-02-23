using System;
using System.Linq;

namespace SmartPlaylist.Services
{
    public class UserPlaylistInfo
    {
        public UserPlaylistInfo(Guid userId, PlaylistInfo[] playlistInfos)
        {
            UserId = userId;
            PlaylistInfos = playlistInfos;
        }
        public UserPlaylistInfo(Guid userId, string playlistName, string mediaType = null)
        {
            UserId = userId;
            PlaylistInfos = new[]{new PlaylistInfo(playlistName, mediaType) };
        }

        public UserPlaylistInfo(Domain.SmartPlaylist smartPlaylist)
        {
            UserId = smartPlaylist.UserId;
            PlaylistInfos = new[] { new PlaylistInfo(smartPlaylist.Name, smartPlaylist.MediaType) };
        }

        public Guid UserId { get; }
        public PlaylistInfo[] PlaylistInfos { get;}

        public string[] PlaylistNames => PlaylistInfos.Select(x => x.PlaylistName).ToArray();

        public string FirstPlaylistName => PlaylistInfos.FirstOrDefault()?.PlaylistName ?? string.Empty;

        public PlaylistInfo[] FilterPlaylists(string[] playlistNames)
        {
            return  PlaylistInfos.Where(x=>playlistNames.Contains(x.PlaylistName)).ToArray();
        }
    }
}