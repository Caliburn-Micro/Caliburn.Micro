namespace Caliburn.Micro.SimpleNavigation {
    public class ShellViewModel : Conductor<object> {
        public ShellViewModel() {
            ShowPageOne();
        }

        public void ShowPageOne() {
            ActivateItem(new PageOneViewModel());
        }

        public void ShowPageTwo() {
            ActivateItem(new PageTwoViewModel());
        }
    }
}