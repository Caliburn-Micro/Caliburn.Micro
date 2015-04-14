using System;
using Caliburn.Micro;
using UIKit;

namespace Hello.Xamarin.iOS.Framework
{
    public class UIViewControllerBase<T> : UIViewController
    {
        public UIViewControllerBase()
        {
            SetupViewModel();
        }

        protected internal UIViewControllerBase(IntPtr handle)
            : base(handle)
        {
            SetupViewModel();
        }

        private void SetupViewModel()
        {
            ViewModel = (T) ViewModelLocator.LocateForView(this);

            var viewAware = ViewModel as IViewAware;

            if (viewAware != null)
            {
                viewAware.AttachView(this);
            }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var activate = ViewModel as IActivate;

            if (activate != null)
            {
                activate.Activate();
            }
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            var deactivate = ViewModel as IDeactivate;

            if (deactivate != null)
            {
                deactivate.Deactivate(false);
            }
        }

        protected T ViewModel { get; private set; }
    }
}