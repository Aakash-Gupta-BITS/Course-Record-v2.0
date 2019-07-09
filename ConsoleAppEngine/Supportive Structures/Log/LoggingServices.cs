using MetroLog;
using MetroLog.Targets;
using System;

namespace ConsoleAppEngine.Log
{
    public class LoggingServices : ILoggingServices
    {
        #region Properties
        public static LoggingServices Instance { get; }

        public static int RetainDays { get; } = 10;

        public static bool Enabled { get; set; } = true;
        #endregion

        static LoggingServices()
        {
            Instance = Instance ?? new LoggingServices();
            LogManagerFactory.DefaultConfiguration.AddTarget(LogLevel.Trace, LogLevel.Fatal, new StreamingFileTarget { RetainDays = RetainDays });
        }

        public void WriteLine<T>(string message, LogLevel logLevel = LogLevel.Trace, Exception exception = null)
        {
            if (Enabled)
            {
                var logger = LogManagerFactory.DefaultLogManager.GetLogger<T>();
                if (logLevel == LogLevel.Trace && logger.IsTraceEnabled)
                {
                    logger.Trace(message);
                }
                else if (logLevel == LogLevel.Debug && logger.IsDebugEnabled)
                {
                    logger.Debug(message);
                }
                else if (logLevel == LogLevel.Error && logger.IsErrorEnabled)
                {
                    logger.Error(message);
                }
                else if (logLevel == LogLevel.Fatal && logger.IsFatalEnabled)
                {
                    logger.Fatal(message);
                }
                else if (logLevel == LogLevel.Info && logger.IsInfoEnabled)
                {
                    logger.Info(message);
                }
                else if (logLevel == LogLevel.Warn && logger.IsWarnEnabled)
                {
                    logger.Warn(message);
                }
            }
        }
    }
}
