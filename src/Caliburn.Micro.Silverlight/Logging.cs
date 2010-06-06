namespace Caliburn.Micro
{
    using System;

    public interface ILog
    {
        void Info(string message);
        void Warn(string message);
        void Error(Exception exception);
        void Error(string message, Exception exception);
    }

    public static class LogManager
    {
        static readonly ILog nullLog = new NullLog();
        public static Func<Type, ILog> GetLog = type => nullLog;

        private class NullLog : ILog
        {
            public void Info(string message) { }
            public void Warn(string message) { }
            public void Error(Exception exception) { }
            public void Error(string message, Exception exception) { }
        }
    }

    public static class LogExtensions
    {
        public static void Info(this ILog log, string format, params object[] args)
        {
            log.Info(string.Format(format, args));
        }

        public static void Warn(this ILog log, string format, params object[] args)
        {
            log.Info(string.Format(format, args));
        }
    }
}