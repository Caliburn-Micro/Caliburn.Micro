using System.Threading.Tasks;
using Caliburn.Micro;
using Features.CrossPlatform.Views;

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
            var view = (ExecuteView)GetView();

            view.UpdateView();

            return Task.CompletedTask;
        }
    }
}
