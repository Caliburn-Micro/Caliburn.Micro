using System;
using System.Threading.Tasks;
using Caliburn.Micro.WinRT.Sample.Results;
using Caliburn.Micro.WinRT.Sample.Views;

namespace Caliburn.Micro.WinRT.Sample.ViewModels
{
    public class ExecuteViewModel : ViewModelBase
    {
        public ExecuteViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }

        // An ActionMessage can now return Task
        // When using async and an Exception is thrown, it will be catched an logged by the Coroutine class
        public async Task ExecuteOnThread()
        {
            // You can use a Coroutine inside an async method and await it using ExecuteAsync() extension method
            await new DelayResult(1000).ExecuteAsync();

            await Task.Factory.StartNew(BackgroundWork);
        }

        private void BackgroundWork()
        {
            // Do some expensive work here ...

            // UpdateView(); // Crashes
            Execute.OnUIThreadAsync(UpdateView);
        }

        private void UpdateView()
        {
            // By not going through bindings we've sidestepped Caliburn to update the view off the UI thread
            var view = (ExecuteView) GetView();
            
            view.UpdateView();
        }
    }
}
