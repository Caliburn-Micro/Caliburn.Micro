using System;

namespace Features.CrossPlatform
{
    public interface INavigationService
    {
        void NavigateToViewModel(Type viewModel);
    }
}
