namespace Caliburn.Micro {
    using System;
    using System.Threading.Tasks;

    /// <summary>
    ///   Enables easy marshalling of code to the UI thread.
    /// </summary>
    public static class Execute {
        /// <summary>
        ///   Indicates whether or not the framework is in design-time mode.
        /// </summary>
        public static bool InDesignMode {
            get {
                return PlatformProvider.Current.InDesignMode;
            }
        }

        /// <summary>
        ///   Executes the action on the UI thread asynchronously.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        public static void BeginOnUIThread(this Action action) {
            PlatformProvider.Current.BeginOnUIThread(action);
        }

        /// <summary>
        ///   Executes the action on the UI thread asynchronously.
        /// </summary>
        /// <param name = "action">The action to execute.</param>
        public static Task OnUIThreadAsync(this Action action) {
            return PlatformProvider.Current.OnUIThreadAsync(action);
        }

        /// <summary>
        ///   Executes the action on the UI thread.
        /// </summary>
        /// <param name = "action">The action to execute.</param>
        public static void OnUIThread(this Action action) {
            PlatformProvider.Current.OnUIThread(action);
        }
    }
}
