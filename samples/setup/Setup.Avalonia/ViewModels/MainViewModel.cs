using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace Setup.Avalonia.ViewModels
{
    public class MainViewModel : Screen
    {
        public MainViewModel() { 
            DisplayName = "Welcome to Main View Model in Avalonia!";
        }
        protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            await base.OnInitializeAsync(cancellationToken);

        }
    }
}
