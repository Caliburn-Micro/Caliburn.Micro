namespace GameLibrary {
    using Caliburn.Micro;
    using Framework;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    public class AppBootstrapper : BootstrapperBase {
        CompositionContainer container;

        public AppBootstrapper()
        {
            LogManager.GetLog = type => new DebugLog(type);
            Start();
        }

        protected override void Configure() {
            ConfigureContainer();
            ConfigureConventions();
        }

        void ConfigureContainer() {
            container = CompositionHost.Initialize(
                new AggregateCatalog(
                    AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog>()
                    )
                );

            var batch = new CompositionBatch();

            batch.AddExportedValue<IWindowManager>(new WindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            batch.AddExportedValue(container);

            container.Compose(batch);
        }

        void ConfigureConventions() {
            ConventionManager.AddElementConvention<BusyIndicator>(BusyIndicator.IsBusyProperty, "IsBusy", "IsBusyChanged");
            ConventionManager.AddElementConvention<Rating>(Rating.ValueProperty, "Value", "ValueChanged");

            ViewLocator.NameTransformer.AddRule(
                @"(?<namespace>(.*\.)*)Model\.(?<basename>[A-Za-z]\w*)",
                @"${namespace}Views.${basename}",
                @"(.*\.)*Model\.[A-Za-z]\w*"
                );
        }

        protected override object GetInstance(Type serviceType, string key) {
            var contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
            var exports = container.GetExportedValues<object>(contract);

            if(exports.Any())
                return exports.First();

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType) {
            return container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
        }

        protected override void BuildUp(object instance) {
            container.SatisfyImportsOnce(instance);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<IShell>();
        }
    }
}
