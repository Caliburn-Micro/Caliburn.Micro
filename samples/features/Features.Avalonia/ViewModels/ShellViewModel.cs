using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls;
using Caliburn.Micro;
using Features.Avalonia.ViewModels;

namespace Features.Avalonia.ViewModels
{
    public class ShellViewModel : Screen
    {
        private readonly SimpleContainer _container;

        public ShellViewModel(SimpleContainer container)
        {
            _container = container;
        }
        protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            await base.OnInitializeAsync(cancellationToken);

            var menuVM = IoC.GetInstance(typeof(MenuViewModel), null) as MenuViewModel;

        }
    }

}
