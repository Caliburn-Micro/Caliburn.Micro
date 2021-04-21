using System;
using System.Net.Mime;
using Android.App;
using Android.Widget;
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

        protected override async void OnResume()
        {
            base.OnResume();

            await ScreenExtensions.TryActivateAsync(viewModel);
        }

        protected override async void OnPause()
        {
            base.OnPause();

            await ScreenExtensions.TryDeactivateAsync(viewModel, false);
        }
    }
}
