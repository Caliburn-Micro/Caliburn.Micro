using System;
using Windows.UI.Xaml;

namespace Caliburn.Micro.HelloUWP.Views
{
    public sealed partial class ShellView
    {
        public ShellView()
        {
            InitializeComponent();
        }

        private void OpenNavigationView(object sender, RoutedEventArgs e)
        {
            NavigationView.IsPaneOpen = true;
        }
    }
}
