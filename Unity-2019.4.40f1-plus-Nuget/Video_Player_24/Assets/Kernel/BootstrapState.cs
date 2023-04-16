using System.IO;
using Kernel.Constants;
using Kernel.Scene;
using Kernel.StateMachine;
using Kernel.Videos;

namespace Kernel
{
    public class BootstrapState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly IVideoFactory _videoFactory;
        private readonly ISceneLoader _sceneLoader;

        public BootstrapState(IStateMachine stateMachine, IVideoFactory videoFactory, ISceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _videoFactory = videoFactory;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            foreach (string videoPath in Directory.GetFiles(MediaFolder.Videos))
                _videoFactory.Create(videoPath);

            _sceneLoader.Load(SceneName.Kernel);
            
            _stateMachine.EnterTo<VideoSyncState>();
        }

        public void Exit()
        {
        }
    }
}