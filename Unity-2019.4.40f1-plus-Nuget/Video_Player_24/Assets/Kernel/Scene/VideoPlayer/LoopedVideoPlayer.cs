using System.Collections.Generic;
using System.Linq;
using Kernel.Infrastructure.UnityVideoPlayer;
using Kernel.Videos;
using UnityEngine;
using Zenject;

namespace Kernel.Scene.VideoPlayer
{
    public class LoopedVideoPlayer : MonoBehaviour
    {
        [SerializeField] private bool _looped = true;
        [SerializeField] private VideoPlayerFacade _videoPlayer;
        private IVideoProvider _videoProvider;

        [Inject]
        private void Construct(IVideoProvider videoProvider)
        {
            _videoProvider = videoProvider;
        }

        private void Update()
        {
            if (_looped && _videoPlayer.Playing == false)
                PlayNext();
        }

        public void PlayNext()
        {
            _looped = true;
            
            if (_videoPlayer.Playing)
                _videoPlayer.StopPlaying();
            
            List<Video> videos = _videoProvider.All
                .Where(v => v.Listening == false)
                .ToList();

            if (videos.Contains(_videoPlayer.CurrentVideo))
            {
                int currentIndex = videos.IndexOf(_videoPlayer.CurrentVideo);
                int nextIndex = (currentIndex + 1) % videos.Count;
                
                _videoPlayer.Play(videos.ElementAt(nextIndex));
            }
            else if (videos.Count > 0)
            {
                _videoPlayer.Play(videos.First());
            }
        }

        public void Stop()
        {
            _videoPlayer.StopPlaying();
            _videoPlayer.ClearOutputTexture();
            _looped = false;
        }
    }
}