using Caliburn.Micro;
using MsBox.Avalonia;


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
            var dialog = MessageBoxManager
                        .GetMessageBoxStandard("title", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed...");

            await dialog.ShowAsync();
            OnCompleted();
        }
    }
}
