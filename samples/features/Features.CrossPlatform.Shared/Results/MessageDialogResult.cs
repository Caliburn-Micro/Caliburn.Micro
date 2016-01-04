using System;
using Caliburn.Micro;

#if SILVERLIGHT
using System.Windows;
#else
using Windows.UI.Popups;
#endif

namespace Features.CrossPlatform.Results
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
#if SILVERLIGHT
            MessageBox.Show(title, content, MessageBoxButton.OK);
#else
            var dialog = new MessageDialog(content, title);

            await dialog.ShowAsync();
#endif
            OnCompleted();
        }
    }
}
