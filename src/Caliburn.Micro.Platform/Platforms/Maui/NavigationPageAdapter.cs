using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Maui.Controls;

namespace Caliburn.Micro.Maui {
    /// <summary>
    /// Adapater around NavigationPage that implements INavigationService.
    /// </summary>
    public class NavigationPageAdapter : INavigationService {
        private readonly NavigationPage navigationPage;
        private Page currentPage;

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationPageAdapter"/> class.
        /// </summary>
        /// <param name="navigationPage">The navigation page to adapat.</param>
        public NavigationPageAdapter(NavigationPage navigationPage) {
            this.navigationPage = navigationPage;

            navigationPage.Pushed += OnPushed;
            navigationPage.Popped += OnPopped;
            navigationPage.PoppedToRoot += OnPoppedToRoot;
        }

        /// <summary>
        /// Asynchronously removes the top Page from the navigation stack, with optional animation.
        /// </summary>
        /// <param name="animated">Animate the transition.</param>
        /// <returns>The asynchrous task representing the transition.</returns>
        public async Task GoBackAsync(bool animated = true) {
            if (!await CanCloseAsync()) {
                return;
            }

            await navigationPage.PopAsync(animated);
        }

        /// <summary>
        /// Pops all but the root Page off the navigation stack.
        /// </summary>
        /// <param name="animated">Animate the transition.</param>
        /// <returns>The asynchrous task representing the transition.</returns>
        public async Task GoBackToRootAsync(bool animated = true) {
            if (!await CanCloseAsync()) {
                return;
            }

            await navigationPage.PopToRootAsync(animated);
        }

        /// <summary>
        ///  A task for asynchronously pushing a view for the given view model onto the navigation stack, with optional animation.
        /// </summary>
        /// <param name="viewModelType">The type of the view model.</param>
        /// <param name="parameter">The paramter to pass to the view model.</param>
        /// <param name="animated">Animate the transition.</param>
        /// <returns>The asynchrous task representing the transition.</returns>
        public async Task NavigateToViewModelAsync(Type viewModelType, object parameter = null, bool animated = true) {
            if (!await CanCloseAsync()) {
                return;
            }

            await PushAsync(ViewLocator.LocateForModelType(viewModelType, null, null), parameter, animated);
        }

        /// <summary>
        ///  A task for asynchronously pushing a page for the given view model onto the navigation stack, with optional animation.
        /// </summary>
        /// <typeparam name="T">The type of the view model.</typeparam>
        /// <param name="parameter">The paramter to pass to the view model.</param>
        /// <param name="animated">Animate the transition.</param>
        /// <returns>The asynchrous task representing the transition.</returns>
        public async Task NavigateToViewModelAsync<T>(object parameter = null, bool animated = true)
            => await NavigateToViewModelAsync(typeof(T), parameter, animated);

        /// <summary>
        ///  A task for asynchronously pushing a view onto the navigation stack, with optional animation.
        /// </summary>
        /// <param name="viewType">The type of the view.</param>
        /// <param name="parameter">The paramter to pass to the view model.</param>
        /// <param name="animated">Animate the transition.</param>
        /// <returns>The asynchrous task representing the transition.</returns>
        public async Task NavigateToViewAsync(Type viewType, object parameter = null, bool animated = true) {
            if (!await CanCloseAsync()) {
                return;
            }

            await PushAsync(ViewLocator.GetOrCreateViewType(viewType), parameter, animated);
        }

        /// <summary>
        ///  A task for asynchronously pushing a view onto the navigation stack, with optional animation.
        /// </summary>
        /// <typeparam name="T">The type of the view.</typeparam>
        /// <param name="parameter">The paramter to pass to the view model.</param>
        /// <param name="animated">Animate the transition.</param>
        /// <returns>The asynchrous task representing the transition.</returns>
        public async Task NavigateToViewAsync<T>(object parameter = null, bool animated = true)
            => await NavigateToViewAsync(typeof(T), parameter, animated);

        /// <summary>
        /// Allow Xamarin to navigate to a ViewModel backed by a view which is of type <see cref="ContentView"/> by adapting the result
        /// to a <see cref="ContentPage"/>.
        /// </summary>
        /// <param name="view">The view to be adapted.</param>
        /// <param name="viewModel">The view model which is bound to the view.</param>
        /// <returns>The adapted ContentPage.</returns>
        protected virtual ContentPage CreateContentPage(ContentView view, object viewModel) {
            var page = new ContentPage { Content = view };
            if (!(viewModel is IHaveDisplayName hasDiplayName)) {
                return page;
            }

            string path = "DisplayName";
            PropertyInfo property = typeof(IHaveDisplayName).GetRuntimeProperty(path);
            ConventionManager.SetBinding(viewModel.GetType(), path, property, page, null, Page.TitleProperty);
            page.BindingContext = viewModel;

            return page;
        }

