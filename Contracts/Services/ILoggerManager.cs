namespace Contracts
{
    public interface ILoggerManager
    {
        void LogInfo(string msg);
        void LogWarn(string msg);
        void LogDebug(string msg);
        void LogError(string msg);
    }
}
