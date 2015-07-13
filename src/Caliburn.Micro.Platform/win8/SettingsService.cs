namespace Caliburn.Micro {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Windows.UI.ApplicationSettings;

    /// <summary>
    /// Serivce tha handles the settings charm
    /// </summary>
    public class SettingsService : ISettingsService {
        private readonly ISettingsWindowManager settingsWindowManager;
        private readonly SettingsPane settingsPane;
        private readonly List<SettingsCommandBase> commands;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsService" /> class.
        /// </summary>
        /// <param name="settingsWindowManager">The window manager used to open the settings views.</param>
        public SettingsService(ISettingsWindowManager settingsWindowManager) {
            this.settingsWindowManager = settingsWindowManager;

            commands = new List<SettingsCommandBase>();

            settingsPane = SettingsPane.GetForCurrentView();
            settingsPane.CommandsRequested += OnCommandsRequested;
        }

        /// <summary>
        /// Displays the Settings Charm pane to the user.
        /// </summary>
        public void ShowSettingsUI() {
            SettingsPane.Show();
        }

        /// <summary>
        /// Registers a flyout command with the service.
        /// </summary>
        /// <typeparam name="TViewModel">The commands view model.</typeparam>
        /// <param name="label">The command label.</param>
        /// <param name="viewSettings">The optional flyout view settings.</param>
        public void RegisterFlyoutCommand<TViewModel>(string label, IDictionary<string, object> viewSettings = null) {
            commands.Add(new FlyoutSettingsCommand(settingsWindowManager, label, typeof (TViewModel), viewSettings));
        }

        /// <summary>
        /// Registers a URI command with the service.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="uri">The URI.</param>
        public void RegisterUriCommand(string label, Uri uri) {
            commands.Add(new UriSettingsCommand(label, uri));
        }

        /// <summary>
        /// Registers a settings command with the service.
        /// </summary>
        /// <param name="command">The command to register.</param>
        public void RegisterCommand(SettingsCommandBase command) {
            commands.Add(command);
        }

        /// <summary>
        /// Occurs when the user opens the settings pane.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="SettingsPaneCommandsRequestedEventArgs" /> instance containing the event data.</param>
        protected virtual void OnCommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args) {
            var settingsCommands = commands.Select(c => new SettingsCommand(Guid.NewGuid(), c.Label, h => OnCommandSelected(c)));

            settingsCommands.Apply(args.Request.ApplicationCommands.Add);
        }

        /// <summary>
        /// Called when a settings command was selected in the Settings Charm.
        /// </summary>
        /// <param name="command">The settings command.</param>
        protected virtual void OnCommandSelected(SettingsCommandBase command) {
            command.OnSelected();
        }
    }
}
