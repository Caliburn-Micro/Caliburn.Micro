using Caliburn.Micro;
using Foundation;
using Setup.iOS.ViewModel;
using System;
using UIKit;

namespace Setup.iOS
{
    public partial class MainViewController : UIViewController
    {
        private readonly MainViewModel viewModel;

        public MainViewController(IntPtr handle)
            : base(handle)
        {
            viewModel = IoC.Get<MainViewModel>();

            var viewAware = (IViewAware)viewModel;

            viewAware.AttachView(this);
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();

            await ScreenExtensions.TryActivateAsync(viewModel);
        }

        public override async void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            await ScreenExtensions.TryDeactivateAsync(viewModel, false);
        }
    }
}
