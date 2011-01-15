namespace Caliburn.Micro.ViewFirst {
    using System.ComponentModel.Composition;
    using System.Windows;

    [Export("Shell", typeof(IShell))]
    public class ShellViewModel : PropertyChangedBase, IShell {
        string name;

        public string Name {
            get { return name; }
            set {
                name = value;
                NotifyOfPropertyChange(() => Name);
                NotifyOfPropertyChange(() => CanSayHello);
            }
        }

        public bool CanSayHello {
            get { return !string.IsNullOrWhiteSpace(Name); }
        }

        public void SayHello() {
            MessageBox.Show(string.Format("Hello {0}!", Name));
        }
    }
}