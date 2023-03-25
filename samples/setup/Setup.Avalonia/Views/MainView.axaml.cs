using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Setup.Avalonia.ViewModels;

namespace Setup.Avalonia.Views
{

    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
