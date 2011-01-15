namespace Caliburn.Micro.HelloWindowManager {
    using System.ComponentModel.Composition;
    using System.Dynamic;
    using System.Windows.Controls.Primitives;

    [Export(typeof(IShell))]
    public class ShellViewModel : Screen, IShell {
        readonly IWindowManager windowManager;

        [ImportingConstructor]
        public ShellViewModel(IWindowManager windowManager) {
            this.windowManager = windowManager;
        }

        public void OpenModeless() {
            windowManager.ShowWindow(new DialogViewModel(), "Modeless");
        }

        public void OpenModal() {
            var result = windowManager.ShowDialog(new DialogViewModel());
        }

        public void OpenPopup() {
            dynamic settings = new ExpandoObject();
            settings.Placement = PlacementMode.Center;
            settings.PlacementTarget = GetView(null);

            windowManager.ShowPopup(new DialogViewModel(), "Popup", settings);
        }
    }
}