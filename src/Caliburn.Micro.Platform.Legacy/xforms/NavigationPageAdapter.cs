using System.Reflection;

namespace Caliburn.Micro.Xamarin.Forms {

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using global::Xamarin.Forms;

    /// <summary>
    /// Adapater around NavigationPage that implements INavigationService
    /// </summary>
    public class NavigationPageAdapter : INavigationService {
        private readonly NavigationPage navigationPage;
        private Page currentPage;

        /// <summary>
        /// Instantiates new instance of NavigationPageAdapter
        /// </summary>
        /// <param name="navigationPage">The navigation page to adapat</param>
        public NavigationPageAdapter(NavigationPage navigationPage) {
            this.navigationPage = navigationPage;

            navigationPage.Pushed += OnPushed;
            navigationPage.Popped += OnPopped;
            navigationPage.PoppedToRoot += OnPoppedToRoot;
        }

        private void OnPoppedToRoot(object sender, NavigationEventArgs e)
        {
            DeactivateView(currentPage);
            ActivateView(navigationPage.CurrentPage);

            currentPage = navigationPage.CurrentPage;
        }

        private void OnPopped(object sender, NavigationEventArgs e)
        {
            DeactivateView(currentPage);
            ActivateView(navigationPage.CurrentPage);

            currentPage = navigationPage.CurrentPage;
        }

        private void OnPushed(object sender, NavigationEventArgs e)
        {
            DeactivateView(currentPage);
            ActivateView(navigationPage.CurrentPage);

            currentPage = navigationPage.CurrentPage;
        }

        /// <summary>
        /// Allow Xamarin to navigate to a ViewModel backed by a view which is of type <see cref="T:Xamarin.Forms.ContentView"/> by adapting the result
        /// to a <see cref="T:Xamarin.Forms.ContentPage"/>.
        /// </summary>
        /// <param name="view">The view to be adapted</param>
        /// <param name="viewModel">The view model which is bound to the view</param>
        /// <returns>The adapted ContentPage</returns>
        protected virtual ContentPage CreateContentPage(ContentView view, object viewModel)
        {
            var page = new ContentPage { Content = view };

            var hasDiplayName = viewModel as IHaveDisplayName;
            if (hasDiplayName != null) {

                var path = "DisplayName";
                var property = typeof(IHaveDisplayName).GetRuntimeProperty(path);
                ConventionManager.SetBinding(viewModel.GetType(), path, property, page, null, Page.TitleProperty);

                page.BindingContext = viewModel;
            }

            return page;
        }

        private static void DeactivateView(BindableObject view)
        {
            var deactivate = view?.BindingContext as IDeactivate;
            deactivate?.Deactivate(false);
        }

        private static void ActivateView(BindableObject view)
        {
            var activate = view?.BindingContext as IActivate;
            activate?.Activate();
        }

        /// <summary>
        /// Asynchronously removes the top <see cref="T:Xamarin.Forms.Page"/> from the navigation stack, with optional animation.
        /// </summary>
        /// <param name="animated">Animate the transition</param>
        /// <returns>The asynchrous task representing the transition</returns>
        public Task GoBackAsync(bool animated = true) {
            
            if (!CanClose())
                return Task.FromResult(false);

            return navigationPage.PopAsync(animated);
        }

        /// <summary>
        /// Pops all but the root <see cref="T:Xamarin.Forms.Page"/> off the navigation stack.
        /// </summary>
        /// <param name="animated">Animate the transition</param>
        /// <returns>The asynchrous task representing the transition</returns>
        public Task GoBackToRootAsync(bool animated = true) 
        {
            if (!CanClose())
                return Task.FromResult(false);

            return navigationPage.PopToRootAsync(animated);
        }

        /// <summary>
        ///  A task for asynchronously pushing a view for the given view model onto the navigation stack, with optional animation.
        /// </summary>
        /// <param name="viewModelType">The type of the view model</param>
        /// <param name="parameter">The paramter to pass to the view model</param>
        /// <param name="animated">Animate the transition</param>
        /// <returns>The asynchrous task representing the transition</returns>
        public Task NavigateToViewModelAsync(Type viewModelType, object parameter = null, bool animated = true)
        {
            if (!CanClose())
                return Task.FromResult(false);

            var view = ViewLocator.LocateForModelType(viewModelType, null, null);

            return PushAsync(view, parameter, animated);
        }

