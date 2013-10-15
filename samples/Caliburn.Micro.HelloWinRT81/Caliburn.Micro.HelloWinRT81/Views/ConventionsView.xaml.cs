using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;

namespace Caliburn.Micro.WinRT.Sample.Views
{
    public sealed partial class ConventionsView
    {
        public ConventionsView()
        {
            InitializeComponent();
        }

        private void OnShowFlyout(object sender, TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout(AttachedFlyoutTest);
        }
    }
}