        /// <summary>
        /// Apply logic to deactivate the active view when it is popped off the navigation stack.
        /// </summary>
        /// <param name="view">the previously active view.</param>
        protected virtual async Task DeactivateViewAsync(BindableObject view) {
            if (view?.BindingContext is IDeactivate deactivate) {
                await deactivate.DeactivateAsync(false);
            }
        }

        /// <summary>
        /// Apply logic to activate a view when it is popped onto the navigation stack.
        /// </summary>
        /// <param name="view">the view to activate.</param>
        protected virtual async Task ActivateViewAsync(BindableObject view) {
            if (view?.BindingContext is IActivate activator) {
                await activator.ActivateAsync();
            }
        }

        /// <summary>
        /// Attempts to inject query string parameters from the view into the view model.
        /// </summary>
        /// <param name="viewModel"> The view model.</param>
        /// <param name="parameter"> The parameter.</param>
        protected virtual void TryInjectParameters(object viewModel, object parameter) {
            Type viewModelType = viewModel.GetType();
            if (parameter is string stringParameter && stringParameter.StartsWith("caliburn://", StringComparison.OrdinalIgnoreCase)) {
                var uri = new Uri(stringParameter);
                if (string.IsNullOrEmpty(uri.Query)) {
                    return;
                }

                Dictionary<string, string> decorder = HttpUtility.ParseQueryString(uri.Query);
                foreach (KeyValuePair<string, string> pair in decorder) {
                    PropertyInfo localProperty = viewModelType.GetPropertyCaseInsensitive(pair.Key);
                    if (localProperty == null) {
                        continue;
                    }

                    localProperty.SetValue(viewModel, MessageBinder.CoerceValue(localProperty.PropertyType, pair.Value, null));
                }

                return;
            }

            if (parameter is IDictionary<string, object> dictionaryParameter) {
                foreach (KeyValuePair<string, object> pair in dictionaryParameter) {
                    PropertyInfo localProperty = viewModelType.GetPropertyCaseInsensitive(pair.Key);
                    if (localProperty == null) {
                        continue;
                    }

                    localProperty.SetValue(viewModel, MessageBinder.CoerceValue(localProperty.PropertyType, pair.Value, null));
                }

                return;
            }

            PropertyInfo property = viewModelType.GetPropertyCaseInsensitive("Parameter");
            if (property == null) {
                return;
            }

            property.SetValue(viewModel, MessageBinder.CoerceValue(property.PropertyType, parameter, null));
        }

        private async void OnPoppedToRoot(object sender, NavigationEventArgs e) {
            await DeactivateViewAsync(currentPage);
            await ActivateViewAsync(navigationPage.CurrentPage);

            currentPage = navigationPage.CurrentPage;
        }

        private async void OnPopped(object sender, NavigationEventArgs e) {
            await DeactivateViewAsync(currentPage);
            await ActivateViewAsync(navigationPage.CurrentPage);

            currentPage = navigationPage.CurrentPage;
        }

        private async void OnPushed(object sender, NavigationEventArgs e) {
            await DeactivateViewAsync(currentPage);
            await ActivateViewAsync(navigationPage.CurrentPage);

            currentPage = navigationPage.CurrentPage;
        }

        private async Task PushAsync(Element view, object parameter, bool animated) {
            var page = view as Page;
            if (page == null && !(view is ContentView)) {
                throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "{0} does not inherit from either {1} or {2}.", view.GetType(), typeof(Page), typeof(ContentView)));
            }

            object viewModel = ViewModelLocator.LocateForView(view);
            if (viewModel != null) {
                TryInjectParameters(viewModel, parameter);
                ViewModelBinder.Bind(viewModel, view, null);
                if (viewModel is IActivate activator) {
                    await activator.ActivateAsync();
                }
            }

            if (view is ContentView contentView) {
                page = CreateContentPage(contentView, viewModel);
            }

            await navigationPage.PushAsync(page, animated);
        }

        private async Task<bool> CanCloseAsync()
            => !(navigationPage.CurrentPage?.BindingContext is IGuardClose guard) ||
               await guard.CanCloseAsync(CancellationToken.None);
    }
}
