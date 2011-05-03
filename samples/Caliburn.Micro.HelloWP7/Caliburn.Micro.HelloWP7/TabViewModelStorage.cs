namespace Caliburn.Micro.HelloWP7 {
    public class TabViewModelStorage : StorageHandler<TabViewModel> {
        public override void Configure() {
            Id(x => x.DisplayName);

            Property(x => x.Text)
                .InPhoneState()
                .RestoreAfterActivation();
        }
    }
}