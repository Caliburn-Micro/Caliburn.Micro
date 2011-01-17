namespace Caliburn.Micro.HelloScreens.Shell
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using System.Linq;
    using System.Windows;
    using Customers;
    using Framework;
    using Orders;

    public class ScreensBootstrapper : Bootstrapper<IShell>
    {
        CompositionContainer container;
        Window mainWindow;
        bool actuallyClosing;

        protected override void Configure() {
            container = CompositionHost.Initialize(
                new AggregateCatalog(
                    AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog>()
                    )
                );

            var batch = new CompositionBatch();

            batch.AddExportedValue<IWindowManager>(new WindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            batch.AddExportedValue<Func<IMessageBox>>(() => container.GetExportedValue<IMessageBox>());
            batch.AddExportedValue<Func<CustomerViewModel>>(() => container.GetExportedValue<CustomerViewModel>());
            batch.AddExportedValue<Func<OrderViewModel>>(() => container.GetExportedValue<OrderViewModel>());
            batch.AddExportedValue(container);

            container.Compose(batch);
        }

        protected override object GetInstance(Type serviceType, string key) {
            var contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
            var exports = container.GetExportedValues<object>(contract);

            if(exports.Count() > 0)
                return exports.First();

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType) {
            return container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
        }

        protected override void BuildUp(object instance) {
            container.SatisfyImportsOnce(instance);
        }

        protected override void DisplayRootView()
        {
            base.DisplayRootView();

            if (Application.IsRunningOutOfBrowser) {
                mainWindow = Application.MainWindow;
                mainWindow.Closing += MainWindowClosing;
            }
        }

        void MainWindowClosing(object sender, ClosingEventArgs e) {
            if (actuallyClosing)
                return;

            e.Cancel = true;

            Execute.OnUIThread(() => {
                var shell = IoC.Get<IShell>();

                shell.CanClose(result => {
                    if(result) {
                        actuallyClosing = true;
                        mainWindow.Close();
                    }
                });
            });
        }
    }
}