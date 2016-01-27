#if !XAMARINFORMS
using System;
using Caliburn.Micro;

#if SILVERLIGHT || WPF
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

#if SILVERLIGHT || WPF
        public override void Execute(CoroutineExecutionContext context)
        {
            MessageBox.Show(content, title, MessageBoxButton.OK);

            OnCompleted();
        }
#else
        public override async void Execute(CoroutineExecutionContext context)
        { 
            var dialog = new MessageDialog(content, title);

            await dialog.ShowAsync();

            OnCompleted();
        }
#endif
    }
}
#endif