using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caliburn.Micro.HelloWinRT.Logging
{
    public class DebugLog : ILog
    {
        public void Error(Exception exception)
        {
            Debug.WriteLine(exception.Message);
        }

        public void Info(string format, params object[] args)
        {
            Debug.WriteLine(format, args);
        }

        public void Warn(string format, params object[] args)
        {
            Debug.WriteLine(format, args);
        }
    }
}
