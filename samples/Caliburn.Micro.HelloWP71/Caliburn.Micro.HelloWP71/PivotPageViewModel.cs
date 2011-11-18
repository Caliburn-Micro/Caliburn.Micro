namespace Caliburn.Micro.HelloWP71 {
    using System;
    using System.Linq;

    public class PivotPageViewModel : Conductor<IScreen>.Collection.OneActive {
        readonly Func<TabViewModel> createTab;

        public PivotPageViewModel(Func<TabViewModel> createTab) {
            this.createTab = createTab;
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
    }
}