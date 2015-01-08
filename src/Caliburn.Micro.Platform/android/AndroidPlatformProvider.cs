using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;

namespace Caliburn.Micro
{
    /// <summary>
    /// A <see cref="IPlatformProvider"/> implementation for the Xamarin Android platfrom.
    /// </summary>
    public class AndroidPlatformProvider : IPlatformProvider
    {
        public bool InDesignMode {
            get { return false; }
        }

        public void BeginOnUIThread(Action action) {

            Application.SynchronizationContext.Post(s => action(), null);

        }

        public Task OnUIThreadAsync(Action action) {

            var completionSource = new TaskCompletionSource<bool>();

            Application.SynchronizationContext.Post(s => {

                try {
                    action();

                    completionSource.SetResult(true);

                }
                catch (TaskCanceledException) {
                    completionSource.SetCanceled();
                }
                catch (Exception ex) {
                    completionSource.SetException(ex);
                }

            }, null);


            return completionSource.Task;
        }

        public void OnUIThread(Action action) {
            OnUIThreadAsync(action).Wait();
        }

        public object GetFirstNonGeneratedView(object view) {
            return view;
        }

        public void ExecuteOnFirstLoad(object view, Action<object> handler) {
            
        }

        public void ExecuteOnLayoutUpdated(object view, Action<object> handler) {
            
        }

        public Action GetViewCloseAction(object viewModel, ICollection<object> views, bool? dialogResult) {

            var child = viewModel as IChild;

            if (child != null) {
                var conductor = child.Parent as IConductor;

                if (conductor != null) {
                    return () => conductor.CloseItem(viewModel);
                }
            }

            return () => LogManager.GetLog(typeof(Screen)).Info("TryClose requires a parent IConductor or a view with a Close method or IsOpen property.");
        }
    }
}