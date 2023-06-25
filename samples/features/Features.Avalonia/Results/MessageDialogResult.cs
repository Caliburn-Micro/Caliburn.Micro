using System;
using Caliburn.Micro;


namespace Features.Avalonia.Results
{
    public class MessageDialogResult : ResultBase
    {
        private readonly string content;
        private readonly string title;

        public MessageDialogResult(string content, string title)
        {
            this.content = content;
            this.title = title;
        }

        public override async void Execute(CoroutineExecutionContext context)
        {
            var dialog = MessageBox.Avalonia.MessageBoxManager
  .GetMessageBoxStandardWindow("title", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed...");

            await dialog.Show();
            OnCompleted();
        }
    }
}
