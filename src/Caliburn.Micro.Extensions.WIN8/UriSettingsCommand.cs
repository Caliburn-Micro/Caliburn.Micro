namespace Caliburn.Micro {
    using System;
    using Windows.System;

    /// <summary>
    /// Represents a URI command registered with the <see cref="ISettingsService" />.
    /// </summary>
    public class UriSettingsCommand : SettingsCommandBase {
        private readonly Uri uri;

        /// <summary>
        /// Initializes a new instance of the <see cref="UriSettingsCommand"/> class.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="uri">The URI.</param>
        public UriSettingsCommand(string label, Uri uri) : base(label) {
            this.uri = uri;
        }

        /// <summary>
        /// Gets the URI.
        /// </summary>
        public Uri Uri {
            get { return uri; }
        }

        /// <summary>
        /// Called when the command was selected in the Settings Charm.
        /// </summary>
        public override async void OnSelected() {
            await Launcher.LaunchUriAsync(Uri);
        }
    }
}
