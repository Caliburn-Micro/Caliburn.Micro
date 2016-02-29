using System;
using Android.App;
using Caliburn.Micro;

namespace Hello.Xamarin.Android.Framework
{
    public class ActivityBase<T> : Activity
    {
        public ActivityBase()
        {
            ViewModel = (T) ViewModelLocator.LocateForView(this);

            var viewAware = ViewModel as IViewAware;

            if (viewAware != null)
            {
                viewAware.AttachView(this);
            }
        }

        protected override void OnResume()
        {
            base.OnResume();

            var activate = ViewModel as IActivate;

            if (activate != null)
            {
                activate.Activate();
            }
        }

        protected override void OnPause()
        {
            base.OnPause();

            var deactivate = ViewModel as IDeactivate;

            if (deactivate != null)
            {
                deactivate.Deactivate(false);
            }
        }

        protected T ViewModel { get; private set; }
    }
}