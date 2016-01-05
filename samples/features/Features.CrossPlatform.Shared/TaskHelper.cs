using System;
using System.Threading;
using System.Threading.Tasks;

namespace Features.CrossPlatform
{
    /// <summary>
    /// Helper class to manage differences in the Task libraries between versions
    /// </summary>
    public static class TaskHelper
    {
#if SILVERLIGHT
        public static Task<T> FromResult<T>(T result)
        {
            var tcs = new TaskCompletionSource<T>();

            tcs.SetResult(result);

            return tcs.Task;
        }

        public static Task Delay(int milliseconds)
        {
            return Task.Factory.StartNew(() => Thread.Sleep(milliseconds));
        }
#else
        public static Task<T> FromResult<T>(T result) => Task.FromResult(result);
        public static Task Delay(int milliseconds) => Task.Delay(milliseconds);
#endif
    }
}
