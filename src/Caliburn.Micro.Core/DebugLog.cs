using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caliburn.Micro
{
    public class DebugLog : ILog
    {
        private const string ErrorText = "ERROR";
        private const string WarnText = "WARN";
        private const string InfoText = "INFO";
        private const string DebugText = "DEBUG";
        private readonly Type logType;

        public DebugLog(Type type)
        {
            logType = type;
        }
        private string CreateLogMessage(string format, params object[] args)
        {
            return string.Format("[{0}] {1}", DateTime.Now.ToString("o"), string.Format(format, args));
        }

        public void Error(Exception exception)
        {
            Trace.WriteLine(CreateLogMessage(exception.ToString()), ErrorText);
        }

        public void Info(string format, params object[] args)
        {
            Trace.WriteLine(CreateLogMessage(format, args), InfoText);
        }
        public void Warn(string format, params object[] args)
        {
            Trace.WriteLine(CreateLogMessage(format, args), WarnText);
        }

        public void Error(string format, params object[] args)
        {
            Trace.WriteLine(CreateLogMessage(format, args), ErrorText);
        }


        public void Debug(string format, params object[] args)
        {
            Trace.WriteLine(CreateLogMessage(format, args), DebugText);
        }

    }
}
