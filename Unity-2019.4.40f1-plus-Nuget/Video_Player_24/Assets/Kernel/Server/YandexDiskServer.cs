using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kernel.Constants;
using Kernel.Logger;
using Kernel.Videos;
using YandexDisk.Client;
using YandexDisk.Client.Http;
using YandexDisk.Client.Protocol;
using ResourceRequest = YandexDisk.Client.Protocol.ResourceRequest;

namespace Kernel.Server
{
    public class YandexDiskServer : IServer
    {
        private readonly ILogger _logger;
        private readonly IVideoFactory _videoFactory;
        private IDiskApi _diskApi;
        private CancellationToken _asyncToken;
        public IEnumerable<Resource> Resources { get; private set; }

        public YandexDiskServer(ILogger logger, IVideoFactory videoFactory)
        {
            _logger = logger;
            _videoFactory = videoFactory;
            Resources = Enumerable.Empty<Resource>();
        }

        public async Task SignIn(ServerAuthorization authorization, CancellationToken token)
        {
            _asyncToken = token;
            await TryOpenDisk(authorization);
        }

        public void SignOut()
        {
            CloseDisk();
            Resources = Enumerable.Empty<Resource>();
        }

        public async Task DownloadFile(Resource resource)
        {
            Link link = await TryGetDownloadLink(resource.Path);

            if (link == null)
                return;

            string filePath = Path.Combine(MediaFolder.Videos, resource.Path.Replace("/", "_").Replace(":", ""));

            try
            {
                _logger.LogMessage($"File {resource.Path} started downloading");
                Stream remoteFile = await _diskApi.Files.DownloadAsync(link, _asyncToken);

                using (FileStream newFile = new FileStream(filePath, FileMode.Create))
                {
                    await remoteFile.CopyToAsync(newFile);
                    _videoFactory.Create(filePath);
                    _logger.LogMessage($"File {resource.Name} has been created");
                }
            }
            catch
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    _logger.LogWarning($"File {resource.Name} has been deleted");
                }
                else
                {
                    _logger.LogWarning($"File {resource.Name} will not be created");
                }
            }
        }

        private async Task<bool> TryOpenDisk(ServerAuthorization authorization)
        {
            try
            {
                _diskApi = new DiskHttpApi(authorization.OAuthToken);
                
                ResourceRequest request = new ResourceRequest {Path = authorization.Directory};
                Resources = (await _diskApi.MetaInfo.GetInfoAsync(request, _asyncToken)).Embedded.Items;
                
                _logger.LogMessage($"Yandex disk has been opened by token: {authorization.OAuthToken}");
                _logger.LogMessage($"Total resources on Yandex disk: {Resources.ToArray().Length}");
                
                return true;
            }
            catch
            {
                _logger.LogWarning($"Yandex disk has not been opened.\nInput token: {authorization.OAuthToken}");
                return false;
            }
        }

        private void CloseDisk()
        {
            _diskApi.Dispose();
        }

        private async Task<Link> TryGetDownloadLink(string resourcePath)
        {
            try
            {
                return await _diskApi.Files.GetDownloadLinkAsync(resourcePath, _asyncToken);
            }
            catch
            {
                return null;
            }
        }
    }
}