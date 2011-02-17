namespace GameLibrary.Framework {
    using System;
    using System.Diagnostics;
    using Caliburn.Micro;

    public class SimpleLog : ILog {
        readonly Type type;

        public SimpleLog(Type type) {
            this.type = type;
        }

        public void Info(string format, params object[] args)
        {
            Debug.WriteLine("INFO: {0} : {1}", type.Name, string.Format(format, args));
        }

        public void Warn(string format, params object[] args)
        {
            Debug.WriteLine("WARN: {0} : {1}", type.Name, string.Format(format, args));
        }

        public void Error(Exception exception) {
            Debug.WriteLine("ERROR: {0}\n{1}", type.Name, exception);
        }
    }
}