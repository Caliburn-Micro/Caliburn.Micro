using System;
using System.Diagnostics;
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
            this.AttachedToVisualTree += ShellView_AttachedToVisualTree;
            var split = this.FindControl<SplitView>("spltTest");
            split.AttachedToVisualTree += Split_AttachedToVisualTree;
            var myGrid = this.FindControl<Grid>("myGrid");
            myGrid.AttachedToVisualTree += MyGrid_AttachedToVisualTree;
        }

        private void MyGrid_AttachedToVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
        {
            Trace.WriteLine("MyGrid_AttachedToVisualTree");
        }

        private void Split_AttachedToVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
        {
            Trace.WriteLine("Split_AttachedToVisualTree");
        }

        private void ShellView_AttachedToVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
        {
            Trace.WriteLine("ShellView_AttachedToVisualTree");
        }

        public void RegisterFrame(object frame)
        {
            int x = 1;
            //navigationService = new FrameAdapter(frame);

            //container.Instance(navigationService);

            //await navigationService.NavigateToViewModelAsync(typeof(MainViewModel));
        }
    }
}
