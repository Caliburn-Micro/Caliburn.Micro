using System;
using System.Collections.Generic;
using Callisto.Controls;

namespace Caliburn.Micro
{
    /// <summary>
    /// An implementation of the <see cref="ISettingsWindowManager" /> using Callisto
    /// </summary>
    public class CallistoSettingsWindowManager : ISettingsWindowManager
    {
        public void ShowSettingsFlyout(object viewModel, string commandLabel, IDictionary<string, object> viewSettings = null)
        {
            var view = ViewLocator.LocateForModel(viewModel, null, null);

            ViewModelBinder.Bind(viewModel, view, null);

            viewSettings = viewSettings ?? new Dictionary<string, object>();

            var width = viewSettings.ContainsKey("width") ?
                            (SettingsFlyout.SettingsFlyoutWidth) viewSettings["width"] :
                            SettingsFlyout.SettingsFlyoutWidth.Narrow;

            var settingsFlyout = new SettingsFlyout
            {
                FlyoutWidth = width,
                HeaderText = commandLabel,
                Content = view,
                IsOpen = true
            };

            settingsFlyout.Closed += (s, e) =>
            {
                var deactivator = viewModel as IDeactivate;

                if (deactivator != null)
                {
                    deactivator.Deactivate(true);
                }
            };

            var activator = viewModel as IActivate;

            if (activator != null)
            {
                activator.Activate();
            }
        }
    }
}
