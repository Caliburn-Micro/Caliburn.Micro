using System;
using System.Runtime.InteropServices;
using Caliburn.Micro;
#if XAMARINFORMS
using Xamarin.Forms;
#elif SILVERLIGHT || WPF
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
#if XAMARINFORMS
        public override async void Execute(CoroutineExecutionContext context)
        {
            if (!(context.View is Page))
                throw new InvalidOperationException("View must be a Page to use MessageDialogResult");

            var page = (Page) context.View;

            await page.DisplayAlert(title, content, "Ok");

            OnCompleted();
        }

#elif SILVERLIGHT || WPF
        public override void Execute(CoroutineExecutionContext context)
        {
            MessageBox.Show(content, title, MessageBoxButton.OK);

            OnCompleted();
        }
#elif WinUI3
        public override async void Execute(CoroutineExecutionContext context)
        { 
            var dialog = new MessageDialog(content, title);

            var hWnd = GetActiveWindow();

            WinRT.Interop.InitializeWithWindow.Initialize(dialog, hWnd);

            await dialog.ShowAsync();

            OnCompleted();
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetActiveWindow();
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
