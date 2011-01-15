namespace Caliburn.Micro.HelloWP7 {
    using System;
    using System.Linq;

    [SurviveTombstone]
    public class PageTwoViewModel : Conductor<IScreen>.Collection.OneActive {
        readonly Func<TabViewModel> createTab;
        readonly PivotFix<IScreen> pivotFix;

        public PageTwoViewModel(Func<TabViewModel> createTab) {
            this.createTab = createTab;
            pivotFix = new PivotFix<IScreen>(this);
        }

        public int NumberOfTabs { get; set; }

        protected override void OnInitialize() {
            Enumerable.Range(1, NumberOfTabs).Apply(x => {
                var tab = createTab();
                tab.DisplayName = "Item " + x;
                Items.Add(tab);
            });

            ActivateItem(Items[0]);
        }

        protected override void OnViewLoaded(object view) {
            pivotFix.OnViewLoaded(view, base.OnViewLoaded);
        }

        protected override void ChangeActiveItem(IScreen newItem, bool closePrevious) {
            pivotFix.ChangeActiveItem(newItem, closePrevious, base.ChangeActiveItem);
        }
    }
}