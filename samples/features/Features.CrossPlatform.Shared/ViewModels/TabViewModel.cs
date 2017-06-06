using System;
using Caliburn.Micro;

namespace Features.CrossPlatform.ViewModels
{
    public class TabViewModel : Screen
    {
        public TabViewModel()
        {
            Messages = new BindableCollection<string>();
        }

        protected override void OnInitialize()
        {
            Messages.Add("Initialized");
        }

        protected override void OnActivate()
        {
            Messages.Add("Activated");
        }

        protected override void OnDeactivate(bool close)
        {
            Messages.Add($"Deactivated, close: {close}");
        }

        public BindableCollection<string> Messages { get; }
    }
}
