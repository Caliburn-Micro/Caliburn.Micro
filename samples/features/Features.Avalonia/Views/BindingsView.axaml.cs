using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Features.Avalonia.ViewModels;

namespace Features.Avalonia.Views
{
    public partial class BindingsView : UserControl
    {
 
        public BindingsView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
