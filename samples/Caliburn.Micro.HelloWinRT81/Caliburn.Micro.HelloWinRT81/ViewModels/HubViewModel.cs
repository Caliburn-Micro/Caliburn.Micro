
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
                this.Set(ref pageTitle, value);
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
                this.Set(ref firstSectionContent, value);
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
                this.Set(ref secondSection, value);
            }
        }
    }
}
