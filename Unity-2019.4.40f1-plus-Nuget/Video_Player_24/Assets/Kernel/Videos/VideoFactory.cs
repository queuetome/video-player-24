using System.IO;

namespace Kernel.Videos
{
    public class VideoFactory : IVideoFactory
    {
        private readonly IVideoProvider _videoProvider;

        public VideoFactory(IVideoProvider videoProvider)
        {
            _videoProvider = videoProvider;
        }

        public Video Create(string filePath)
        {
            Video video = new Video(new FileInfo(filePath), _videoProvider.All);
            _videoProvider.All.Add(video);
            return video;
        }
    }
}