namespace Caliburn.Micro.HelloWP71.Storage {
    using Caliburn.Micro.HelloWP71.ViewModels;

    public class PivotPageModelStorage : StorageHandler<PivotPageViewModel> {
        public override void Configure() {
            this.ActiveItemIndex()
                .InPhoneState()
                .RestoreAfterViewLoad();
        }
    }
}