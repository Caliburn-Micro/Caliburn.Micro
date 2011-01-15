namespace Caliburn.Micro.HelloScreens.Framework {
    using System.ComponentModel;

    public abstract class DocumentWorkspace<TDocument> : Conductor<TDocument>.Collection.OneActive, IDocumentWorkspace
        where TDocument : INotifyPropertyChanged, IDeactivate, IHaveDisplayName {
        DocumentWorkspaceState state = DocumentWorkspaceState.Master;

        protected DocumentWorkspace() {
            Items.CollectionChanged += delegate { NotifyOfPropertyChange(() => Status); };
            DisplayName = IconName;
        }

        public DocumentWorkspaceState State {
            get { return state; }
            set {
                if(state == value)
                    return;

                state = value;
                NotifyOfPropertyChange(() => State);
            }
        }

        protected IConductor Conductor {
            get { return (IConductor)Parent; }
        }

        public abstract string IconName { get; }
        public abstract string Icon { get; }

        public string Status {
            get { return Items.Count > 0 ? Items.Count.ToString() : string.Empty; }
        }

        public void Show() {
            var haveActive = Parent as IHaveActiveItem;
            if (haveActive != null && haveActive.ActiveItem == this) {
                DisplayName = IconName;
                State = DocumentWorkspaceState.Master;
            }
            else Conductor.ActivateItem(this);
        }

        void IDocumentWorkspace.Edit(object document) {
            Edit((TDocument)document);
        }

        public void Edit(TDocument child) {
            Conductor.ActivateItem(this);
            State = DocumentWorkspaceState.Detail;
            DisplayName = child.DisplayName;
            ActivateItem(child);
        }

        public override void ActivateItem(TDocument item) {
            item.Deactivated += OnItemOnDeactivated;
            item.PropertyChanged += OnItemPropertyChanged;

            base.ActivateItem(item);
        }

        void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e) {
            if(e.PropertyName == "DisplayName")
                DisplayName = ((TDocument)sender).DisplayName;
        }

        void OnItemOnDeactivated(object sender, DeactivationEventArgs e) {
            var doc = (TDocument)sender;
            if(e.WasClosed) {
                DisplayName = IconName;
                State = DocumentWorkspaceState.Master;
                doc.Deactivated -= OnItemOnDeactivated;
                doc.PropertyChanged -= OnItemPropertyChanged;
            }
        }
    }
}