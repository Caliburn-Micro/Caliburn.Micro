namespace Caliburn.Micro {
    using System;

    /// <summary>
    /// Used to manage logging.
    /// </summary>
    public static class LogManager {
        static readonly ILog NullLogInstance = new NullLog();

        /// <summary>
        /// Creates an <see cref="ILog"/> for the provided type.
        /// </summary>
        public static Func<Type, ILog> GetLog = type => NullLogInstance;

        class NullLog : ILog {
            public void Debug(string format, params object[] args) { }
            public void Info(string format, params object[] args) { }
            public void Warn(string format, params object[] args) { }
            public void Error(Exception exception) { }
            public void Error(Exception exception, string format, params object[] args) { }
            public void Error(string format, params object[] args) { }
        }
    }
}
