using Kernel.StateMachine;
using Kernel.Videos;

namespace Kernel
{
    public class VideoDeleteState : IState
    {
        private readonly IVideoProvider _videoProvider;

        public VideoDeleteState(IVideoProvider videoProvider)
        {
            _videoProvider = videoProvider;
        }

        public void Enter()
        {
            for (int index = _videoProvider.All.Count - 1; index >= 0; index--)
            {
                Video video = _videoProvider.All[index];
                _videoProvider.All.Remove(video);
                video.Delete();
            }
        }

        public void Exit()
        {
        }
    }
}