using System;
using NLog;

namespace Wsm.Logger
{
    public class Nlogger : Contracts.Logger.ILogger
    {
        private readonly NLog.Logger _logger;

        public Nlogger()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(Exception x)
        {
            Error(x.StackTrace);
        }

        public void Error(string message, Exception x)
        {
            Error(message, x);
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        public void Fatal(Exception x)
        {
            Fatal(x.StackTrace);
        }
    }
}
