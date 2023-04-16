using System.IO;
using static System.Environment;
using static System.Environment.SpecialFolder;
using static System.IO.Directory;
using static System.IO.File;
using static System.IO.Path;

namespace Kernel.Constants
{
    public static class ApplicationPersistent
    {
#if !UNITY_EDITOR
        public static readonly string DataFolder = UnityEngine.Application.persistentDataPath;
#endif
#if UNITY_EDITOR
        public static string DataFolder => 
            CreateDirectory(Combine(GetFolderPath(Desktop), "Video_Player_24_Data")).FullName;
#endif
    }
}