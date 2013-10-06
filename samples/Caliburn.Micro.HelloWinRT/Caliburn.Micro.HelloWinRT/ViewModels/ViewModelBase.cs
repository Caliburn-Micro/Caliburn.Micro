namespace Caliburn.Micro.WinRT.Sample.ViewModels
{
    /// <summary>
    /// Base view model for all our main screens, the method GoBack will be bound via convention
    /// to the back button and only display when it can go back due to the template of the back 
    /// button (Collapsed when Disabled)
    /// </summary>
    public abstract class ViewModelBase : Screen
    {
        private static readonly ILog Log = LogManager.GetLog(typeof (ViewModelBase));
        private readonly INavigationService navigationService;

        protected ViewModelBase(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        protected override void OnInitialize()
        {
            Log.Info("OnInitialize {0}", GetType().Name);
        }

        protected override void OnActivate()
        {
            Log.Info("OnActivate {0}", GetType().Name);
        }

        protected override void OnDeactivate(bool close)
        {
            Log.Info("OnDeactivate({0}) {1}", close, GetType().Name);
        }

        protected override void OnViewAttached(object view, object context)
        {
            Log.Info("OnViewAttached {0}", GetType().Name);
        }

        protected override void OnViewLoaded(object view)
        {
            Log.Info("OnViewLoaded {0}", GetType().Name);
        }

        protected override void OnViewReady(object view)
        {
            Log.Info("OnViewReady {0}", GetType().Name);
        }

        public void GoBack()
        {
            navigationService.GoBack();
        }

        public bool CanGoBack
        {
            get
            {
                return navigationService.CanGoBack;
            }
        }
    }
}
