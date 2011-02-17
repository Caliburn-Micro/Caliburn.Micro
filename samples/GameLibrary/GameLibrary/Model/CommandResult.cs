namespace GameLibrary.Model {
    using System;
    using System.ComponentModel.Composition;
    using Caliburn.Micro;

    public class CommandResult : IResult {
        readonly ICommand command;

        [Import]
        public IBackend Backend { get; set; }

        public CommandResult(ICommand command) {
            this.command = command;
        }

        public void Execute(ActionExecutionContext context) {
            Backend.Send(command);
            Completed(this, new ResultCompletionEventArgs());
        }

        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };
    }
}