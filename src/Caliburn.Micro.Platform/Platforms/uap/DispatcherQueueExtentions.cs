#if WinUI3


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Dispatching;
namespace Caliburn.Micro
{
    internal static class DispatcherQueueExtentions
    {
        public static Task RunAsync(this DispatcherQueue dispatcher, System.Action action)
        {
            if(dispatcher == null)
                return Task.FromException(new ArgumentException("Dispatcher cannot be null"));

            if (dispatcher.HasThreadAccess)
            {
                try
                {
                    action();

                    return Task.CompletedTask;
                }
                catch(Exception ex) 
                {
                    return Task.FromException(ex);
                }
            }

            var tcs = new TaskCompletionSource<object>();

            if (!dispatcher.TryEnqueue(() =>
                {
                    try
                    {
                        action();

                        tcs.SetResult(null);
                    }
                    catch (Exception ex)
                    {
                        tcs.TrySetException(ex);
                    }
                }))
            {
                tcs.SetException(new InvalidOperationException("Faild to queue task"));
            }

            return tcs.Task;
        }

        public static Task<T> RunAsync<T>(this DispatcherQueue dispatcher, System.Func<T> action)
        {
            if (dispatcher == null)
                return Task.FromException<T>(new ArgumentException("Dispatcher cannot be null"));

            if (dispatcher.HasThreadAccess)
            {
                try
                {
                    return Task.FromResult<T>(action());
                }
                catch (Exception ex)
                {
                    return Task.FromException<T>(ex);
                }
            }

            var tcs = new TaskCompletionSource<T>();

            if (!dispatcher.TryEnqueue(() =>
                {
                    try
                    {
                        tcs.SetResult(action());
                    }
                    catch (Exception ex)
                    {
                        tcs.SetException(ex);
                    }
                }))
            {
                tcs.SetException(new InvalidOperationException("Faild to queue task"));
            }

            return tcs.Task;
        }
    }
}

#endif
