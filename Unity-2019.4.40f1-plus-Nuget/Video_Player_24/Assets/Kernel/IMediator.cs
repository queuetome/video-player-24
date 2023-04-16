namespace Kernel
{
    public interface IMediator
    {
        void PlayNextVideo();
        void StopVideoPlaying();
        void SyncVideos();
        void DeleteVideos();
    }
}