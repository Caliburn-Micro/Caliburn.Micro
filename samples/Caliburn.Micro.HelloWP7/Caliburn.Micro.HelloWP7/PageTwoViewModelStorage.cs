namespace Caliburn.Micro.HelloWP7 {
    public class PageTwoViewModelStorage : StorageHandler<PageTwoViewModel> {
        public override void Configure() {
            this.ActiveItemIndex()
                .InPhoneState()
                .RestoreAfterViewLoad();
        }
    }
}