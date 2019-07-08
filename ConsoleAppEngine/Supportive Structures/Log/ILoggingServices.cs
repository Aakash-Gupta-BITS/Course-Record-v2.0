using MetroLog;
using System;

namespace ConsoleAppEngine.Log
{
    public interface ILoggingServices
    {
        void WriteLine<T>(string message, LogLevel logLevel = LogLevel.Trace, Exception exception = null);
    }
}
