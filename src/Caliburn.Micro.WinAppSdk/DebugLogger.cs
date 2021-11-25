using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caliburn.Micro
{
    public class DebugLogger : ILog
    {
        #region Constants
        private const string ErrorText = "ERROR";
        private const string WarnText = "WARN";
        private const string InfoText = "INFO";
        #endregion

        #region Fields
        private readonly Type _type;
        #endregion

        #region Constructors
        public DebugLogger(Type type)
        {
            _type = type;
        }
        #endregion

        #region Helper Methods
        private string CreateLogMessage(string format, params object[] args)
        {
            return string.Format("[{0}] {1}", DateTime.Now.ToString("o"), string.Format(format, args));
        }
        #endregion

        #region ILog Members
        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public void Error(Exception exception)
        {
            Debug.WriteLine(CreateLogMessage(exception.ToString()), ErrorText);
        }
        /// <summary>
        /// Logs the message as info.
        /// </summary>
        /// <param name="format">A formatted message.</param><param name="args">Parameters to be injected into the formatted message.</param>
        public void Info(string format, params object[] args)
        {
            Debug.WriteLine(CreateLogMessage(format, args), InfoText);
        }
        /// <summary>
        /// Logs the message as a warning.
        /// </summary>
        /// <param name="format">A formatted message.</param><param name="args">Parameters to be injected into the formatted message.</param>
        public void Warn(string format, params object[] args)
        {
            Debug.WriteLine(CreateLogMessage(format, args), WarnText);
        }
        #endregion

        #region Implementation of ILogExtended
        /// <summary>
        /// Logs the message as error.
        /// </summary>
        /// <param name="format">A formatted message.</param>
        /// <param name="args">Parameters to be injected into the formatted message.</param>
        public void Error(string format, params object[] args)
        {
            Debug.WriteLine(CreateLogMessage(format, args), ErrorText);
        }
        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="format">A formatted message.</param>
        /// <param name="args">Parameters to be injected into the formatted message.</param>
        public void Error(Exception exception, string format, params object[] args)
        {
            Debug.WriteLine(CreateLogMessage(format + " - Exception = " + exception.ToString(), args), ErrorText);
        }
        #endregion
    }
}
