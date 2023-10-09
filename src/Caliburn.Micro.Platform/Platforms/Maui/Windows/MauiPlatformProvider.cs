using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.UI.Xaml;
using Windows.UI.Core;

namespace Caliburn.Micro.Maui {
    /// <summary>
    /// A <see cref="IPlatformProvider"/> implementation for the XAML platfrom.
    /// </summary>
    public class MauiPlatformProvider : IPlatformProvider {
        /*
        private static readonly DependencyProperty PreviouslyAttachedProperty
            = DependencyProperty.RegisterAttached(
                "PreviouslyAttached",
                typeof(bool),
                typeof(MauiPlatformProvider),
                null);
        */

        private readonly CoreDispatcher _dispatcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="MauiPlatformProvider"/> class.
        /// </summary>
        public MauiPlatformProvider()
            => _dispatcher = Window.Current?.Dispatcher;

        /// <summary>
        /// Gets a value indicating whether or not classes should execute property change notications on the UI thread.
        /// </summary>
        public virtual bool PropertyChangeNotificationsOnUIThread => true;

        /// <summary>
        /// Gets a value indicating whether or not the framework is in design-time mode.
        /// </summary>
        public virtual bool InDesignMode => View.InDesignMode;

        /// <summary>
        /// Executes the action on the UI thread asynchronously.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        public virtual void BeginOnUIThread(System.Action action) {
            ValidateDispatcher();
            Windows.Foundation.IAsyncAction dummy = _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => action());
        }

        /// <summary>
        /// Executes the action on the UI thread asynchronously.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        public virtual Task OnUIThreadAsync(Func<Task> action) {
            ValidateDispatcher();

            return _dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () => action())
                .AsTask();
        }

        /// <summary>
        /// Executes the action on the UI thread.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        public virtual void OnUIThread(System.Action action) {
            if (CheckAccess()) {
                action();
            } else {
                _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => action()).AsTask().Wait();
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
        public virtual object GetFirstNonGeneratedView(object view)
            => View.GetFirstNonGeneratedView(view);

        /// <summary>
        /// Executes the handler the fist time the view is loaded.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="handler">The handler.</param>
        public virtual void ExecuteOnFirstLoad(object view, Action<object> handler) {
            /*
            var element = view as FrameworkElement;
            if (element != null && !(bool)element.GetValue(PreviouslyAttachedProperty)) {
                element.SetValue(PreviouslyAttachedProperty, true);
                View.ExecuteOnLoad(element, (s, e) => handler(s));
            }
            */
        }

        /// <summary>
        /// Executes the handler the next time the view's LayoutUpdated event fires.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="handler">The handler.</param>
        public virtual void ExecuteOnLayoutUpdated(object view, Action<object> handler) {
            /*
            var element = view as FrameworkElement;
            if (element != null) {
                View.ExecuteOnLayoutUpdated(element, (s, e) => handler(s));
            }
            */
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

                MethodInfo closeMethod = viewType.GetRuntimeMethod("Close", Array.Empty<Type>());

                if (closeMethod != null) {
                    return ct => {
                        closeMethod.Invoke(contextualView, null);
                        return Task.FromResult(true);
                    };
                }

                PropertyInfo isOpenProperty = viewType.GetRuntimeProperty("IsOpen");

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
            if (_dispatcher == null) {
                throw new InvalidOperationException("Not initialized with dispatcher.");
            }
        }

        private bool CheckAccess()
            => _dispatcher == null || Window.Current != null;
    }
}
