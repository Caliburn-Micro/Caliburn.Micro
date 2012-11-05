using System;
using System.Threading.Tasks;
using Caliburn.Micro.WinRT.Sample.Views;

namespace Caliburn.Micro.WinRT.Sample.ViewModels
{
    public class ExecuteViewModel : ViewModelBase
    {
        public ExecuteViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }

        public async void ExecuteOnThread()
        {
            await Task.Factory.StartNew(BackgroundWork);
        }

        private void BackgroundWork()
        {
            // Do some expensive work here ...

            // UpdateView(); // Crashes
            Execute.OnUIThread(UpdateView);
        }

        private void UpdateView()
        {
            // By not going through bindings we've sidestepped Caliburn to update the view off the UI thread
            var view = (ExecuteView) GetView();
            
            view.UpdateView();
        }
    }
}
