using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Caliburn.Micro.Xamarin.Forms {
    /// <summary>
    /// A slimmed down version of the normal Caliburn Application for Xamarin Forms, used to register the navigation service and set up the initial view.
    /// </summary>
    public class FormsApplication : Application {
        private bool _isInitialized;

        /// <summary>
        /// Gets the root frame of the application.
        /// </summary>
        protected NavigationPage RootNavigationPage { get; private set; }

        /// <summary>
        /// Start the framework.
        /// </summary>
        protected void Initialize() {
            if (_isInitialized) {
                return;
            }

            _isInitialized = true;
            PlatformProvider.Current = new FormsPlatformProvider(PlatformProvider.Current);
            Func<Assembly, System.Collections.Generic.IEnumerable<Type>> baseExtractTypes = AssemblySourceCache.ExtractTypes;
            AssemblySourceCache.ExtractTypes = assembly => {
                System.Collections.Generic.IEnumerable<Type> baseTypes = baseExtractTypes(assembly);
                System.Collections.Generic.IEnumerable<Type> elementTypes = assembly.ExportedTypes
                    .Where(t => typeof(Element).GetTypeInfo().IsAssignableFrom(t.GetTypeInfo()));

                return baseTypes.Union(elementTypes);
            };
            AssemblySource.Instance.Refresh();
        }

        /// <summary>
        /// Creates the root frame used by the application.
        /// </summary>
        /// <returns>The frame.</returns>
        protected virtual NavigationPage CreateApplicationPage()
            => new NavigationPage();

        /// <summary>
        /// Allows you to trigger the creation of the RootFrame from Configure if necessary.
        /// </summary>
        protected virtual void PrepareViewFirst() {
            if (RootNavigationPage != null) {
                return;
            }

            RootNavigationPage = CreateApplicationPage();
            PrepareViewFirst(RootNavigationPage);
        }

        /// <summary>
        /// Override this to register a navigation service.
        /// </summary>
        /// <param name="navigationPage">The root frame of the application.</param>
        protected virtual void PrepareViewFirst(NavigationPage navigationPage) {
        }

        /// <summary>
        /// Creates the root frame and navigates to the specified view.
        /// </summary>
        /// <param name="viewType">The view type to navigate to.</param>
        protected async Task DisplayRootView(Type viewType) {
            PrepareViewFirst();

            // Normally we'd just do everything through NavigationPage
            // and listen for events like all the other navigation services
            // Xamarin Forms acts differentl
            INavigationService navigationService = IoC.Get<INavigationService>();

            await navigationService.NavigateToViewAsync(viewType);

            MainPage = RootNavigationPage;
        }

        /// <summary>
        /// Creates the root frame and navigates to the specified view.
        /// </summary>
        /// <typeparam name="T">The view type to navigate to.</typeparam>
        protected Task DisplayRootView<T>() => DisplayRootView(typeof(T));

        /// <summary>
        /// Locates the view model, locates the associate view, binds them and shows it as the root view.
        /// </summary>
        /// <param name="viewModelType">The view model type.</param>
        protected async Task DisplayRootViewForAsync(Type viewModelType) {
            object viewModel = IoC.GetInstance(viewModelType, null);
            Element view = ViewLocator.LocateForModel(viewModel, null, null);

            if (!(view is Page page)) {
                throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "{0} does not inherit from {1}.", view.GetType(), typeof(Page)));
            }

            ViewModelBinder.Bind(viewModel, view, null);

            if (viewModel is IActivate activator) {
                await activator.ActivateAsync();
            }

            MainPage = page;
        }

        /// <summary>
        /// Locates the view model, locates the associate view, binds them and shows it as the root view.
        /// </summary>
        /// <typeparam name="T">The view model type.</typeparam>
        protected Task DisplayRootViewForAsync<T>()
            => DisplayRootViewForAsync(typeof(T));
    }
}
