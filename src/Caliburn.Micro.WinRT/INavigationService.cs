using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Caliburn.Micro
{
    public interface INavigationService
    {
        event NavigatedEventHandler Navigated;
        event NavigatingCancelEventHandler Navigating;
        event NavigationFailedEventHandler NavigationFailed;
        event NavigationStoppedEventHandler NavigationStopped;
        Type SourcePageType { get; set; }
        Type CurrentSourcePageType { get; }
        bool CanGoForward { get; }
        bool CanGoBack { get; }
        bool Navigate(Type sourcePageType);
        bool Navigate(Type sourcePageType, object parameter);
        void GoForward();
        void GoBack();
    }

    public class FrameAdapter : INavigationService
    {
        private readonly Frame frame;
        private readonly bool treatViewAsLoaded;
        private event NavigatingCancelEventHandler ExternalNavigatingHandler = delegate { };

        public FrameAdapter(Frame frame, bool treatViewAsLoaded = false)
        {
            this.frame = frame;
            this.treatViewAsLoaded = treatViewAsLoaded;

            this.frame.Navigating += OnNavigating;
            this.frame.Navigated += OnNavigated;
        }

        protected virtual void OnNavigating(object sender, NavigatingCancelEventArgs e)
        {
            ExternalNavigatingHandler(sender, e);

            if (e.Cancel)
                return;

            var view = frame.Content as FrameworkElement;

            if (view == null)
                return;

            var guard = view.DataContext as IGuardClose;

            if (guard != null)
            {
                var shouldCancel = false;
                guard.CanClose(result => { shouldCancel = !result; });

                if (shouldCancel)
                {
                    e.Cancel = true;
                    return;
                }
            }

            var deactivator = view.DataContext as IDeactivate;

            if (deactivator != null)
            {
                deactivator.Deactivate(CanCloseOnNavigating(sender, e));
            }
        }

        protected virtual void OnNavigated(object sender, NavigationEventArgs e)
        {
            if (e.Content == null)
                return;

            ViewLocator.InitializeComponent(e.Content);

            var viewModel = ViewModelLocator.LocateForView(e.Content);

            if (viewModel == null)
                return;

            var view = e.Content as Page;

            if (view == null)
            {
                throw new ArgumentException("View '" + e.Content.GetType().FullName + "' should inherit from Page or one of its descendents.");
            }

            if (treatViewAsLoaded)
            {
                view.SetValue(View.IsLoadedProperty, true);
            }

            TryInjectParameter(viewModel, e.Parameter);

            ViewModelBinder.Bind(viewModel, view, null);

            var activator = viewModel as IActivate;

            if (activator != null)
            {
                activator.Activate();
            }

            var viewAware = viewModel as ViewAware;

            if (viewAware != null)
            {
                EventHandler<object> onLayoutUpdate = null;

                onLayoutUpdate = delegate
                {
                    viewAware.OnViewReady(view);
                    view.LayoutUpdated -= onLayoutUpdate;
                };

                view.LayoutUpdated += onLayoutUpdate;
            }

            GC.Collect(); // Why?
        }

        protected virtual void TryInjectParameter(object viewModel, object parameter)
        {
            var viewModelType = viewModel.GetType();
            var property = viewModelType.GetPropertyCaseInsensitive("Parameter");

            if (property == null)
                return;

            property.SetValue(viewModel, MessageBinder.CoerceValue(property.PropertyType, parameter, null));
        }

        /// <summary>
        /// Called to check whether or not to close current instance on navigating.
        /// </summary>
        /// <param name="sender"> The event sender from OnNavigating event. </param>
        /// <param name="e"> The event args from OnNavigating event. </param>
        protected virtual bool CanCloseOnNavigating(object sender, NavigatingCancelEventArgs e)
        {
            return false;
        }

        public event NavigatedEventHandler Navigated
        {
            add
            {
                frame.Navigated += value;
            }
            remove
            {
                frame.Navigated -= value;
            }
        }

        public event NavigatingCancelEventHandler Navigating
        {
            add
            {
                ExternalNavigatingHandler += value;
            }
            remove
            {
                ExternalNavigatingHandler -= value;
            }
        }

        public event NavigationFailedEventHandler NavigationFailed
        {
            add
            {
                frame.NavigationFailed += value;
            }
            remove
            {
                frame.NavigationFailed -= value;
            }
        }

        public event NavigationStoppedEventHandler NavigationStopped
        {
            add
            {
                frame.NavigationStopped += value;
            }
            remove
            {
                frame.NavigationStopped -= value;
            }
        }

        public Type SourcePageType
        {
            get
            {
                return frame.SourcePageType;
            }
            set
            {
                frame.SourcePageType = value;
            }
        }

        public Type CurrentSourcePageType
        {
            get
            {
                return frame.CurrentSourcePageType;
            }
        }

        public bool Navigate(Type sourcePageType)
        {
            return frame.Navigate(sourcePageType);
        }

        public bool Navigate(Type sourcePageType, object parameter)
        {
            return frame.Navigate(sourcePageType, parameter);
        }

        public void GoForward()
        {
            frame.GoForward();
        }

        public void GoBack()
        {
            frame.GoBack();
        }

        public bool CanGoForward
        {
            get
            {
                return frame.CanGoForward;
            }
        }

        public bool CanGoBack
        {
            get
            {
                return frame.CanGoBack;
            }
        }
    }
}
