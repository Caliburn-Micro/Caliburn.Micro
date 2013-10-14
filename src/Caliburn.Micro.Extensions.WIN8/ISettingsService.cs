using System;
using System.Collections.Generic;

namespace Caliburn.Micro
{
    /// <summary>
    /// Service that handles the Settings Charm.
    /// </summary>
    public interface ISettingsService
    {
        /// <summary>
        /// Displays the Settings Charm pane to the user.
        /// </summary>
        void ShowSettingsUI();

        /// <summary>
        /// Registers a Settings Command with the service.
        /// </summary>
        /// <typeparam name="TViewModel">The commands view model.</typeparam>
        /// <param name="label">The command label.</param>
        /// <param name="viewSettings">The optional flyout view settings.</param>
        void RegisterFlyoutCommand<TViewModel>(string label, IDictionary<string, object> viewSettings = null);

        void RegisterUriCommand(string label, Uri uri);

        void RegisterCommand(SettingsCommandBase command);
    }
}