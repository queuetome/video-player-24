using UnityEngine;

namespace Kernel.Logger
{
    public class DebugLogger : ILogger
    {
#if UNITY_EDITOR
        public void LogMessage(string text)
        {
            Debug.Log(text);
        }

        public void LogWarning(string text)
        {
            Debug.LogWarning(text);
        }
#endif
#if  !UNITY_EDITOR
        public void LogMessage(string text)
        {
        }
        public void LogWarning(string text)
        {
        }        
#endif
    }
}