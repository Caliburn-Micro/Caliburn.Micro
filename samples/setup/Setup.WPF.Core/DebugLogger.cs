using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Serilog;

namespace Setup.WPF.Core
{
    class DebugLogger : ILog
    {

        private readonly Type _type;
        public DebugLogger(Type type)
        {
            _type = type;
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File("myapp.txt")
                .CreateLogger();
        }

        private string CreateLogMessage(string format, params object[] args)
        {
            return string.Format("[{0}] {1}",
            DateTime.Now.ToString("o"),
            string.Format(format, args));
        }
        public void Error(Exception exception)
        {
            Log.Error(CreateLogMessage(exception.ToString()), "ERROR");
        }

        public void Info(string format, params object[] args)
        {
            Log.Information(CreateLogMessage(format, args), "INFO");
        }

        public void Warn(string format, params object[] args)
        {
            Log.Warning(CreateLogMessage(format, args), "WARN");
        }
    }
}
