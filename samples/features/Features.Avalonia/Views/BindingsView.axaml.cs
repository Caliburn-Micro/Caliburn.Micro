using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Features.CrossPlatform.Views
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
