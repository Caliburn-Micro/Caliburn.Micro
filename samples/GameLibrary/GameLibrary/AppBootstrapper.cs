namespace GameLibrary {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using System.Linq;
    using System.Windows.Controls;
    using Caliburn.Micro;
    using Framework;

    public class AppBootstrapper : Bootstrapper<IShell> {
        CompositionContainer container;

        protected override void StartDesignTime()
        {
            LogManager.GetLog = type => new SimpleLog(type);
            base.StartDesignTime();
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

            var baseLocator = ViewLocator.LocateForModelType;
            ViewLocator.LocateForModelType = (modelType, displayLocation, context) => {
                if (modelType.FullName.StartsWith("GameLibrary.Model")) {
                    var viewName = modelType.FullName.Replace("GameLibrary.Model", "GameLibrary.Views");
                    var viewType = (from assembly in AssemblySource.Instance
                                    from type in assembly.GetExportedTypes()
                                    where type.FullName == viewName
                                    select type).FirstOrDefault();

                    if(viewType != null)
                        return ViewLocator.GetOrCreateViewType(viewType);
                }

                return baseLocator(modelType, displayLocation, context);
            };
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
    }
}