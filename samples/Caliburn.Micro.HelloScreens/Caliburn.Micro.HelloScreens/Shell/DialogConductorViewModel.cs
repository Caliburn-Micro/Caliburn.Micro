namespace Caliburn.Micro.HelloScreens.Shell {
    using System;
    using System.Collections;
    using System.ComponentModel.Composition;
    using Framework;

    [Export(typeof(IDialogManager)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class DialogConductorViewModel : PropertyChangedBase, IDialogManager, IConductActiveItem {
        readonly Func<IMessageBox> createMessageBox;

        [ImportingConstructor]
        public DialogConductorViewModel(Func<IMessageBox> messageBoxFactory) {
            createMessageBox = messageBoxFactory;
        }

        public IScreen ActiveItem { get; private set; }

        public IEnumerable GetChildren() {
            return ActiveItem != null ? new[] { ActiveItem } : new object[0];
        }

        public void ActivateItem(object item) {
            ActiveItem = item as IScreen;

            var child = ActiveItem as IChild;
            if(child != null)
                child.Parent = this;

            if(ActiveItem != null)
                ActiveItem.Activate();

            NotifyOfPropertyChange(() => ActiveItem);
            ActivationProcessed(this, new ActivationProcessedEventArgs { Item = ActiveItem, Success = true });
        }

        public void DeactivateItem(object item, bool close) {
            var guard = item as IGuardClose;
            if(guard != null) {
                guard.CanClose(result => {
                    if(result)
                        CloseActiveItemCore();
                });
            }
            else CloseActiveItemCore();
        }

        object IHaveActiveItem.ActiveItem
        {
            get { return ActiveItem; }
            set { ActivateItem(value); }
        }

        public event EventHandler<ActivationProcessedEventArgs> ActivationProcessed = delegate { };

        public void ShowDialog(IScreen dialogModel) {
            ActivateItem(dialogModel);
        }

        public void ShowMessageBox(string message, string title = "Hello Screens", MessageBoxOptions options = MessageBoxOptions.Ok, Action<IMessageBox> callback = null) {
            var box = createMessageBox();

            box.DisplayName = title;
            box.Options = options;
            box.Message = message;

            if(callback != null)
                box.Deactivated += delegate { callback(box); };

            ActivateItem(box);
        }

        void CloseActiveItemCore() {
            var oldItem = ActiveItem;
            ActivateItem(null);
            oldItem.Deactivate(true);
        }
    }
}