// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Caliburn.Micro;
using Features.CrossPlatform.ViewModels;
using Windows.UI.Core;
using Windows.UI.Popups;
using Features.CrossPlatform.Views;
using System.Reflection.Metadata;
using Features.WinUI3.Views;
using Microsoft.UI.Xaml.Media.Animation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Features.WinUI3
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : CaliburnApplication
    {
        private WinRTContainer container;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        protected override void Configure()
        {
            container = new WinRTContainer();
            container.RegisterWinRTServices();

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
        }

        protected override void PrepareViewFirst(Frame rootFrame)
        {
            var navigationService = container.RegisterNavigationService(rootFrame);
            //var navigationManager = SystemNavigationManager.GetForCurrentView();

            //navigationService.Navigated += (s, e) =>
            //{
            //    navigationManager.AppViewBackButtonVisibility = navigationService.CanGoBack ?
            //        AppViewBackButtonVisibility.Visible :
            //        AppViewBackButtonVisibility.Collapsed;
            //};
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            if (args.UWPLaunchActivatedEventArgs.PreviousExecutionState == ApplicationExecutionState.Running)
                return;

            //DisplayRootView<MenuView>();
            DisplayRootNavigationView();
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

        protected override async void OnUnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            e.Handled = true;

            var dialog = new MessageDialog(e.Message, "An error has occurred");

            await dialog.ShowAsync();
        }

        public void GoBack()
        {
            RootFrame.GoBack();
        }

        public void DisplayRootNavigationView()
        {
            Initialize();

            InitializeWindow();
            PrepareViewFirst();

  
            RootFrame.Navigate(typeof(MenuView), null);

            var root = new RootView(RootFrame);

            // Seems stupid but observed weird behaviour when resetting the Content
            Window.Content = root;

            Window.Activate();
        }

    }
}
