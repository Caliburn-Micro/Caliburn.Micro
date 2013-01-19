using System.Linq;
using Windows.UI.ApplicationSettings;

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
    }

    internal class SettingsService : ISettingsService
    {
        private readonly ISettingsWindowManager _settingsWindowManager;
        private readonly SettingsPane _settingsPane;

        public SettingsService(ISettingsWindowManager settingsWindowManager)
        {
            _settingsWindowManager = settingsWindowManager;
            _settingsPane = SettingsPane.GetForCurrentView();
            _settingsPane.CommandsRequested += OnCommandsRequested;
        }

        /// <summary>
        /// Displays the Settings Charm pane to the user.
        /// </summary>
        public void ShowSettingsUI()
        {
            SettingsPane.Show();
        }

        protected virtual void OnCommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            var caliburnCommands = IoC.GetAllInstances(typeof(CaliburnSettingsCommand)).Cast<CaliburnSettingsCommand>();
            var settingsCommands = caliburnCommands.Select(c => new SettingsCommand(c.Id, c.Label, h => OnCommandSelected(c)));

            settingsCommands.Apply(args.Request.ApplicationCommands.Add);
        }

        protected virtual void OnCommandSelected(CaliburnSettingsCommand command)
        {
            var viewModel = IoC.GetInstance(command.ViewModelType, null);
            if (viewModel == null)
                return;

            _settingsWindowManager.ShowSettingsFlyout(viewModel, command.Label, command.ViewSettings);
        }
    }
}
