using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Features.Avalonia.Views
{
    public partial class FeatureView : UserControl
    {
        public FeatureView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
