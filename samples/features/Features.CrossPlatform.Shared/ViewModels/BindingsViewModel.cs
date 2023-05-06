using System;
using System.Linq;
using Caliburn.Micro;
using Features.CrossPlatform.ViewModels.Activity;

namespace Features.CrossPlatform.ViewModels
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
            selectedActivity = Activities.First();
            NotifyOfPropertyChange("Activities");
        }

        public BindableCollection<ActivityBaseViewModel> Activities { get; }

        public ActivityBaseViewModel SelectedActivity
        {
            get { return selectedActivity; }
            set { Set(ref selectedActivity, value); }
        }
    }
}
