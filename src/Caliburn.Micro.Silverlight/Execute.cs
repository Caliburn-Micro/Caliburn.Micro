namespace Caliburn.Micro
{
    using System;
    using System.Reflection;
    using System.Threading;
    using System.Windows;
    using System.Windows.Threading;

    /// <summary>
    /// Enables easy marshalling of code to the UI thread.
    /// </summary>
    public static class Execute
    {
        private static Action<System.Action> executor = action => action();

        /// <summary>
        /// Initializes the framework using the current dispatcher.
        /// </summary>
        public static void InitializeWithDispatcher()
        {
#if SILVERLIGHT
            var dispatcher = Deployment.Current.Dispatcher;

            executor = action => {
                if(dispatcher.CheckAccess())
                    action();
                else {
                    var waitHandle = new ManualResetEvent(false);
                    Exception exception = null;
                    dispatcher.BeginInvoke(() => {
                        try {
                            action();
                        }
                        catch(Exception ex) {
                            exception = ex;
                        }
                        waitHandle.Set();
                    });
                    waitHandle.WaitOne();
                    if(exception != null)
                        throw new TargetInvocationException("An error occurred while dispatching a call to the UI Thread", exception);
                }
            };
#else
            var dispatcher = Dispatcher.CurrentDispatcher;

            executor = action => {
                if(dispatcher.CheckAccess())
                    action();
                else dispatcher.Invoke(action);
            };
#endif

        }

        /// <summary>
        /// Resets the executor to use a non-dispatcher-based action executor.
        /// </summary>
        public static void ResetWithoutDispatcher() {
            executor = action => action();
        }

        /// <summary>
        /// Executes the action on the UI thread.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        public static void OnUIThread(this System.Action action) {
            executor(action);
        }
    }
}