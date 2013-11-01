using Caliburn.Micro.WinFormsInterop.ViewModels;
using System.Windows.Forms.Integration;

namespace Caliburn.Micro.WinFormsInterop
{
    public class InteropBootstrapper : BootstrapperBase
    {
        private readonly ElementHost _elementHost;

        public InteropBootstrapper(ElementHost elementHost) : base(false)
        {
            _elementHost = elementHost;
        }

        protected override void StartRuntime()
        {
            base.StartRuntime();

            var viewModel = IoC.GetInstance(typeof(MainViewModel), null);
            var view = ViewLocator.LocateForModel(viewModel, null, null);

            ViewModelBinder.Bind(viewModel, view, null);

            var activator = viewModel as IActivate;
            if (activator != null)
                activator.Activate();

            _elementHost.Child = view;
        }
    }
}
