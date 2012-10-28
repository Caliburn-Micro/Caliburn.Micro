using System;
using Windows.ApplicationModel.Search;

namespace Caliburn.Micro.WinRT.Sample.ViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        private string parameter;

        public SearchViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }

        public string Parameter
        {
            get
            {
                return parameter;
            }
            set
            {
                parameter = value;
                NotifyOfPropertyChange();
            }
        }

        protected override void OnInitialize()
        {
            Parameter = Parameter ?? "None";
        }

        public void Search()
        {
            var searchPane = SearchPane.GetForCurrentView();

            searchPane.Show();
        }
    }
}
