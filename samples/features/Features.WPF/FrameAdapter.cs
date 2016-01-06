using System;
using System.Windows.Controls;
using Caliburn.Micro;

namespace Features.CrossPlatform
{
    public class FrameAdapter : INavigationService
    {
        private readonly Frame frame;

        public FrameAdapter(Frame frame)
        {
            this.frame = frame;
        }

        public void NavigateToViewModel(Type viewModelType)
        {
            var view = ViewLocator.LocateForModelType(viewModelType, null, null);
            var viewModel = ViewModelLocator.LocateForView(view);

            ViewModelBinder.Bind(viewModel, view, null);

            var activator = viewModel as IActivate;

            activator?.Activate();

            frame.Navigate(view);
        }
    }
}
