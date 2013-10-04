using System.Windows;

namespace Caliburn.Micro.Hello
{
    public class HelloBootstrapper : BootstrapperBase
    {
        public HelloBootstrapper()
        {
            Start();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}
