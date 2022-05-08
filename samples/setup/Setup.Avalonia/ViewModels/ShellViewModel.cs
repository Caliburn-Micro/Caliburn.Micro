using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls.Chrome;
using Caliburn.Micro;

namespace Setup.Avalonia.ViewModels
{
    public class ShellViewModel : Screen
    {
        protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            await base.OnInitializeAsync(cancellationToken);

            DisplayName = "Welcome to Caliburn.Micro.Avalonia!";
        }
    }
}
