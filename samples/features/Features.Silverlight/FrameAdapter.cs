using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Caliburn.Micro;

namespace Features.CrossPlatform
{
    public class FrameAdapter : INavigationService
    {
        private readonly Frame frame;

        public FrameAdapter(Frame frame)
        {
            this.frame = frame;

            frame.Navigated += OnNavigated;
        }

        public void NavigateToViewModel<T>()
        {
            NavigateToViewModel(typeof (T));
        }

        public void NavigateToViewModel(Type viewModel)
        {
            var viewType = ViewLocator.LocateTypeForModelType(viewModel, null, null);

            var packUri = ViewLocator.DeterminePackUriFromType(viewModel, viewType);

            var uri = new Uri(packUri, UriKind.Relative);

            frame.Navigate(uri);
        }

        protected virtual void OnNavigated(object sender, NavigationEventArgs e)
        {
            if (e.Uri.IsAbsoluteUri || e.Content == null)
            {
                return;
            }

            ViewLocator.InitializeComponent(e.Content);

            var viewModel = ViewModelLocator.LocateForView(e.Content);
            if (viewModel == null)
            {
                return;
            }

            var page = e.Content as Page;
            if (page == null)
            {
                throw new ArgumentException("View '" + e.Content.GetType().FullName + "' should inherit from PhoneApplicationPage or one of its descendents.");
            }

            TryInjectQueryString(viewModel, page);
            ViewModelBinder.Bind(viewModel, page, null);

            var activator = viewModel as IActivate;
            if (activator != null)
            {
                activator.Activate();
            }

            GC.Collect();
        }

        /// <summary>
        ///   Attempts to inject query string parameters from the view into the view model.
        /// </summary>
        /// <param name="viewModel"> The view model. </param>
        /// <param name="page"> The page. </param>
        protected virtual void TryInjectQueryString(object viewModel, Page page)
        {
            var viewModelType = viewModel.GetType();

            foreach (var pair in page.NavigationContext.QueryString)
            {
                var property = viewModelType.GetPropertyCaseInsensitive(pair.Key);
                if (property == null)
                {
                    continue;
                }

                property.SetValue(
                    viewModel,
                    MessageBinder.CoerceValue(property.PropertyType, pair.Value, page.NavigationContext),
                    null
                    );
            }
        }

        public event NavigatedEventHandler Navigated
        {
            add { frame.Navigated += value; }
            remove { frame.Navigated -= value; }
        }

        public bool CanGoBack => frame.CanGoBack;

        public void GoBack() => frame.GoBack();
    }
}
