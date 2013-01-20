using System;

namespace Caliburn.Micro.WinRT.Sample.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly ISettingsService settingsService;

        public SettingsViewModel(ISettingsService settingsService, INavigationService navigationService)
            : base(navigationService)
        {
            this.settingsService = settingsService;
        }

        public void Open()
        {
            settingsService.ShowSettingsUI();
        }
    }
}
