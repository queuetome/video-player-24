using System;

namespace Kernel.Scene
{
    public interface ISceneLoader
    {
        void Load(string sceneName, Action loadingCompleted = null);
    }
}