using System;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.Chrome;
using Caliburn.Micro;
using ReactiveUI;

namespace Setup.Avalonia.ViewModels
{
    public class ShellViewModel : ReactiveShellScreen
    {
        private readonly SimpleContainer container;

        public ShellViewModel(SimpleContainer container)
        {
            this.container = container;
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
            mainVM.SetHostScreen(this);
            Router.Navigate.Execute(mainVM);
        }

    }
}
