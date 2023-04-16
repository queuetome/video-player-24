using System.Collections.Generic;

namespace Kernel.Videos
{
    public class VideoProvider : IVideoProvider
    {
        public List<Video> All { get; } = new List<Video>();
    }
}