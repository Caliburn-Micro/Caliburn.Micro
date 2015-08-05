using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Foundation;
using UIKit;

namespace Caliburn.Micro
{
    /// <summary>
    /// A <see cref="IPlatformProvider"/> implementation for the Xamarin iOS platfrom.
    /// </summary>
    public class IOSPlatformProvider : IPlatformProvider
    {
        private bool CheckAccess() {
            return NSThread.IsMain;
        }

        public bool InDesignMode
        {
            get { return false; }
        }

        public void BeginOnUIThread(Action action)
        {
            UIApplication.SharedApplication.InvokeOnMainThread(action);
        }

        public Task OnUIThreadAsync(Action action)
        {
            var completionSource = new TaskCompletionSource<bool>();

            UIApplication.SharedApplication.InvokeOnMainThread(() =>
            {

                try
                {
                    action();

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

        public void OnUIThread(Action action) {
            if (CheckAccess())
                action();
            else
                OnUIThreadAsync(action).Wait();
        }

        public object GetFirstNonGeneratedView(object view)
        {
            return view;
        }

        public void ExecuteOnFirstLoad(object view, Action<object> handler)
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

        public void ExecuteOnLayoutUpdated(object view, Action<object> handler)
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

        public Action GetViewCloseAction(object viewModel, ICollection<object> views, bool? dialogResult)
        {
            var child = viewModel as IChild;

            if (child != null)
            {
                var conductor = child.Parent as IConductor;

                if (conductor != null)
                {
                    return () => conductor.CloseItem(viewModel);
                }
            }

            return () => LogManager.GetLog(typeof(Screen)).Info("TryClose requires a parent IConductor or a view with a Close method or IsOpen property.");
        }
    }
}