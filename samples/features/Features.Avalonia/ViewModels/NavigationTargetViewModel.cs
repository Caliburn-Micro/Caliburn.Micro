using System;
using Caliburn.Micro;

namespace Features.Avalonia.ViewModels
{
    public class NavigationTargetViewModel : Screen
    {
        private string text;
        private bool isEnabled;

        public string Text
        {
            get { return text; }
            set { Set(ref text, value); }
        }

        public bool IsEnabled
        {
            get { return isEnabled; }
            set { Set(ref isEnabled, value); }
        }
    }
}
