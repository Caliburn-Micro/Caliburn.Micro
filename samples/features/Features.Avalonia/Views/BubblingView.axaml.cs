using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Features.Avalonia.Views
{
    public partial class BubblingView : UserControl
    {
        public BubblingView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
