using Contracts;
using NLog;

namespace LoggerService
{
    public class LoggerManager : ILoggerManager
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();
       
        public LoggerManager()
        {
        }
        public void LogDebug(string msg)
        {
            logger.Debug(msg);
        }

        public void LogError(string msg)
        {
            logger.Error(msg);
        }

        public void LogInfo(string msg)
        {
            logger.Info(msg);
        }

        public void LogWarn(string msg)
        {
            logger.Warn(msg);
        }
    }
}
