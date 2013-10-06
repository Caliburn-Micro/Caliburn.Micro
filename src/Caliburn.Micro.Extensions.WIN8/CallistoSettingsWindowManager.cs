namespace Caliburn.Micro {
    using Callisto.Controls;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
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

            // use real property names for ApplySettings
            if (!viewSettings.ContainsKey("FlyoutWidth"))
                viewSettings["FlyoutWidth"] = width;
            if (!viewSettings.ContainsKey("HeaderBrush"))
                viewSettings["HeaderBrush"] = headerBackground;
            if (!viewSettings.ContainsKey("SmallLogoImageSource"))
                viewSettings["SmallLogoImageSource"] = smallLogo;

            var settingsFlyout = new SettingsFlyout
                {
                    HeaderText = commandLabel,
                    Content = view,
                };

            ApplySettings(settingsFlyout, viewSettings);
            settingsFlyout.IsOpen = true;

            var deactivator = viewModel as IDeactivate;
            if (deactivator != null) {
                EventHandler<object> closed = null;
                closed = (s, e) => {
                    settingsFlyout.Closed -= closed;
                    deactivator.Deactivate(true);
                };

                settingsFlyout.Closed += closed;
            }

            var activator = viewModel as IActivate;
            if (activator != null) {
                activator.Activate();
            }
        }

        static bool ApplySettings(object target, IEnumerable<KeyValuePair<string, object>> settings) {
            if (settings == null)
                return false;

            var type = target.GetType();

            foreach (var pair in settings) {
                var propertyInfo = type.GetRuntimeProperty(pair.Key);

                if (propertyInfo != null)
                    propertyInfo.SetValue(target, pair.Value, null);
            }

            return true;
        }
    }
}
