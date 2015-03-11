using System;

namespace Caliburn.Micro.State.ViewModels
{
    public class PageViewModelBase : Screen
    {
        public PageViewModelBase(INavigationService navigationService)
        {
            Navigation = new DebugNavigationViewModel(navigationService);
        }

        public DebugNavigationViewModel Navigation
        {
            get; private set;
        }
    }
}
