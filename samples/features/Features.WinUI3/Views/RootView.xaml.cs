// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Features.WinUI3.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RootView : UserControl
    {
        public RootView(Frame frame)
        {
            this.InitializeComponent();
            Frame = frame;
            Layoutroot.Children.Add(frame);
            Grid.SetRow(frame, 1);
        }

        public Frame Frame { get; }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current as App)?.GoBack();
        }
    }
}
