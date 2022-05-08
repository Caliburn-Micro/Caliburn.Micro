using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Caliburn.Micro;

namespace Setup.Avalonia.Views
{
    public class ShellView : Window
    {
        public ShellView()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
