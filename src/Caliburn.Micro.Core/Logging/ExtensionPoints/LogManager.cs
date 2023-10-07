using System;

namespace Caliburn.Micro
{
    /// <summary>
    /// Used to manage logging.
    /// </summary>
    public static class LogManager
    {
        private static readonly ILog NullLogInstance = new NullLog();

        /// <summary>
        /// Creates an <see cref="ILog"/> for the provided type.
        /// </summary>
        public static Func<Type, ILog> GetLog { get; set; }
            = type 
                => NullLogInstance;

        private sealed class NullLog : ILog
        {
            public void Info(string format, params object[] args) { }
            public void Warn(string format, params object[] args) { }
            public void Error(Exception exception) { }
        }
    }
}
