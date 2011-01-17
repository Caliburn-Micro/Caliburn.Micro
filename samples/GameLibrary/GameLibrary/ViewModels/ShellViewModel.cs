namespace GameLibrary.ViewModels {
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using Caliburn.Micro;
    using Framework;

    [Export(typeof(IShell))]
    public class ShellViewModel : Conductor<IScreen>, IShell {
        readonly SearchViewModel firstScreen;

        [ImportingConstructor]
        public ShellViewModel(SearchViewModel firstScreen) {
            this.firstScreen = firstScreen;
        }

        public void Back() {
            ActivateItem(firstScreen);
        }

        protected override void OnInitialize() {
            ActivateItem(firstScreen);
            base.OnInitialize();
        }
    }
}