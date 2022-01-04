using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific;
using Application = Microsoft.Maui.Controls.Application;
using Caliburn.Micro.Maui;
using Setup.Maui.ViewModels;
using Setup.Maui.Views;

namespace Setup.Maui
{
    public partial class App : FormsApplication
    {
        public App()
        {
            InitializeComponent();
            
            ViewLocator.AddNamespaceMapping("Setup.Maui.Views", "Setup.Maui.ViewModels");
            ViewLocator.AddNamespaceMapping("Setup.Maui.ViewModels", "Setup.Maui.Views");
            ViewLocator.AddNamespaceMapping("Setup.Maui.Views", "Setup.Maui.ViewModels", "ViewModel");
            ViewLocator.AddNamespaceMapping("Setup.Maui.ViewModels", "Setup.Maui.Views", "ViewModel");
            ViewModelLocator.AddNamespaceMapping("Setup.Maui.Views", "Setup.Maui.ViewModels");
            ViewModelLocator.AddNamespaceMapping("Setup.Maui.ViewModels", "Setup.Maui.Views");
            DisplayRootViewForAsync<MainViewModel>();
        }
    }
}
