using Kernel.Scene.VideoPlayer;
using Kernel.StateMachine;
using Tools;
using UnityEngine;
using Zenject;

namespace Kernel
{
    public class Mediator : MonoBehaviour, IMediator
    {
        [SerializeField] private LoopedVideoPlayer _loopedVideoPlayer;
        [Inject] private IStateMachine _stateMachine;

        [Button] public void PlayNextVideo()    => _loopedVideoPlayer.PlayNext();
        [Button] public void StopVideoPlaying() => _loopedVideoPlayer.Stop();
        [Button] public void SyncVideos()       => _stateMachine.EnterTo<VideoSyncState>();
        [Button] public void DeleteVideos()     => _stateMachine.EnterTo<VideoDeleteState>();
    }
}