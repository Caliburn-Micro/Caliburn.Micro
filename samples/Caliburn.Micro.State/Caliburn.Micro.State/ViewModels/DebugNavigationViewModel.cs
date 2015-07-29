using System;
using Windows.UI.Xaml.Navigation;
using Caliburn.Micro.State.Views;

namespace Caliburn.Micro.State.ViewModels
{
    public class DebugNavigationViewModel : Screen
    {
        private readonly INavigationService _navigationService;

        public DebugNavigationViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void Insert()
        {
            var view = typeof (RepositoryDetailsView);
            var parameter = _navigationService.UriFor<RepositoryDetailsViewModel>()
                .WithParam(v => v.Owner, "caliburn-micro")
                .WithParam(v => v.Name, "caliburn.micro")
                .BuildUri()
                .AbsoluteUri;

            _navigationService.ForwardStack.Add(new PageStackEntry(view, parameter, null));

            NotifyOfPropertyChange(() => CanGoForward);
        }

        public bool CanGoBack
        {
            get { return _navigationService.CanGoBack; }
        }

        public void GoBack()
        {
            _navigationService.GoBack();
        }

        public bool CanGoForward
        {
            get { return _navigationService.CanGoForward; }
        }

        public void GoForward()
        {
            _navigationService.GoForward();
        }

        public void Clear()
        {
            _navigationService.BackStack.Clear();
            _navigationService.ForwardStack.Clear();

            NotifyOfPropertyChange(() => CanGoBack);
            NotifyOfPropertyChange(() => CanGoForward);
        }
    }
}
