namespace Kernel.Videos
{
    public interface IVideoFactory
    {
        Video Create(string filePath);
    }
}