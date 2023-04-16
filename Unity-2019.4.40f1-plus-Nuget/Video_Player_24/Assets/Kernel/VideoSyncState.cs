using System.Threading;
using Kernel.Configs;
using Kernel.Data;
using Kernel.StateMachine;
using Kernel.Videos.Updater;

namespace Kernel
{
    public class VideoSyncState : IState
    {
        private readonly IStaticData _staticData;
        private readonly IVideoUpdater _videoUpdater;
        private readonly CancellationTokenSource _cancellation;

        public VideoSyncState(IStaticData staticData, IVideoUpdater videoUpdater)
        {
            _staticData = staticData;
            _videoUpdater = videoUpdater;
            _cancellation = new CancellationTokenSource();
        }

        public void Enter()
        {
            ServerConfig serverConfig = _staticData.Get<ServerConfig>();
            _videoUpdater.UpdateFromServer(serverConfig.Authorization, _cancellation.Token);
        }

        public void Exit()
        {
            _cancellation.Cancel();
        }
    }
}