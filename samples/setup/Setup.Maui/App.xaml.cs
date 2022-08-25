using Caliburn.Micro.Maui;
using Setup.Maui.ViewModels;

namespace Setup.Maui
{
    public partial class App : Caliburn.Micro.Maui.MauiApplication
    {
        public App()
        {
            InitializeComponent();

            Initialize();

            DisplayRootViewForAsync<MainViewModel>();
        }
    }
}
