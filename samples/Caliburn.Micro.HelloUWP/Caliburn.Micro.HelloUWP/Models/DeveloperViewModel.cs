using System;

namespace Caliburn.Micro.HelloUWP.ViewModels
{
    public class DeveloperViewModel : PropertyChangedBase
    {
        private string name;

        public DeveloperViewModel(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                 name=value;
            }
        }
    }
}
