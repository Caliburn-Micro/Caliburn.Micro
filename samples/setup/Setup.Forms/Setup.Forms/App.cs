using System;
using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Setup.Forms.ViewModels;
using Setup.Forms.Views;
using Xamarin.Forms;

namespace Setup.Forms
{
    public class App : FormsApplication
    {
        private readonly SimpleContainer container;

        public App(SimpleContainer container)
        {
            Initialize();

            this.container = container;

            container.PerRequest<HomeViewModel>();

            DisplayRootView<HomeView>();
        }

        protected override void PrepareViewFirst(NavigationPage navigationPage)
        {
            container.Instance<INavigationService>(new NavigationPageAdapter(navigationPage));
        }
    }
}
