namespace Caliburn.Micro.HelloScreens.Shell
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using Framework;

    [Export(typeof(IShell))]
    public class ShellViewModel : Conductor<IWorkspace>.Collection.OneActive, IShell
    {
        readonly IDialogManager dialogs;

        [ImportingConstructor]
        public ShellViewModel(IDialogManager dialogs, [ImportMany]IEnumerable<IWorkspace> workspaces) {
            this.dialogs = dialogs;
            Items.AddRange(workspaces);
            CloseStrategy = new ApplicationCloseStrategy();
        }

        public IDialogManager Dialogs {
            get { return dialogs; }
        }
    }
}