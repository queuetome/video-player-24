using Kernel.Videos;
using UnityEngine;
using UnityEngine.Video;

namespace Kernel.Infrastructure.UnityVideoPlayer
{
    public class VideoPlayerFacade : MonoBehaviour, IVideoListener
    {
        [SerializeField] private VideoPlayer _videoPlayer;
        public Video CurrentVideo { get; private set; }
        public bool Playing { get; private set; }

        private void Update()
        {
            if (Playing && _videoPlayer.isPlaying == false && _videoPlayer.frame > 0)
                StopPlaying();
        }

        private void OnDestroy()
        {
            StopPlaying();
            ClearOutputTexture();
        }

        public void Play(Video video)
        {
            CurrentVideo = video;
            CurrentVideo.SetListener(this);
            
            if (_videoPlayer.isPrepared == false)
                _videoPlayer.Prepare();
            
            _videoPlayer.url = CurrentVideo.Path;
            _videoPlayer.Play();
            Playing = true;
        }

        public void StopPlaying()
        {
            _videoPlayer.Stop();
            CurrentVideo?.RemoveListener();
            Playing = false;
        }

        public void OnVideoForcedClosing()
        {
            StopPlaying();
        }

        public void ClearOutputTexture()
        {
            RenderTexture buffer = RenderTexture.active;
            RenderTexture.active = _videoPlayer.targetTexture;
            GL.Clear(true, true, Color.clear);
            RenderTexture.active = buffer;
        }
    }
}