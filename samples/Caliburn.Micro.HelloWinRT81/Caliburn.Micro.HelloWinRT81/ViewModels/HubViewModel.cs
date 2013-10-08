using Windows.UI.Xaml.Controls;
using Caliburn.Micro.WinRT.Sample.Results;
using Caliburn.Micro.WinRT.Sample.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caliburn.Micro.WinRT.Sample.ViewModels
{
    public class HubViewModel : ViewModelBase
    {
        private string pageTitle;
        private string firstSectionContent;
        private string secondSection;
        public HubViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            PageTitle = "Hub Control";
            FirstSectionContent = "TextBlock bound by convention...";
            SecondSection = "Second Section";
        }

        public string PageTitle
        {
            get
            {
                return pageTitle;
            }
            set
            {
                pageTitle = value;
                NotifyOfPropertyChange(() => PageTitle);
            }
        }

        public string FirstSectionContent
        {
            get
            {
                return firstSectionContent;
            }
            set
            {
                firstSectionContent = value;
                NotifyOfPropertyChange(() => FirstSectionContent);
            }
        }

        public string SecondSection
        {
            get
            {
                return secondSection;
            }
            set
            {
                secondSection = value;
                NotifyOfPropertyChange(() => SecondSection);
            }
        }
    }
}
