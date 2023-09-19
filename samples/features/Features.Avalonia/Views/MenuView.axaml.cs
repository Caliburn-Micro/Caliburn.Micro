using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Caliburn.Micro;

namespace Features.CrossPlatform.Views
{
    public partial class MenuView : UserControl
    {

        public MenuView()
        {
            InitializeComponent();
        }



        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

        }
    }
}
