namespace Caliburn.Micro.HelloWP71.Storage {
    using Caliburn.Micro.HelloWP71.ViewModels;

    public class TabViewModelStorage : StorageHandler<TabViewModel> {
        public override void Configure() {
            Id(x => x.DisplayName);

            Property(x => x.Text)
                .InPhoneState()
                .RestoreAfterActivation();
        }
    }
}