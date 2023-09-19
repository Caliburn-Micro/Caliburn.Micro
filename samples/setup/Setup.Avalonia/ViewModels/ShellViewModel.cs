using System;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.Chrome;
using Caliburn.Micro;

namespace Setup.Avalonia.ViewModels
{
    public class ShellViewModel : Conductor<Screen>
    {
        public ShellViewModel()
        {
        }
        protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            await base.OnInitializeAsync(cancellationToken);

            DisplayName = "Welcome to Caliburn.Micro.Avalonia!";
        }
        
        public async Task btnSayHello()
        {
            DisplayName = "Hello";
            
            var mainVM = IoC.GetInstance(typeof(MainViewModel), null) as MainViewModel;
            await ActivateItemAsync(mainVM);
        }

    }
}
