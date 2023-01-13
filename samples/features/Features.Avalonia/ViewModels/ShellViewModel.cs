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
    public class ShellViewModel : ReactiveShellScreen
    {
        private readonly SimpleContainer _container;

        public ShellViewModel(SimpleContainer container)
        {
            _container = container;
        }
        protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            await base.OnInitializeAsync(cancellationToken);

            DisplayName = "Welcome to Caliburn.Micro.Avalonia!";
        }
    }

}
