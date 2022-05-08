using Caliburn.Micro;
using Setup.Avalonia.ViewModels;

namespace Setup.Avalonia
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
            
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}
