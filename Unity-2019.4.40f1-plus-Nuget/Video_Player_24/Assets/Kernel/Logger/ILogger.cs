namespace Kernel.Logger
{
    public interface ILogger
    {
        void LogMessage(string text);
        void LogWarning(string text);
    }
}