        /// <summary>
        ///  A task for asynchronously pushing a page for the given view model onto the navigation stack, with optional animation.
        /// </summary>
        /// <typeparam name="T">The type of the view model</typeparam>
        /// <param name="parameter">The paramter to pass to the view model</param>
        /// <param name="animated">Animate the transition</param>
        /// <returns>The asynchrous task representing the transition</returns>
        public Task NavigateToViewModelAsync<T>(object parameter = null, bool animated = true)
        {
            if (!CanClose())
                return Task.FromResult(false);

           return NavigateToViewModelAsync(typeof(T), parameter, animated);
        }

        /// <summary>
        ///  A task for asynchronously pushing a view onto the navigation stack, with optional animation.
        /// </summary>
        /// <param name="viewType">The type of the view</param>
        /// <param name="parameter">The paramter to pass to the view model</param>
        /// <param name="animated">Animate the transition</param>
        /// <returns>The asynchrous task representing the transition</returns>
        public Task NavigateToViewAsync(Type viewType, object parameter = null, bool animated = true)
        {
            if (!CanClose())
                return Task.FromResult(false);

            var view = ViewLocator.GetOrCreateViewType(viewType);

            return PushAsync(view, parameter, animated);
        }

        /// <summary>
        ///  A task for asynchronously pushing a view onto the navigation stack, with optional animation.
        /// </summary>
        /// <typeparam name="T">The type of the view</typeparam>
        /// <param name="parameter">The paramter to pass to the view model</param>
        /// <param name="animated">Animate the transition</param>
        /// <returns>The asynchrous task representing the transition</returns>
        public Task NavigateToViewAsync<T>(object parameter = null, bool animated = true)
        {
            if (!CanClose())
                return Task.FromResult(false);

            return NavigateToViewAsync(typeof(T), parameter, animated);
        }

        private Task PushAsync(Element view, object parameter, bool animated)
        {
            var page = view as Page;

            if (page == null && !(view is ContentView))
                throw new NotSupportedException(String.Format("{0} does not inherit from either {1} or {2}.", view.GetType(), typeof(Page), typeof(ContentView)));

            var viewModel = ViewModelLocator.LocateForView(view);

            if (viewModel != null) {
                TryInjectParameters(viewModel, parameter);

                ViewModelBinder.Bind(viewModel, view, null);
            }

            var contentView = view as ContentView;
            if (contentView != null) {
                page = CreateContentPage(contentView, viewModel);
            }

            return navigationPage.PushAsync(page, animated);
        }

        /// <summary>
        /// Attempts to inject query string parameters from the view into the view model.
        /// </summary>
        /// <param name="viewModel"> The view model.</param>
        /// <param name="parameter"> The parameter.</param>
        protected virtual void TryInjectParameters(object viewModel, object parameter)
        {
            var viewModelType = viewModel.GetType();

            var stringParameter = parameter as string;
            var dictionaryParameter = parameter as IDictionary<string, object>;

            if (stringParameter != null && stringParameter.StartsWith("caliburn://"))
            {
                var uri = new Uri(stringParameter);

                if (!String.IsNullOrEmpty(uri.Query)) {

                    var decorder = HttpUtility.ParseQueryString(uri.Query);

                    foreach (var pair in decorder)
                    {
                        var property = viewModelType.GetPropertyCaseInsensitive(pair.Key);

                        if (property == null)
                        {
                            continue;
                        }

                        property.SetValue(viewModel, MessageBinder.CoerceValue(property.PropertyType, pair.Value, null));
                    }
                }
            }
            else if (dictionaryParameter != null) {
                foreach (var pair in dictionaryParameter) {
                    var property = viewModelType.GetPropertyCaseInsensitive(pair.Key);

                    if (property == null) {
                        continue;
                    }

                    property.SetValue(viewModel, MessageBinder.CoerceValue(property.PropertyType, pair.Value, null));
                }
            }
            else
            {
                var property = viewModelType.GetPropertyCaseInsensitive("Parameter");

                if (property == null)
                    return;

                property.SetValue(viewModel, MessageBinder.CoerceValue(property.PropertyType, parameter, null));
            }
        }

        private bool CanClose() {
            var view = navigationPage.CurrentPage;

            var guard = view?.BindingContext as IGuardClose;

            if (guard != null)
            {
                var shouldCancel = false;
                var runningAsync = true;
                guard.CanClose(result => { runningAsync = false; shouldCancel = !result; });
                if (runningAsync)
                    throw new NotSupportedException("Async CanClose is not supported.");

                if (shouldCancel) {
                    return false;
                }
            }

            return true;
        }
    }
}
