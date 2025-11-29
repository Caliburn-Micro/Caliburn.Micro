using Microsoft.VisualStudio.TestPlatform.TestExecutor;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Caliburn.Micro.UWP.Tests
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default <see cref="Application"/> class, specific to unit testing scenarios.
    /// </summary>
    public sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object. This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
        }

        /// <inheritdoc/>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            // Make sure we have some XAML content, so that we can initialize the window.
            // This app is only meant to run unit tests, so we can skip the usual setup.
            Window.Current.Content ??= new Frame();

            // Delegate to VSTest to create any necessary additional UI elements
            UnitTestClient.CreateDefaultUI();

            // Ensure the current window is active
            Window.Current.Activate();

            // Invoke all unit tests defined in the current project
            UnitTestClient.Run(e.Arguments);
        }
    }
}
