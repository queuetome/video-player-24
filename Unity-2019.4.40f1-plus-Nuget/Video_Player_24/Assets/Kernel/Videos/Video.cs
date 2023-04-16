using System.Collections.Generic;
using System.IO;

namespace Kernel.Videos
{
    public class Video
    {
        private readonly FileInfo _fileInfo;
        private readonly List<Video> _registration;
        private IVideoListener _listener;
        
        public string Name => _fileInfo.Name;
        public string Path => _fileInfo.FullName;
        public bool Listening => _listener != null;
        
        public Video(FileInfo fileInfo, List<Video> registration)
        {
            _fileInfo = fileInfo;
            _registration = registration;
        }
        
        public void SetListener(IVideoListener user)
        {
            _listener = user;
        }

        public void RemoveListener()
        {
            _listener = null;
        }

        public void Delete()
        {
            _registration.Remove(this);
            _listener?.OnVideoForcedClosing();
            _fileInfo.Delete();
        }
    }
}