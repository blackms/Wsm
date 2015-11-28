using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wsm.Contracts.Logger;

namespace Wsm.Logger
{
    /// <summary>
    /// 
    /// </summary>
    public class Log4Net : ILogger
    {
        private log4net.ILog _log;

        public Log4Net(string loggerName)
        {
            _log = log4net.LogManager.GetLogger(loggerName);
        }

        /// <summary>
        /// Informations the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Info(string message)
        {
            _log.Info(message);
        }

        /// <summary>
        /// Warns the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Warn(string message)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Debug(string message)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Error(string message)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="x">The x.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Error(string message, Exception x)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Errors the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Error(Exception x)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Fatals the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Fatal(string message)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Fatals the specified x.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Fatal(Exception x)
        {
            throw new NotImplementedException();
        }
    }
}
