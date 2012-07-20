namespace Caliburn.Micro.HelloWP71 {
    public class PivotPageModelStorage : StorageHandler<PivotPageViewModel> {
        public override void Configure() {
            this.ActiveItemIndex()
                .InPhoneState()
                .RestoreAfterViewLoad();
        }
    }
}