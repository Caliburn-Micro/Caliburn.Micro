using System;

namespace Caliburn.Micro.WinRT.Sample.ViewModels
{
    public class SampleViewModel : PropertyChangedBase
    {
        private string title;
        private string subtitle;
        private Type viewModelType;

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                NotifyOfPropertyChange();
            }
        }

        public Type ViewModelType
        {
            get
            {
                return viewModelType;
            }
            set
            {
                viewModelType = value;
                NotifyOfPropertyChange();
            }
        }

        public string Subtitle
        {
            get
            {
                return subtitle;
            }
            set
            {
                subtitle = value;
                NotifyOfPropertyChange();
            }
        }
    }
}
