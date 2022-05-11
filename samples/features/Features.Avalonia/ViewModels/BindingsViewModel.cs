using System;
using Caliburn.Micro;
using Features.Avalonia.ViewModels.Activity;

namespace Features.Avalonia.ViewModels
{
    public class BindingsViewModel : Screen
    {
        private ActivityBaseViewModel selectedActivity;

        public BindingsViewModel()
        {
            Activities = new BindableCollection<ActivityBaseViewModel>
            {
                new MessageActivityViewModel(Lipsum.Get()),
                new PhotoActivityViewModel(Lipsum.Get()),
                new MessageActivityViewModel(Lipsum.Get()),
                new PhotoActivityViewModel(Lipsum.Get())
            };
        }

        public BindableCollection<ActivityBaseViewModel> Activities { get; }

        public ActivityBaseViewModel SelectedActivity
        {
            get { return selectedActivity; }
            set { Set(ref selectedActivity, value); }
        }
    }
}
