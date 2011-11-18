namespace Caliburn.Micro.HelloWP71 {
    public class TabViewModelStorage : StorageHandler<TabViewModel> {
        public override void Configure() {
            Id(x => x.DisplayName);

            Property(x => x.Text)
                .InPhoneState()
                .RestoreAfterActivation();
        }
    }
}