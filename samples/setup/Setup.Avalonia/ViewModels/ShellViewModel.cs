using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.Chrome;
using Caliburn.Micro;

namespace Setup.Avalonia.ViewModels
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
        protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            await base.OnInitializeAsync(cancellationToken);

            DisplayName = "Welcome to Caliburn.Micro.Avalonia!";
        }

        public void btnSayHello()
        {
            DisplayName = "Hello";
        }
        
        public void RegisterFrame(SplitView frame)
        {
            int x = 1;
            //navigationService = new FrameAdapter(frame);

            //container.Instance(navigationService);
            
            //await navigationService.NavigateToViewModelAsync(typeof(MainViewModel));
        }
    }
}
