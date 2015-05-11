using System;
using System.Linq;
using Xamarin.Forms;

namespace Caliburn.Micro.Xamarin.Forms
{
    /// <summary>
    /// A slimmed down version of the normal Caliburn Application for Xamarin Forms, used to register the navigation service and set up the initial view.
    /// </summary>
    public class FormsApplication : Application
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

            var baseExtractTypes = AssemblySourceCache.ExtractTypes;

            AssemblySourceCache.ExtractTypes = assembly => {
                var baseTypes = baseExtractTypes(assembly);
                var elementTypes = assembly.GetExportedTypes()
                    .Where(t => typeof (Element).IsAssignableFrom(t));

                return baseTypes.Union(elementTypes);
            };

            AssemblySource.Instance.Refresh();
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
            MainPage = RootNavigationPage;
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
        protected void DisplayRootView(Type viewType)
        {
            PrepareViewFirst();

            // Normally we'd just do everything through NavigationPage
            // and listen for events like all the other navigation services
            // Xamarin Forms acts differentl

            var navigationService = IoC.Get<INavigationService>();

            navigationService.NavigateToViewAsync(viewType);
        }

        /// <summary>
        /// Creates the root frame and navigates to the specified view.
        /// </summary>
        /// <typeparam name="T">The view type to navigate to.</typeparam>
        protected void DisplayRootView<T>()
        {
            DisplayRootView(typeof(T));
        }

        /// <summary>
        /// Locates the view model, locates the associate view, binds them and shows it as the root view.
        /// </summary>
        /// <param name="viewModelType">The view model type.</param>
        protected void DisplayRootViewFor(Type viewModelType)
        {
            var viewModel = IoC.GetInstance(viewModelType, null);
            var view = ViewLocator.LocateForModel(viewModel, null, null);

            var page = view as Page;

            if (page == null)
                throw new NotSupportedException(String.Format("{0} does not inherit from {1}.", view.GetType(), typeof(Page)));

            ViewModelBinder.Bind(viewModel, view, null);

            var activator = viewModel as IActivate;
            if (activator != null)
                activator.Activate();

            MainPage = page;
        }

        /// <summary>
        /// Locates the view model, locates the associate view, binds them and shows it as the root view.
        /// </summary>
        /// <typeparam name="T">The view model type.</typeparam>
        protected void DisplayRootViewFor<T>()
        {
            DisplayRootViewFor(typeof(T));
        }
    }
}
