using System;
using System.Collections.Generic;
using System.Text;
using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Setup.Forms5.ViewModels;
using Setup.Forms5.Views;
using Xamarin.Forms;
using INavigationService = Caliburn.Micro.Xamarin.Forms.INavigationService;

namespace Setup.Forms5
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
