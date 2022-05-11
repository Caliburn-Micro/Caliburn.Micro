using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Features.Avalonia.ViewModels;

namespace Features.Avalonia.ViewModels
{
    public class ShellViewModel : Screen
    {
        private readonly SimpleContainer container;
        private INavigationService navigationService;

        public ShellViewModel(SimpleContainer container, INavigationService navigation)
        {
            this.container = container;
            navigationService = navigation;
        }

        public void RegisterFrame(Frame frame)
        {
            navigationService.NavigateToViewModel(typeof(MenuViewModel));
        }
    }

}
