namespace Caliburn.Micro.HelloWP7 {
    public class PivotPageModelStorage : StorageHandler<PivotPageViewModel> {
        public override void Configure() {
            this.ActiveItemIndex()
                .InPhoneState()
                .RestoreAfterViewLoad();
        }
    }
}