using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

#if WINDOWS_UWP
using System.Reflection;

using Windows.UI.Core;
using Windows.UI.Xaml;
#else
using System.Windows;
using System.Windows.Threading;
#endif

namespace Caliburn.Micro {
    /// <summary>
    /// A <see cref="IPlatformProvider"/> implementation for the XAML platfrom.
    /// </summary>
    public class XamlPlatformProvider : IPlatformProvider {
        private static readonly DependencyProperty PreviouslyAttachedProperty = DependencyProperty.RegisterAttached(
            "PreviouslyAttached",
            typeof(bool),
            typeof(XamlPlatformProvider),
            null);

#if WINDOWS_UWP
        private readonly CoreDispatcher dispatcher;
#else
        private readonly Dispatcher dispatcher;
#endif

        /// <summary>
        /// Initializes a new instance of the <see cref="XamlPlatformProvider"/> class.
        /// </summary>
        public XamlPlatformProvider() =>
#if WINDOWS_UWP
            dispatcher = Window.Current.Dispatcher;
#else
            dispatcher = Dispatcher.CurrentDispatcher;
#endif

        /// <summary>
        /// Gets a value indicating whether or not classes should execute property change notications on the UI thread.
        /// </summary>
        public virtual bool PropertyChangeNotificationsOnUIThread => true;

        /// <summary>
        /// Gets a value indicating whether or not the framework is in design-time mode.
        /// </summary>
        public virtual bool InDesignMode
            => View.InDesignMode;

        /// <summary>
        /// Executes the action on the UI thread asynchronously.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        public virtual void BeginOnUIThread(System.Action action) {
            ValidateDispatcher();
#if WINDOWS_UWP
            Windows.Foundation.IAsyncAction dummy = dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => action());
#else
            dispatcher.BeginInvoke(action);
#endif
        }

        /// <summary>
        /// Executes the action on the UI thread asynchronously.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        public virtual Task OnUIThreadAsync(Func<Task> action) {
            ValidateDispatcher();
#if WINDOWS_UWP
            return dispatcher.RunTaskAsync(action);
#else
            return dispatcher.InvokeAsync(action).Task.Unwrap();
#endif
        }

        /// <summary>
        /// Executes the action on the UI thread.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        public virtual void OnUIThread(System.Action action) {
            if (CheckAccess()) {
                action();
                return;
            }
#if WINDOWS_UWP
            dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => action()).AsTask().Wait();
#else
            Exception exception = null;
            void Method() {
                try {
                    action();
                } catch (Exception ex) {
                    exception = ex;
                }
            }

            dispatcher.Invoke(Method);
            if (exception != null) {
                throw new System.Reflection.TargetInvocationException("An error occurred while dispatching a call to the UI Thread", exception);
            }
#endif
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
        public virtual object GetFirstNonGeneratedView(object view) => View.GetFirstNonGeneratedView(view);

        /// <summary>
        /// Executes the handler the fist time the view is loaded.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="handler">The handler.</param>
        public virtual void ExecuteOnFirstLoad(object view, Action<object> handler) {
            if (view is not FrameworkElement element || (bool)element.GetValue(PreviouslyAttachedProperty)) {
                return;
            }

            element.SetValue(PreviouslyAttachedProperty, true);
            View.ExecuteOnLoad(element, (s, e) => handler(s));
        }

        /// <summary>
        /// Executes the handler the next time the view's LayoutUpdated event fires.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="handler">The handler.</param>
        public virtual void ExecuteOnLayoutUpdated(object view, Action<object> handler) {
            if (view is not FrameworkElement element) {
                return;
            }

            View.ExecuteOnLayoutUpdated(element, (s, e) => handler(s));
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
        public virtual Func<CancellationToken, Task> GetViewCloseAction(object viewModel, ICollection<object> views, bool? dialogResult) {
            foreach (object contextualView in views) {
                Type viewType = contextualView.GetType();
#if WINDOWS_UWP
                MethodInfo closeMethod = viewType.GetRuntimeMethod("Close", Array.Empty<Type>());
#else
                System.Reflection.MethodInfo closeMethod = viewType.GetMethod("Close", Array.Empty<Type>());
#endif
                if (closeMethod != null) {
                    return ct => {
#if !WINDOWS_UWP
                        bool isClosed = false;
                        if (dialogResult != null) {
                            System.Reflection.PropertyInfo resultProperty = contextualView.GetType().GetProperty("DialogResult");
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
                        return Task.FromResult(true);
                    };
                }

#if WINDOWS_UWP
                PropertyInfo isOpenProperty = viewType.GetRuntimeProperty("IsOpen");
#else
                System.Reflection.PropertyInfo isOpenProperty = viewType.GetProperty("IsOpen");
#endif
                if (isOpenProperty != null) {
                    return ct => {
                        isOpenProperty.SetValue(contextualView, false, null);

                        return Task.FromResult(true);
                    };
                }
            }

            return ct => {
                LogManager.GetLog(typeof(Screen)).Info("TryClose requires a parent IConductor or a view with a Close method or IsOpen property.");
                return Task.FromResult(true);
            };
        }

        private void ValidateDispatcher() {
            if (dispatcher == null) {
                throw new InvalidOperationException("Not initialized with dispatcher.");
            }
        }

        private bool CheckAccess() =>
#if WINDOWS_UWP
            dispatcher == null || Window.Current != null;
#else
            dispatcher == null || dispatcher.CheckAccess();
#endif
    }
}
