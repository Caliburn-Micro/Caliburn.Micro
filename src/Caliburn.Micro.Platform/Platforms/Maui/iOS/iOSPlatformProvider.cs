using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Foundation;
using UIKit;

namespace Caliburn.Micro.Maui
{
    /// <summary>
    /// A <see cref="IPlatformProvider"/> implementation for the Xamarin iOS platfrom.
    /// </summary>
    public class IOSPlatformProvider : IPlatformProvider
    {
        private bool CheckAccess() {
            return NSThread.IsMain;
        }

        /// <summary>
        ///   Indicates whether or not the framework is in design-time mode.
        /// </summary>
        public virtual bool InDesignMode
        {
            get { return false; }
        }

        /// <summary>
        /// Whether or not classes should execute property change notications on the UI thread.
        /// </summary>
        public virtual bool PropertyChangeNotificationsOnUIThread => true;

        /// <summary>
        ///   Executes the action on the UI thread asynchronously.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        public virtual void BeginOnUIThread(System.Action action)
        {
            UIApplication.SharedApplication.InvokeOnMainThread(action);
        }

        /// <summary>
        ///   Executes the action on the UI thread asynchronously.
        /// </summary>
        /// <param name = "action">The action to execute.</param>
        public virtual Task OnUIThreadAsync(Func<Task> action)
        {
            var completionSource = new TaskCompletionSource<bool>();

            UIApplication.SharedApplication.InvokeOnMainThread(async () =>
            {

                try
                {
                    await action();

                    completionSource.SetResult(true);

                }
                catch (TaskCanceledException)
                {
                    completionSource.SetCanceled();
                }
                catch (Exception ex)
                {
                    completionSource.SetException(ex);
                }

            });

            return completionSource.Task;
        }

        /// <summary>
        ///   Executes the action on the UI thread.
        /// </summary>
        /// <param name = "action">The action to execute.</param>
        public virtual void OnUIThread(System.Action action) {
            if (CheckAccess())
                action();
            else
                OnUIThreadAsync(() =>
                {
                    action();
                    return Task.CompletedTask;
                }).Wait();
        }

        /// <summary>
        /// Used to retrieve the root, non-framework-created view.
        /// </summary>
        /// <param name="view">The view to search.</param>
        /// <returns>The root element that was not created by the framework.</returns>
        /// <remarks>In certain instances the services create UI elements.
        /// For example, if you ask the window manager to show a UserControl as a dialog, it creates a window to host the UserControl in.
        /// The WindowManager marks that element as a framework-created element so that it can determine what it created vs. what was intended by the developer.
        /// Calling GetFirstNonGeneratedView allows the framework to discover what the original element was. 
        /// </remarks>
        public virtual object GetFirstNonGeneratedView(object view)
        {
            return view;
        }

        /// <summary>
        /// Executes the handler the fist time the view is loaded.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="handler">The handler.</param>
        public virtual void ExecuteOnFirstLoad(object view, Action<object> handler)
        {
            var viewController = view as IUIViewController;

            if (viewController != null) {

                EventHandler created = null;

                created = (s, e) =>
                {
                    viewController.ViewLoaded -= created;

                    handler(view);
                };

                viewController.ViewLoaded += created;
            }
        }

        /// <summary>
        /// Executes the handler the next time the view's LayoutUpdated event fires.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="handler">The handler.</param>
        public virtual void ExecuteOnLayoutUpdated(object view, Action<object> handler)
        {
            var viewController = view as IUIViewController;

            if (viewController != null)
            {
                EventHandler appeared = null;

                appeared = (s, e) =>
                {
                    viewController.ViewAppeared -= appeared;

                    handler(view);
                };

                viewController.ViewAppeared += appeared;
            }
        }

        /// <summary>
        /// Get the close action for the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model to close.</param>
        /// <param name="views">The associated views.</param>
        /// <param name="dialogResult">The dialog result.</param>
        /// <returns>An <see cref="Action"/> to close the view model.</returns>
        public virtual Func<CancellationToken, Task> GetViewCloseAction(object viewModel, ICollection<object> views, bool? dialogResult)
        {
            return ct =>
            {
                LogManager.GetLog(typeof(Screen)).Info("TryClose requires a parent IConductor or a view with a Close method or IsOpen property.");
                return Task.FromResult(true);
            };
        }
    }
}
