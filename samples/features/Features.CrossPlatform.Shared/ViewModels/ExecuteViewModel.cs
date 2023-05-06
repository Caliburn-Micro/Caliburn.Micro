using System;
using System.Threading.Tasks;
using Caliburn.Micro;
#if AVALONIA
using Features.Avalonia.Views;
using Features.CrossPlatform.Views;
#else
using Features.CrossPlatform.Views;
#endif
namespace Features.CrossPlatform.ViewModels
{
    public class ExecuteViewModel : Screen
    {
        private bool _safe;

        public bool Safe
        {
            get { return _safe; }
            set { Set(ref _safe, value); }
        }

        public void StartBackgroundWork()
        {
            Task.Factory.StartNew(BackgroundWork);
        }

        private void BackgroundWork()
        {
            if (Safe)
                SafeBackgroundWork();
            else
                UnsafeBackgroundWork();
        }

        private void SafeBackgroundWork()
        {
            Execute.OnUIThreadAsync(UpdateView);
        }

        private void UnsafeBackgroundWork()
        {
            UpdateView();
        }

        private Task UpdateView()
        {
            var view = (ExecuteView) GetView();

            view.UpdateView();

            return Task.CompletedTask;
        }
    }
}
