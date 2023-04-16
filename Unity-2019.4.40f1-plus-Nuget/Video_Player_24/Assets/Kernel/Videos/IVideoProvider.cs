using System.Collections.Generic;

namespace Kernel.Videos
{
    public interface IVideoProvider
    {
        List<Video> All { get; }
    }
}