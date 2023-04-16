using System.Threading;
using Kernel.Server;

namespace Kernel.Videos.Updater
{
    public interface IVideoUpdater
    {
        void UpdateFromServer(ServerAuthorization authorization, CancellationToken token);
    }
}