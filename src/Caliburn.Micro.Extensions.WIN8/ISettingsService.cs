namespace Caliburn.Micro {
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Service that handles the Settings Charm.
    /// </summary>
    public interface ISettingsService {
        /// <summary>
        /// Displays the Settings Charm pane to the user.
        /// </summary>
        void ShowSettingsUI();

        /// <summary>
        /// Registers a flyout command with the service.
        /// </summary>
        /// <typeparam name="TViewModel">The commands view model.</typeparam>
        /// <param name="label">The command label.</param>
        /// <param name="viewSettings">The optional flyout view settings.</param>
        void RegisterFlyoutCommand<TViewModel>(string label, IDictionary<string, object> viewSettings = null);

        /// <summary>
        /// Registers a URI command with the service.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="uri">The URI.</param>
        void RegisterUriCommand(string label, Uri uri);

        /// <summary>
        /// Registers a settings command with the service.
        /// </summary>
        /// <param name="command">The command to register.</param>
        void RegisterCommand(SettingsCommandBase command);
    }
}
