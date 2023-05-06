using System;
using Caliburn.Micro;
using Features.CrossPlatform.ViewModels.Activity;

namespace Features.CrossPlatform.ViewModels
{
    public class BindingsViewModel : Screen
    {
        private ActivityBaseViewModel _selectedActivity;

        public BindingsViewModel()
        {
            Activities = new BindableCollection<ActivityBaseViewModel>
            {
                new MessageActivityViewModel(Lipsum.Get()),
                new PhotoActivityViewModel(Lipsum.Get()),
                new MessageActivityViewModel(Lipsum.Get()),
                new PhotoActivityViewModel(Lipsum.Get())
            };
            _selectedActivity = Activities.First();
            NotifyOfPropertyChange("Activities");
        }

        public BindableCollection<ActivityBaseViewModel> Activities { get; }

        public ActivityBaseViewModel SelectedActivity
        {
            get { return _selectedActivity; }
            set { Set(ref _selectedActivity, value); }
        }
    }
}
