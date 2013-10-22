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
                this.Set(ref title, value);
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
                this.Set(ref viewModelType, value);
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
                this.Set(ref subtitle, value);
            }
        }
    }
}
