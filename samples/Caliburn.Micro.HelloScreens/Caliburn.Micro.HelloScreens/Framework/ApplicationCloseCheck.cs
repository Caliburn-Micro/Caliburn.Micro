namespace Caliburn.Micro.HelloScreens.Framework {
    using System;
    using System.ComponentModel.Composition;

    public class ApplicationCloseCheck : IResult {
        readonly Action<IDialogManager, Action<bool>> closeCheck;
        readonly IChild screen;

        public ApplicationCloseCheck(IChild screen, Action<IDialogManager, Action<bool>> closeCheck) {
            this.screen = screen;
            this.closeCheck = closeCheck;
        }

        [Import]
        public IShell Shell { get; set; }

        public void Execute(ActionExecutionContext context) {
            var documentWorkspace = screen.Parent as IDocumentWorkspace;
            if (documentWorkspace != null)
                documentWorkspace.Edit(screen);

            closeCheck(Shell.Dialogs, result => Completed(this, new ResultCompletionEventArgs { WasCancelled = !result }));
        }

        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };
    }
}