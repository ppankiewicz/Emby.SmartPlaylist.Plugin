using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MediaBrowser.Model.Serialization;
using SmartPlaylist.Contracts;

namespace SmartPlaylist.Services.SmartPlaylist
{
    public class SmartPlaylistStore : ISmartPlaylistStore
    {
        private readonly ISmartPlaylistFileSystem _fileSystem;
        private readonly IJsonSerializer _jsonSerializer;

        public SmartPlaylistStore(IJsonSerializer jsonSerializer, ISmartPlaylistFileSystem fileSystem)
        {
            _jsonSerializer = jsonSerializer;
            _fileSystem = fileSystem;
        }


        public async Task<SmartPlaylistDto> GetSmartPlaylistAsync(Guid smartPlaylistId)
        {
            var fileName = _fileSystem.GetSmartPlaylistFilePath(smartPlaylistId);

            return await LoadPlaylistAsync(fileName).ConfigureAwait(false);
        }

        public async Task<SmartPlaylistDto[]> LoadPlaylistsAsync(Guid userId)
        {
            var deserializeTasks = _fileSystem.GetSmartPlaylistFilePaths(userId).Select(LoadPlaylistAsync).ToArray();

            await Task.WhenAll(deserializeTasks).ConfigureAwait(false);

            return deserializeTasks.Select(x => x.Result).ToArray();
        }

        public async Task<SmartPlaylistDto[]> GetAllSmartPlaylistsAsync()
        {
            var deserializeTasks = _fileSystem.GetAllSmartPlaylistFilePaths().Select(LoadPlaylistAsync).ToArray();

            await Task.WhenAll(deserializeTasks).ConfigureAwait(false);

            return deserializeTasks.Select(x => x.Result).ToArray();
        }

        public void Save(SmartPlaylistDto smartPlaylist)
        {
            var filePath = _fileSystem.GetSmartPlaylistPath(smartPlaylist.UserId, smartPlaylist.Id);
            _jsonSerializer.SerializeToFile(smartPlaylist, filePath);
        }

        public void Delete(Guid userId, string smartPlaylistId)
        {
            var filePath = _fileSystem.GetSmartPlaylistPath(userId, smartPlaylistId);
            if (File.Exists(filePath)) File.Delete(filePath);
        }

        private async Task<SmartPlaylistDto> LoadPlaylistAsync(string filePath)
        {
            using (var reader = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None, 4096,
                FileOptions.Asynchronous))
            {
                var res = await _jsonSerializer.DeserializeFromStreamAsync<SmartPlaylistDto>(reader)
                    .ConfigureAwait(false);
                reader.Close();
                return res;
            }
        }
    }
}