using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Caliburn.Micro.Xamarin.Forms
{
    public interface INavigtionService {

        /// <summary>
        ///   Navigates back.
        /// </summary>
        Task PopAsync();

        Task PushViewModelAsync(Type viewModelType);
        Task PushViewModelAsync<T>();
    }

    public class NavigationPageAdapter : INavigtionService {
        private readonly NavigationPage navigationPage;

        public NavigationPageAdapter(NavigationPage navigationPage) {
            this.navigationPage = navigationPage;

            navigationPage.Pushed += OnPushed;
            navigationPage.Popped += OnPopped;
        }

        private void OnPopped(object sender, NavigationEventArgs e) {
            //DeactivateView(e.Page);
            ActivateView(navigationPage.CurrentPage);
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

            ViewLocator.InitializeComponent(view);

            var viewModel = ViewModelLocator.LocateForView(view);
            if (viewModel == null)
                return;

            //TryInjectParameters(viewModel, currentParameter);

            ViewModelBinder.Bind(viewModel, view, null);

            var activator = viewModel as IActivate;

            if (activator != null)
            {
                activator.Activate();
            }
        }

        private void OnPushed(object sender, NavigationEventArgs e) {
            //DeactivateView(navigationPage.CurrentPage);
            ActivateView(e.Page);
        }

        /// <summary>
        ///   Navigates back.
        /// </summary>
        public Task PopAsync() {
            return navigationPage.PopAsync();
        }

        public async Task PushViewModelAsync(Type viewModelType)
        {
            var view = ViewLocator.LocateForModelType(viewModelType, null, null);
            var page = view as Page;

            if (page == null)
                throw new NotSupportedException(String.Format("{0} does not inherit from {1}.", view.GetType(), typeof(Page)));

            await navigationPage.PushAsync(page);
        }

        public Task PushViewModelAsync<T>() {
           return PushViewModelAsync(typeof(T));
        }
    }
}
