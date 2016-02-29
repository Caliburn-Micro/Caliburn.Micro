namespace Caliburn.Micro {
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Media.Imaging;

    /// <summary>
    /// An implementation of the <see cref="ISettingsWindowManager" /> using the default Windows 8.1 controls
    /// </summary>
    public class SettingsWindowManager : ISettingsWindowManager {
        /// <summary>
        /// Shows a settings flyout panel for the specified model.
        /// </summary>
        /// <param name="viewModel">The settings view model.</param>
        /// <param name="commandLabel">The settings command label.</param>
        /// <param name="viewSettings">The optional dialog settings.</param>
        /// <param name="independent">Show settings independent from <seealso cref="Windows.UI.ApplicationSettings.SettingsPane"/>.</param>
        public async void ShowSettingsFlyout(object viewModel, string commandLabel,
            IDictionary<string, object> viewSettings = null, bool independent = false) {
            var view = ViewLocator.LocateForModel(viewModel, null, null);
            ViewModelBinder.Bind(viewModel, view, null);

            viewSettings = viewSettings ?? new Dictionary<string, object>();

            var settingsFlyout = new SettingsFlyout
            {
                Title = commandLabel,
                Content = view,
                HorizontalContentAlignment = HorizontalAlignment.Stretch
            };

            // extract the header color/logo from the appmanifest.xml
            var visualElements = await AppManifestHelper.GetManifestVisualElementsAsync();

            // enable the overriding of these, but default to manifest
            var headerBackground = viewSettings.ContainsKey("headerbackground")
                ? (SolidColorBrush) viewSettings["headerbackground"]
                : new SolidColorBrush(visualElements.BackgroundColor);

            var smallLogoUri = viewSettings.ContainsKey("smalllogouri")
                ? (Uri) viewSettings["smalllogouri"]
                : visualElements.SmallLogoUri;

            var smallLogo = new BitmapImage(smallLogoUri);

            // use real property names for ApplySettings
            if (!viewSettings.ContainsKey("HeaderBackground"))
                viewSettings["HeaderBackground"] = headerBackground;

            if (!viewSettings.ContainsKey("IconSource"))
                viewSettings["IconSource"] = smallLogo;

            ApplySettings(settingsFlyout, viewSettings);

            var deactivator = viewModel as IDeactivate;
            if (deactivator != null) {
                RoutedEventHandler closed = null;
                closed = (s, e) => {
                    settingsFlyout.Unloaded -= closed;
                    deactivator.Deactivate(true);
                };

                settingsFlyout.Unloaded += closed;
            }

            var activator = viewModel as IActivate;
            if (activator != null) {
                activator.Activate();
            }

            if (independent)
                settingsFlyout.ShowIndependent();
            else
                settingsFlyout.Show();
        }

        private static bool ApplySettings(object target, IEnumerable<KeyValuePair<string, object>> settings) {
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
