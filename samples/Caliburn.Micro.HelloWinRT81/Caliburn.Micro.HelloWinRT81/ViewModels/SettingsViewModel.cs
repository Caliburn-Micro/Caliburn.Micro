using System;

namespace Caliburn.Micro.WinRT.Sample.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly ISettingsService settingsService;
        private readonly ISettingsWindowManager settingsWindowManager;

        public SettingsViewModel(ISettingsService settingsService, INavigationService navigationService, ISettingsWindowManager settingsWindowManager)
            : base(navigationService)
        {
            this.settingsService = settingsService;
            this.settingsWindowManager = settingsWindowManager;
        }

        public void Open()
        {
            settingsService.ShowSettingsUI();
        }

        public void ShowIndependent()
        {
            settingsWindowManager.ShowSettingsFlyout(new SampleSettingsViewModel(), "Settings", independent: true);
        }
    }
}
