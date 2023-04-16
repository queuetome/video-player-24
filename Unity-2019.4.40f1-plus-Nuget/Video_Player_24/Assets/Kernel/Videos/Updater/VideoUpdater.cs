using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kernel.Server;
using YandexDisk.Client.Protocol;

namespace Kernel.Videos.Updater
{
    public class VideoUpdater : IVideoUpdater
    {
        private readonly IVideoProvider _videoProvider;
        private readonly IServer _server;

        public VideoUpdater(IVideoProvider videoProvider, IServer server)
        {
            _videoProvider = videoProvider;
            _server = server;
        }

        public async void UpdateFromServer(ServerAuthorization authorization, CancellationToken token)
        {
            await _server.SignIn(authorization, token);
            await DownloadNew();
            DeleteOutdated();
            _server.SignOut();
        }

        private async Task DownloadNew()
        {
            IEnumerable<string> localNames = _videoProvider.All.Select(v => v.Name);
            
            IEnumerable<Resource> newResources = _server.Resources.Where(res => res.Type == ResourceType.File)
                .Where(res => localNames.Contains(res.Path.Replace("/", "_").Replace(":", "")) == false);
            
            foreach (Resource resource in newResources)
                await _server.DownloadFile(resource);
        }
        
        private void DeleteOutdated()
        {
            IEnumerable<string> remoteNames = _server.Resources
                .Select(res => res.Path.Replace("/", "_").Replace(":", ""));
            
            List<Video> outdated = _videoProvider.All
                .Where(v => remoteNames.Contains(v.Name) == false)
                .ToList();
            
            foreach (Video video in outdated)
                video.Delete();
        }
    }
}