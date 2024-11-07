#define DEBUG

using System;
using System.Diagnostics;

namespace Caliburn.Micro
{
    /// <summary>
    ///   A simple logger thats logs everything to the debugger.
    /// </summary>
    public class DebugLog : ILog
    {
        private readonly string typeName;

        /// <summary>
        /// Initializes a new instance of the <see cref="DebugLog"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public DebugLog(Type type)
        {
            typeName = type.FullName;
        }

        /// <summary>
        /// Logs the message as info.
        /// </summary>
        /// <param name="format">A formatted message.</param>
        /// <param name="args">Parameters to be injected into the formatted message.</param>
        public void Info(string format, params object[] args)
        {
            Trace.WriteLine($"[{typeName}] INFO: {string.Format(format, args)}");
        }

        /// <summary>
        /// Logs the message as a warning.
        /// </summary>
        /// <param name="format">A formatted message.</param>
        /// <param name="args">Parameters to be injected into the formatted message.</param>
        public void Warn(string format, params object[] args)
        {
            Trace.WriteLine($"[{typeName}] WARN: {string.Format(format, args)}");
        }

        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public void Error(Exception exception)
        {
            Trace.WriteLine($"[{typeName}] ERROR: {exception}" );
        }
    }
}
