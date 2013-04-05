namespace Caliburn.Micro {
    using Callisto.Controls;
    using System;
    using System.Collections.Generic;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Media.Imaging;

    /// <summary>
    /// An implementation of the <see cref="ISettingsWindowManager" /> using Callisto
    /// </summary>
    public class CallistoSettingsWindowManager : ISettingsWindowManager {
        /// <summary>
        /// Shows a settings flyout panel for the specified model.
        /// </summary>
        /// <param name="viewModel">The settings view model.</param>
        /// <param name="commandLabel">The settings command label.</param>
        /// <param name="viewSettings">The optional dialog settings.</param>
        public async void ShowSettingsFlyout(object viewModel, string commandLabel, IDictionary<string, object> viewSettings = null) {
            var view = ViewLocator.LocateForModel(viewModel, null, null);
            ViewModelBinder.Bind(viewModel, view, null);

            viewSettings = viewSettings ?? new Dictionary<string, object>();

            var width = viewSettings.ContainsKey("width")
                            ? (SettingsFlyout.SettingsFlyoutWidth) viewSettings["width"]
                            : SettingsFlyout.SettingsFlyoutWidth.Narrow;

            // extract the header color/logo from the appmanifest.xml
            var visualElements = await Callisto.Controls.Common.AppManifestHelper.GetManifestVisualElementsAsync();

            // enable the overriding of these, but default to manifest
            var headerBackground = viewSettings.ContainsKey("headerbackground")
                                       ? (SolidColorBrush) viewSettings["headerbackground"]
                                       : new SolidColorBrush(visualElements.BackgroundColor);

            var smallLogoUri = viewSettings.ContainsKey("smalllogouri")
                                   ? (Uri) viewSettings["smalllogouri"]
                                   : visualElements.SmallLogoUri;

            var smallLogo = new BitmapImage(smallLogoUri);

            var settingsFlyout = new SettingsFlyout
                {
                    FlyoutWidth = width,
                    HeaderText = commandLabel,
                    Content = view,
                    IsOpen = true,
                    HeaderBrush = headerBackground,
                    SmallLogoImageSource = smallLogo
                };

            var deactivator = viewModel as IDeactivate;
            if (deactivator != null) {
                EventHandler<object> closed = null;
                closed = (s, e) => {
                    deactivator.Deactivate(true);
                    settingsFlyout.Closed -= closed;
                };

                settingsFlyout.Closed += closed;
            }

            var activator = viewModel as IActivate;
            if (activator != null) {
                activator.Activate();
            }
        }
    }
}
