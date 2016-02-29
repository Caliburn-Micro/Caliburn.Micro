namespace Caliburn.Micro {
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a flyout command registered with the <see cref="ISettingsService" />.
    /// </summary>
    public class FlyoutSettingsCommand : SettingsCommandBase {
        private readonly ISettingsWindowManager settingsWindowManager;
        private readonly Type viewModelType;
        private readonly IDictionary<string, object> viewSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="FlyoutSettingsCommand" /> class.
        /// </summary>
        /// <param name="settingsWindowManager"></param>
        /// <param name="label">The label to use in the settings charm.</param>
        /// <param name="viewModelType">The view model to display.</param>
        /// <param name="viewSettings">Additional settings to pass to the <see cref="ISettingsWindowManager" />.</param>
        public FlyoutSettingsCommand(ISettingsWindowManager settingsWindowManager, string label, Type viewModelType,
            IDictionary<string, object> viewSettings)
            : base(label) {
            this.settingsWindowManager = settingsWindowManager;
            this.viewModelType = viewModelType;
            this.viewSettings = viewSettings;
        }

        /// <summary>
        /// The view model to display.
        /// </summary>
        public Type ViewModelType {
            get { return viewModelType; }
        }

        /// <summary>
        /// Additional settings to pass to the <see cref="ISettingsWindowManager" />.
        /// </summary>
        public IDictionary<string, object> ViewSettings {
            get { return viewSettings; }
        }

        /// <summary>
        /// Called when the command was selected in the Settings Charm.
        /// </summary>
        public override void OnSelected() {
            var viewModel = IoC.GetInstance(ViewModelType, null);
            if (viewModel == null)
                return;

            settingsWindowManager.ShowSettingsFlyout(viewModel, Label, ViewSettings);
        }
    }
}
