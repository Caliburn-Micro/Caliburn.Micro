namespace Caliburn.Micro
{
    using System;

    public interface ILog
    {
        void Info(string format, params object[] args);
        void Warn(string format, params object[] args);
        void Error(Exception exception);
    }

    public static class LogManager
    {
        static readonly ILog nullLog = new NullLog();
        public static Func<Type, ILog> GetLog = type => nullLog;

        private class NullLog : ILog
        {
            public void Info(string format, params object[] args) { }
            public void Warn(string format, params object[] args) { }
            public void Error(Exception exception) { }
        }
    }
}