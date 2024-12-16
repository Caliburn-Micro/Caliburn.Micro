using System;
using System.Runtime.InteropServices;
using Caliburn.Micro;
#if XAMARINFORMS
using Xamarin.Forms;
#elif SILVERLIGHT || WPF
using System.Windows;
#elif AVALONIA
using Avalonia.Controls;
using MsBox.Avalonia;
#else
using Windows.UI.Popups;
#endif

namespace Features.CrossPlatform.Results
{
    public class MessageDialogResult : ResultBase
    {
        private readonly string _content;
        private readonly string _title;

        public MessageDialogResult(string content, string title)
        {
            _content = content;
            _title = title;
        }
#if XAMARINFORMS
        public override async void Execute(CoroutineExecutionContext context)
        {
            if (!(context.View is Page))
                throw new InvalidOperationException("View must be a Page to use MessageDialogResult");

            var page = (Page) context.View;

            await page.DisplayAlert(_title, _content, "Ok");

            OnCompleted();
        }

#elif SILVERLIGHT || WPF
        public override void Execute(CoroutineExecutionContext context)
        {
            MessageBox.Show(_content, _title, MessageBoxButton.OK);

            OnCompleted();
        }
#elif AVALONIA
        public override async void Execute(CoroutineExecutionContext context)
        {
            var dialog = MessageBoxManager.GetMessageBoxStandard(_title, _content);

            await dialog.ShowAsync();
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
            var dialog = new MessageDialog(_content, _title);

            await dialog.ShowAsync();

            OnCompleted();
        }
#endif
    }
}
