namespace Caliburn.Micro.HelloWP71.ViewModels {
    using System;
    using System.Linq;

    public class PivotPageViewModel : Conductor<IScreen>.Collection.OneActive {
        private static readonly ILog Log = LogManager.GetLog(typeof(PivotPageViewModel));
        readonly Func<TabViewModel> createTab;

        public PivotPageViewModel(Func<TabViewModel> createTab) {
            this.createTab = createTab;
        }

        public int NumberOfTabs { get; set; }

        protected override void OnInitialize() {
            Log.Info("OnInitialize {0}", GetType().Name);

            Enumerable.Range(1, NumberOfTabs).Apply(x => {
                var tab = createTab();
                tab.DisplayName = "Item " + x;
                Items.Add(tab);
            });

            ActivateItem(Items[0]);
        }

        protected override void OnActivate()
        {
            Log.Info("OnActivate {0}", GetType().Name);
        }

        protected override void OnDeactivate(bool close)
        {
            Log.Info("OnDeactivate({0}) {1}", close, GetType().Name);
        }

        protected override void OnViewAttached(object view, object context)
        {
            Log.Info("OnViewAttached {0}", GetType().Name);
        }

        protected override void OnViewLoaded(object view)
        {
            Log.Info("OnViewLoaded {0}", GetType().Name);
        }

        protected override void OnViewReady(object view)
        {
            Log.Info("OnViewReady {0}", GetType().Name);
        }
    }
}