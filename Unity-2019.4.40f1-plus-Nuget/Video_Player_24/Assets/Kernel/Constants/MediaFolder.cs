using System.IO;
using static System.IO.Directory;
using static System.IO.Path;
using static Kernel.Constants.ApplicationPersistent;

namespace Kernel.Constants
{
    public static class MediaFolder
    {
        public static readonly string Videos = CreateDirectory(Combine(DataFolder, "Videos")).FullName;
    }
}