namespace Caliburn.Micro {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using System.Windows;
#if WinRT
    using System.Reflection;
    using Windows.ApplicationModel;
    using Windows.UI.Core;
    using Windows.UI.Xaml;
#else
    using System.Windows.Threading;
#endif

    /// <summary>
    /// A <see cref="IPlatformProvider"/> implementation for the XAML platfrom.
    /// </summary>
    public class XamlPlatformProvider : IPlatformProvider {
        private bool? inDesignMode;
#if WinRT
        private CoreDispatcher dispatcher;
#else
        private Dispatcher dispatcher;
#endif

        /// <summary>
        /// Initializes a new instance of the <see cref="XamlPlatformProvider"/> class.
        /// </summary>
        public XamlPlatformProvider() {
#if SILVERLIGHT
            dispatcher = System.Windows.Deployment.Current.Dispatcher;
#elif WinRT
            dispatcher = Window.Current.Dispatcher;
#else
            dispatcher = Dispatcher.CurrentDispatcher;
#endif
        }

        /// <summary>
        /// Indicates whether or not the framework is in design-time mode.
        /// </summary>
        public bool InDesignMode {
            get {
                if (inDesignMode == null) {
#if WinRT
                    inDesignMode = DesignMode.DesignModeEnabled;
#elif SILVERLIGHT
                    inDesignMode = DesignerProperties.IsInDesignTool;
#else
                    var prop = DesignerProperties.IsInDesignModeProperty;
                    inDesignMode = (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;

                    if (!inDesignMode.GetValueOrDefault(false) && Process.GetCurrentProcess().ProcessName.StartsWith("devenv", StringComparison.Ordinal))
                        inDesignMode = true;
#endif
                }

                return inDesignMode.GetValueOrDefault(false);
            }
        }

        private void ValidateDispatcher() {
            if (dispatcher == null)
                throw new InvalidOperationException("Not initialized with dispatcher.");
        }

        private bool CheckAccess() {
#if WinRT
            return dispatcher == null || Window.Current != null;
#else
            return dispatcher == null || dispatcher.CheckAccess();
#endif
        }

        /// <summary>
        /// Executes the action on the UI thread asynchronously.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        public void BeginOnUIThread(System.Action action) {
            ValidateDispatcher();
#if WinRT
            var dummy = dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => action());
#else
            dispatcher.BeginInvoke(action);
#endif
        }

        /// <summary>
        /// Executes the action on the UI thread asynchronously.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <returns></returns>
        public Task OnUIThreadAsync(System.Action action) {
            ValidateDispatcher();
#if WinRT
            return dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => action()).AsTask();
#elif NET45
            return dispatcher.InvokeAsync(action).Task;
#else
            var taskSource = new TaskCompletionSource<object>();
            System.Action method = () => {
                try {
                    action();
                    taskSource.SetResult(null);
                }
                catch(Exception ex) {
                    taskSource.SetException(ex);
                }
            };
            var operation = dispatcher.BeginInvoke(method);
#if !SL5 && !WP8
            if (operation.Status == DispatcherOperationStatus.Aborted) {
                taskSource.SetCanceled();
            }
#endif
            return taskSource.Task;
#endif
        }

        /// <summary>
        /// Executes the action on the UI thread.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void OnUIThread(System.Action action) {
            if (CheckAccess())
                action();
            else
                OnUIThreadAsync(action).Wait();
        }

        /// <summary>
        /// Used to retrieve the root, non-framework-created view.
        /// </summary>
        /// <param name="view">The view to search.</param>
        /// <returns>
        /// The root element that was not created by the framework.
        /// </returns>
        /// <remarks>
        /// In certain instances the services create UI elements.
        /// For example, if you ask the window manager to show a UserControl as a dialog, it creates a window to host the UserControl in.
        /// The WindowManager marks that element as a framework-created element so that it can determine what it created vs. what was intended by the developer.
        /// Calling GetFirstNonGeneratedView allows the framework to discover what the original element was.
        /// </remarks>
        public object GetFirstNonGeneratedView(object view) {
            return View.GetFirstNonGeneratedView(view);
        }

        private static readonly DependencyProperty PreviouslyAttachedProperty = DependencyProperty.RegisterAttached(
            "PreviouslyAttached",
            typeof (bool),
            typeof (XamlPlatformProvider),
            null
            );

        /// <summary>
        /// Executes the handler the fist time the view is loaded.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="handler">The handler.</param>
        /// <returns>true if the handler was executed immediately; false otherwise</returns>
        public bool ExecuteOnFirstLoad(object view, Action<object> handler) {
            var element = view as FrameworkElement;
            if (element != null && !(bool) element.GetValue(PreviouslyAttachedProperty)) {
                element.SetValue(PreviouslyAttachedProperty, true);
                return View.ExecuteOnLoad(element, (s, e) => handler(s));
            }
            return false;
        }

        /// <summary>
        /// Get the close action for the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model to close.</param>
        /// <param name="views">The associated views.</param>
        /// <param name="dialogResult">The dialog result.</param>
        /// <returns>
        /// An <see cref="Action" /> to close the view model.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public System.Action GetViewCloseAction(object viewModel, ICollection<object> views, bool? dialogResult) {
            var child = viewModel as IChild;
            if (child != null) {
                var conductor = child.Parent as IConductor;
                if (conductor != null) {
                    return () => conductor.CloseItem(viewModel);
                }
            }

            foreach (var contextualView in views) {
                var viewType = contextualView.GetType();
#if WinRT
                var closeMethod = viewType.GetRuntimeMethod("Close", new Type[0]);
#else
                var closeMethod = viewType.GetMethod("Close");
#endif
                if (closeMethod != null)
                    return () => {
#if !SILVERLIGHT && !WinRT
                        var isClosed = false;
                        if (dialogResult != null) {
                            var resultProperty = contextualView.GetType().GetProperty("DialogResult");
                            if (resultProperty != null) {
                                resultProperty.SetValue(contextualView, dialogResult, null);
                                isClosed = true;
                            }
                        }

                        if (!isClosed) {
                            closeMethod.Invoke(contextualView, null);
                        }
#else
                        closeMethod.Invoke(contextualView, null);
#endif
                    };

#if WinRT
                var isOpenProperty = viewType.GetRuntimeProperty("IsOpen");
#else
                var isOpenProperty = viewType.GetProperty("IsOpen");
#endif
                if (isOpenProperty != null) {
                    return () => isOpenProperty.SetValue(contextualView, false, null);
                }
            }

            return () => LogManager.GetLog(typeof(Screen)).Info("TryClose requires a parent IConductor or a view with a Close method or IsOpen property.");
        }
    }
}
