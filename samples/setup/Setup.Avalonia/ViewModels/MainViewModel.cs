using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace Setup.Avalonia.ViewModels
{
    public class MainViewModel:Screen
    {
        protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            await base.OnInitializeAsync(cancellationToken);

            DisplayName = "Welcome to Main View Model in Avalonia!";
        }
    }
}
