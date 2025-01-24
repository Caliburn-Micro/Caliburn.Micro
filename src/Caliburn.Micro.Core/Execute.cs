using System;
using System.Threading.Tasks;

namespace Caliburn.Micro
{
    /// <summary>
    ///   Enables easy marshalling of code to the UI thread.
    /// </summary>
    public static class Execute
    {
        /// <summary>
        ///   Indicates whether or not the framework is in design-time mode.
        /// </summary>
        public static bool InDesignMode
        {
            get
            {
                return PlatformProvider.Current.InDesignMode;
            }
        }

        /// <summary>
        ///   Executes the action on the UI thread asynchronously.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        public static void BeginOnUIThread(this Action action)
        {
            PlatformProvider.Current.BeginOnUIThread(action);
        }

        /// <summary>
        ///   Executes the action on the UI thread asynchronously.
        /// </summary>
        /// <param name = "action">The action to execute.</param>
        public static Task OnUIThreadAsync(this Func<Task> action)
        {
            return PlatformProvider.Current.OnUIThreadAsync(action);
        }

        /// <summary>
        ///   Executes the action on the UI thread.
        /// </summary>
        /// <param name = "action">The action to execute.</param>
        public static void OnUIThread(this Action action)
        {
            PlatformProvider.Current.OnUIThread(action);
        }

        /// <summary>
        /// Executes the action on safe manner either on UI thread or current Thread
        /// </summary>
        /// <param name="action"></param>
        public static void SafeRun(this Action action)
        {
            if (PlatformProvider.Current.PropertyChangeNotificationsOnUIThread)
            {
                OnUIThread(action);
            }
            else
            {
                action();
            }
        }
    }
}
