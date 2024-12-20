using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace Caliburn.Micro.Maui
{
    /// <summary>
    /// A slimmed down version of the normal Caliburn Application for MAUI, used to register the navigation service and set up the initial view.
    /// </summary>
    public abstract class MauiApplication : Application
    {
        private bool isInitialized;


        /// <summary>
        /// Start the framework.
        /// </summary>
        protected void Initialize() {
            if (isInitialized) {
                return;
            }

            isInitialized = true;

            PlatformProvider.Current = new FormsPlatformProvider(PlatformProvider.Current);

            var baseExtractTypes = AssemblySourceCache.ExtractTypes;

            AssemblySourceCache.ExtractTypes = assembly => {
                var baseTypes = baseExtractTypes(assembly);
                var elementTypes = assembly.ExportedTypes
                    .Where(t => typeof (Element).GetTypeInfo().IsAssignableFrom(t.GetTypeInfo()));

                return baseTypes.Union(elementTypes);
            };

            Configure();
            IoC.GetInstance = GetInstance;
            IoC.GetAllInstances = GetAllInstances;
            IoC.BuildUp = BuildUp;

            AssemblySource.Instance.Refresh();
        }

        /// <summary>
        /// Override to configure the framework and setup your IoC container.
        /// </summary>
        protected virtual void Configure()
        {
        }

        /// <summary>
        /// The root frame of the application.
        /// </summary>
        protected NavigationPage RootNavigationPage { get; private set; }

        /// <summary>
        /// Creates the root frame used by the application.
        /// </summary>
        /// <returns>The frame.</returns>
        protected virtual NavigationPage CreateApplicationPage()
        {
            return new NavigationPage();
        }

        /// <summary>
        /// Allows you to trigger the creation of the RootFrame from Configure if necessary.
        /// </summary>
        protected virtual void PrepareViewFirst()
        {
            if (RootNavigationPage != null)
                return;

            RootNavigationPage = CreateApplicationPage();
            PrepareViewFirst(RootNavigationPage);
        }

        /// <summary>
        /// Override this to register a navigation service.
        /// </summary>
        /// <param name="navigationPage">The root frame of the application.</param>
        protected virtual void PrepareViewFirst(NavigationPage navigationPage)
        {
        }

        /// <summary>
        /// Creates the root frame and navigates to the specified view.
        /// </summary>
        /// <param name="viewType">The view type to navigate to.</param>
        protected async Task DisplayRootView(Type viewType)
        {
            PrepareViewFirst();

            // Normally we'd just do everything through NavigationPage
            // and listen for events like all the other navigation services
            // Xamarin Forms acts differentl

            var navigationService = IoC.Get<INavigationService>();

            await navigationService.NavigateToViewAsync(viewType);

            MainPage = RootNavigationPage;
        }

        /// <summary>
        /// Creates the root frame and navigates to the specified view.
        /// </summary>
        /// <typeparam name="T">The view type to navigate to.</typeparam>
        protected Task DisplayRootView<T>()
        {
            return DisplayRootView(typeof(T));
        }

        /// <summary>
        /// Locates the view model, locates the associate view, binds them and shows it as the root view.
        /// </summary>
        /// <param name="viewModelType">The view model type.</param>
        protected async Task DisplayRootViewForAsync(Type viewModelType)
        {
            var viewModel = IoC.GetInstance(viewModelType, null);
            var view = ViewLocator.LocateForModel(viewModel, null, null);

            if (!(view is Page page))
                throw new NotSupportedException(String.Format("{0} does not inherit from {1}.", view.GetType(), typeof(Page)));

            ViewModelBinder.Bind(viewModel, view, null);

            if (viewModel is IActivate activator)
            {
                await activator.ActivateAsync();
            }

            MainPage = page;
        }

        /// <summary>
        /// Locates the view model, locates the associate view, binds them and shows it as the root view.
        /// </summary>
        /// <typeparam name="T">The view model type.</typeparam>
        protected Task DisplayRootViewForAsync<T>()
        {
            return DisplayRootViewForAsync(typeof(T));
        }

        /// <summary>
        /// Override this to provide an IoC specific implementation.
        /// </summary>
        /// <param name="service">The service to locate.</param>
        /// <param name="key">The key to locate.</param>
        /// <returns>The located service.</returns>
        protected virtual object GetInstance(Type service, string key)
        {
            return System.Activator.CreateInstance(service);
        }

        /// <summary>
        /// Override this to provide an IoC specific implementation
        /// </summary>
        /// <param name="service">The service to locate.</param>
        /// <returns>The located services.</returns>
        protected virtual IEnumerable<object> GetAllInstances(Type service)
        {
            return new[] { System.Activator.CreateInstance(service) };
        }

        /// <summary>
        /// Override this to provide an IoC specific implementation.
        /// </summary>
        /// <param name="instance">The instance to perform injection on.</param>
        protected virtual void BuildUp(object instance)
        {
        }
    }
}
