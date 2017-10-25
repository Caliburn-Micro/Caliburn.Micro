namespace Caliburn.Micro {
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
#if WINDOWS_UWP
    using System.Reflection;
    using Windows.UI.Core;
    using Windows.UI.Xaml;
#else
    using System.Windows;
    using System.Windows.Threading;
#endif

    /// <summary>
    /// A <see cref="IPlatformProvider"/> implementation for the XAML platfrom.
    /// </summary>
    public class XamlPlatformProvider : IPlatformProvider {
#if WINDOWS_UWP
        private CoreDispatcher dispatcher;
#else
        private Dispatcher dispatcher;
#endif

        /// <summary>
        /// Initializes a new instance of the <see cref="XamlPlatformProvider"/> class.
        /// </summary>
        public XamlPlatformProvider() {
#if WINDOWS_UWP
            dispatcher = Window.Current.Dispatcher;
#else
            dispatcher = Dispatcher.CurrentDispatcher;
#endif
        }

        /// <summary>
        /// Indicates whether or not the framework is in design-time mode.
        /// </summary>
        public virtual bool InDesignMode {
            get { return View.InDesignMode; }
        }

        private void ValidateDispatcher() {
            if (dispatcher == null)
                throw new InvalidOperationException("Not initialized with dispatcher.");
        }

        private bool CheckAccess() {
#if WINDOWS_UWP
            return dispatcher == null || Window.Current != null;
#else
            return dispatcher == null || dispatcher.CheckAccess();
#endif
        }

        /// <summary>
        /// Executes the action on the UI thread asynchronously.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        public virtual void BeginOnUIThread(System.Action action) {
            ValidateDispatcher();
#if WINDOWS_UWP
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
        public virtual Task OnUIThreadAsync(System.Action action) {
            ValidateDispatcher();
#if WINDOWS_UWP
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
            dispatcher.BeginInvoke(method);
            return taskSource.Task;
#endif
        }

        /// <summary>
        /// Executes the action on the UI thread.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual void OnUIThread(System.Action action) {
            if (CheckAccess())
                action();
            else {
#if WINDOWS_UWP
                dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => action()).AsTask().Wait();
#elif NET
                Exception exception = null;
                System.Action method = () => {
                    try {
                        action();
                    }
                    catch(Exception ex) {
                        exception = ex;
                    }
                };
                dispatcher.Invoke(method);
                if (exception != null)
                    throw new System.Reflection.TargetInvocationException("An error occurred while dispatching a call to the UI Thread", exception);
#else
                var waitHandle = new System.Threading.ManualResetEvent(false);
                Exception exception = null;
                System.Action method = () => {
                    try {
                        action();
                    }
                    catch (Exception ex) {
                        exception = ex;
                    }
                    waitHandle.Set();
                };
                dispatcher.BeginInvoke(method);
                waitHandle.WaitOne();
                if (exception != null)
                    throw new System.Reflection.TargetInvocationException("An error occurred while dispatching a call to the UI Thread", exception);
#endif
            }
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
        public virtual object GetFirstNonGeneratedView(object view) {
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
        public virtual void ExecuteOnFirstLoad(object view, Action<object> handler) {
            var element = view as FrameworkElement;
            if (element != null && !(bool) element.GetValue(PreviouslyAttachedProperty)) {
                element.SetValue(PreviouslyAttachedProperty, true);
                View.ExecuteOnLoad(element, (s, e) => handler(s));
            }
        }

        /// <summary>
        /// Executes the handler the next time the view's LayoutUpdated event fires.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="handler">The handler.</param>
        public virtual void ExecuteOnLayoutUpdated(object view, Action<object> handler) {
            var element = view as FrameworkElement;
            if (element != null) {
                View.ExecuteOnLayoutUpdated(element, (s, e) => handler(s));
            }
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
        public virtual System.Action GetViewCloseAction(object viewModel, ICollection<object> views, bool? dialogResult) {
            var child = viewModel as IChild;
            if (child != null) {
                var conductor = child.Parent as IConductor;
                if (conductor != null) {
                    return () => conductor.CloseItem(viewModel);
                }
            }

            foreach (var contextualView in views) {
                var viewType = contextualView.GetType();
#if WINDOWS_UWP
                var closeMethod = viewType.GetRuntimeMethod("Close", new Type[0]);
#else
                var closeMethod = viewType.GetMethod("Close");
#endif
                if (closeMethod != null)
                    return () => {
#if !WINDOWS_UWP
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

#if WINDOWS_UWP
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
