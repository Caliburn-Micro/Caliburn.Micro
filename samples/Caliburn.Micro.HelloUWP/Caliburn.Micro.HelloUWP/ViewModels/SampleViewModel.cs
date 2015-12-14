using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caliburn.Micro.HelloUWP.ViewModels
{
    public class SampleViewModel : PropertyChangedBase
    {
        private string _title;
        private string _subtitle;
        private Type _viewModelType;

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }

        public Type ViewModelType
        {
            get
            {
                return _viewModelType;
            }
            set
            {
                _viewModelType = value;
                NotifyOfPropertyChange(() => ViewModelType);
            }
        }

        public string Subtitle
        {
            get
            {
                return _subtitle;
            }
            set
            {
                _subtitle = value; 
                NotifyOfPropertyChange(() => Subtitle);
            }
        }
    }
}
