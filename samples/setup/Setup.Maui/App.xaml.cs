using Caliburn.Micro;
using Caliburn.Micro.Maui;
using Setup.Maui.Services;
using Setup.Maui.ViewModels;

namespace Setup.Maui
{
    public partial class App : Caliburn.Micro.Maui.MauiApplication
    {
        private SimpleContainer container;
        public App()
        {
            InitializeComponent();

            Initialize();

            DisplayRootViewForAsync<MainViewModel>();
        }
        
        protected void Configure()
        {
            container = new SimpleContainer();
            container.Instance(container);
            // Register your services here
            // For example:
            container.RegisterPerRequest(typeof(IService),null, typeof(MyService));
        }

      
    }
}
