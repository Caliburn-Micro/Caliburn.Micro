using System;

namespace Caliburn.Micro.WinRT.Sample.ViewModels
{
    public class DeveloperViewModel : PropertyChangedBase
    {
        private string _name;

        public DeveloperViewModel(string name)
        {
            _name = name;
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                NotifyOfPropertyChange();
            }
        }
    }
}
