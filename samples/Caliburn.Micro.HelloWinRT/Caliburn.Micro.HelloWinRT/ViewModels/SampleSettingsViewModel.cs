using System;
using Windows.Storage;

namespace Caliburn.Micro.WinRT.Sample.ViewModels
{
    public class SampleSettingsViewModel : Screen
    {
        private string _savedText;

        protected override void OnActivate()
        {
            base.OnActivate();

            SavedText = (string)ApplicationData.Current.LocalSettings.Values["SavedText"];
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);

            ApplicationData.Current.LocalSettings.Values["SavedText"] = SavedText;
        }

        public string SavedText
        {
            get
            {
                return _savedText;
            }
            set
            {
                _savedText = value;
                NotifyOfPropertyChange();
            }
        }
    }
}
