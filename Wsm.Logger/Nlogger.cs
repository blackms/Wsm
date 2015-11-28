using System;
using NLog;

namespace Wsm.Logger
{
    public class NLogger : Contracts.Logger.ILogger
    {
        private readonly NLog.Logger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="NLogger"/> class.
        /// </summary>
        public NLogger()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// Informations the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Info(string message)
        {
            _logger.Info(message);
        }

        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Error(string message)
        {
            _logger.Error(message);
        }

        /// <summary>
        /// Errors the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        public void Error(Exception x)
        {
            Error(x.StackTrace);
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="x">The x.</param>
        public void Error(string message, Exception x)
        {
            Error(message, x);
        }

        /// <summary>
        /// Fatals the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        /// <summary>
        /// Fatals the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        public void Fatal(Exception x)
        {
            Fatal(x.StackTrace);
        }
    }
}
