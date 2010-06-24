namespace Caliburn.Micro
{
    using System;
    using System.Windows;

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
#else
            var dispatcher = Application.Current.Dispatcher;
#endif
            executor = action =>{
                if(dispatcher.CheckAccess())
                    action();
                else dispatcher.BeginInvoke(action);
            };
        }

        /// <summary>
        /// Executes the action on the UI thread.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        public static void OnUIThread(this System.Action action)
        {
            executor(action);
        }
    }
}