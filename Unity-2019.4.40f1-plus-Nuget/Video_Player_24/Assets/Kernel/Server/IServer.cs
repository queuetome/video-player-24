using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using YandexDisk.Client.Protocol;

namespace Kernel.Server
{
    public interface IServer
    {
        IEnumerable<Resource> Resources { get; }
        Task SignIn(ServerAuthorization authorization, CancellationToken token);
        void SignOut();
        Task DownloadFile(Resource resource);
    }
}