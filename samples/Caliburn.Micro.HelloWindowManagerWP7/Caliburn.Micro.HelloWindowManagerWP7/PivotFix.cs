namespace Caliburn.Micro.HelloWP7 {
    using System;

    //The following code exists to enable resurrection of the ActiveItem when bound to a Pivot control
    //Unfortunately, Pivot has a bug in its SelectedItem/SelectedIndex databinding and it requires custom code to make sure everything happens at the right time.
    //This bug has been reported by several MVPs. Hopefully it will be fixed in the near future.
    public class PivotFix<T> {
        readonly Conductor<T>.Collection.OneActive conductor;
        bool readyToActivate;
        T toActivate;
        bool doneReactivating;

        public PivotFix(Conductor<T>.Collection.OneActive conductor) {
            this.conductor = conductor;
            this.conductor.CloseStrategy = new DefaultCloseStrategy<T>(false);
        }

        public void OnViewLoaded(object view, Action<object> onViewLoadedBase) {
            onViewLoadedBase(view);

            readyToActivate = true;
            if(toActivate != null && !doneReactivating) {
                conductor.ActivateItem(toActivate);
                doneReactivating = true;
            }
        }

        public void ChangeActiveItem(T newItem, bool closePrevious, Action<T, bool> changeActiveItemBase) {
            if (newItem == null)
                return;

            if (!readyToActivate && !doneReactivating)
            {
                if (conductor.Items.IndexOf(newItem) > 0)
                {
                    toActivate = newItem;
                    return;
                }
            }

            changeActiveItemBase(newItem, closePrevious);
        }
    }
}