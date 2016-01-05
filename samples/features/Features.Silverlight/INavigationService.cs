using System;
using System.Windows.Navigation;

namespace Features.CrossPlatform
{
    public interface INavigationService
    {
        void NavigateToViewModel<T>();
        void NavigateToViewModel(Type viewModel);
        event NavigatedEventHandler Navigated;

        bool CanGoBack { get; }
        void GoBack();
    }
}
