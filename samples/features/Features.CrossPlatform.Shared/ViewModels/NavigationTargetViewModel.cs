using System;
using Caliburn.Micro;

namespace Features.CrossPlatform.ViewModels
{
    public class NavigationTargetViewModel : Screen
    {
        private string text;
        private bool isEnabled;

        public string Text
        {
            get { return text; }
            set { this.Set(ref text, value); }
        }

        public bool IsEnabled
        {
            get { return isEnabled; }
            set { this.Set(ref isEnabled, value); }
        }
    }
}
