using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Features.Avalonia.ViewModels;

namespace Features.CrossPlatform.Views
{
    public partial class ExecuteView : UserControl
    {

        public ExecuteView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public void UpdateView()
        {
            Output = this.FindControl<TextBlock>("Output");
            Output.Text = "View Updated";
        }
    }
}
