using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Caliburn.Micro.Xamarin.Forms
{
    public interface INavigtionService {

        /// <summary>
        ///   Navigates back.
        /// </summary>
        Task PopAsync(bool animated = true);

        Task PushViewAsync(Type viewType, object parameter = null, bool animated = true);
        Task PushViewAsync<T>(object parameter = null, bool animated = true);

        Task PushViewModelAsync(Type viewModelType, object parameter = null, bool animated = true);
        Task PushViewModelAsync<T>(object parameter = null, bool animated = true);
    }

    public class NavigationPageAdapter : INavigtionService {
        private readonly NavigationPage navigationPage;

        public NavigationPageAdapter(NavigationPage navigationPage) {
            this.navigationPage = navigationPage;
        }

        private void DeactivateView(BindableObject view)
        {
            if (view == null)
                return;

            var deactivate = view.BindingContext as IDeactivate;

            if (deactivate != null)
            {
                deactivate.Deactivate(false);
            }
        }

        private void ActivateView(BindableObject view)
        {
            if (view == null)
                return;

            var activator = view.BindingContext as IActivate;

            if (activator != null)
            {
                activator.Activate();
            }
        }

        /// <summary>
        ///   Navigates back.
        /// </summary>
        public Task PopAsync(bool animated = true)
        {
            return navigationPage.PopAsync(animated);
        }

        public Task PushViewModelAsync(Type viewModelType, object parameter = null, bool animated = true)
        {
            var view = ViewLocator.LocateForModelType(viewModelType, null, null);

            return PushAsync(view, parameter, animated);
        }

        public Task PushViewModelAsync<T>(object parameter = null, bool animated = true)
        {
           return PushViewModelAsync(typeof(T), parameter, animated);
        }

        public Task PushViewAsync(Type viewType, object parameter = null, bool animated = true)
        {
            var view = ViewLocator.GetOrCreateViewType(viewType);

            return PushAsync(view, parameter, animated);
        }

        public Task PushViewAsync<T>(object parameter = null, bool animated = true)
        {
            return PushViewAsync(typeof(T), parameter, animated);
        }

        private Task PushAsync(Element view, object parameter, bool animated)
        {
            var page = view as Page;

            if (page == null)
                throw new NotSupportedException(String.Format("{0} does not inherit from {1}.", view.GetType(), typeof(Page)));

            ViewLocator.InitializeComponent(view);

            var viewModel = ViewModelLocator.LocateForView(view);

            if (viewModel != null) {
                TryInjectParameters(viewModel, parameter);

                ViewModelBinder.Bind(viewModel, view, null);
            }

            page.Appearing += (s, e) => ActivateView(page);
            page.Disappearing += (s, e) => DeactivateView(page);

            return navigationPage.PushAsync(page, animated);
        }

        /// <summary>
        ///   Attempts to inject query string parameters from the view into the view model.
        /// </summary>
        /// <param name="viewModel"> The view model.</param>
        /// <param name="parameter"> The parameter.</param>
        protected virtual void TryInjectParameters(object viewModel, object parameter)
        {
            var viewModelType = viewModel.GetType();

            if (parameter is string && ((string)parameter).StartsWith("caliburn://"))
            {
                var uri = new Uri((string)parameter);

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
            else
            {
                var property = viewModelType.GetPropertyCaseInsensitive("Parameter");

                if (property == null)
                    return;

                property.SetValue(viewModel, MessageBinder.CoerceValue(property.PropertyType, parameter, null));
            }
        }
    }
}
