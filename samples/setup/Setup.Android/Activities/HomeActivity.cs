using System;
using Android.App;
using Caliburn.Micro;
using Setup.Android.ViewModels;

namespace Setup.Android.Activities
{
    [Activity(MainLauncher = true)]
    public class HomeActivity : Activity
    {
        private readonly HomeViewModel viewModel;

        public HomeActivity()
        {
            viewModel = IoC.Get<HomeViewModel>();

            var viewAware = (IViewAware) viewModel;

            viewAware.AttachView(this);
        }

        protected override void OnResume()
        {
            base.OnResume();

            ScreenExtensions.TryActivate(viewModel);
        }

        protected override void OnPause()
        {
            base.OnPause();

            ScreenExtensions.TryDeactivate(viewModel, false);
        }
    }
}