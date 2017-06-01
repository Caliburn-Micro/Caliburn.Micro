#define DEBUG

namespace Caliburn.Micro {
    using System;
    using System.Diagnostics;

    /// <summary>
    ///   A simple logger thats logs everything to the debugger.
    /// </summary>
    public class DebugLog : ILog {
        private readonly string typeName;

        /// <summary>
        /// Initializes a new instance of the <see cref="DebugLog"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public DebugLog(Type type) {
            typeName = type.FullName;
        }

        /// <summary>
        /// Logs the message as info.
        /// </summary>
        /// <param name="format">A formatted message.</param>
        /// <param name="args">Parameters to be injected into the formatted message.</param>
        public void Info(string format, params object[] args) {
            Debug.WriteLine("[{1}] INFO: {0}", string.Format(format, args), typeName);
        }

        /// <summary>
        /// Logs the message as a warning.
        /// </summary>
        /// <param name="format">A formatted message.</param>
        /// <param name="args">Parameters to be injected into the formatted message.</param>
        public void Warn(string format, params object[] args) {
            Debug.WriteLine("[{1}] WARN: {0}", string.Format(format, args), typeName);
        }

        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public void Error(Exception exception) {
            Debug.WriteLine("[{1}] ERROR: {0}", exception, typeName);
        }
    }
}
