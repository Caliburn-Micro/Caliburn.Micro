using System;
using Caliburn.Micro;
using Setup.iOS.ViewModels;
using UIKit;

namespace Setup.iOS
{
	public partial class MainViewController : UIViewController
	{
        private readonly MainViewModel viewModel;

        public MainViewController (IntPtr handle)
            : base (handle)
		{
            viewModel = IoC.Get<MainViewModel>();

            var viewAware = (IViewAware)viewModel;

            viewAware.AttachView(this);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            ScreenExtensions.TryActivate(viewModel);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            ScreenExtensions.TryDeactivate(viewModel, false);
        }
    }
}
