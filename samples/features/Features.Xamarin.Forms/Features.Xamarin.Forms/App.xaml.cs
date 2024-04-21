﻿using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Features.CrossPlatform.ViewModels;
using Features.CrossPlatform.Views;
using Xamarin.Forms;

namespace Features.Xamarin.Forms
{
    public partial class App : FormsApplication
    {
        private readonly SimpleContainer container;

        public App(SimpleContainer container)
        {
            Initialize();

            MessageBinder.SpecialValues.Add("$selecteditem", c =>
            {
                var listView = c.Source as ListView;

                return listView?.SelectedItem;
            });

            this.container = container;

            container
               .PerRequest<MenuViewModel>()
               .PerRequest<BindingsViewModel>()
               .PerRequest<ActionsViewModel>()
               .PerRequest<CoroutineViewModel>()
               .PerRequest<ExecuteViewModel>()
               .PerRequest<EventAggregationViewModel>()
               .PerRequest<DesignTimeViewModel>()
               .PerRequest<ConductorViewModel>()
               .PerRequest<BubblingViewModel>()
               .PerRequest<NavigationSourceViewModel>()
               .PerRequest<NavigationTargetViewModel>();

            DisplayRootView<MenuView>();
        }

        protected override void PrepareViewFirst(NavigationPage navigationPage)
        {
            container.Instance<INavigationService>(new NavigationPageAdapter(navigationPage));
        }
    }
}
