using Caliburn.Micro.WinRT.Sample.Results;
using System;
using System.Collections.Generic;

namespace Caliburn.Micro.WinRT.Sample.ViewModels
{
    public class ConventionsViewModel : ViewModelBase
    {
        private DateTime currentDate = DateTime.Now.Date;
        private TimeSpan currentTime = DateTime.Now.TimeOfDay;
        private string searchText;

        public ConventionsViewModel(INavigationService navigationService)
            : base(navigationService)
        {

        }

        public DateTime CurrentDate
        {
            get
            {
                return currentDate;
            }
            set
            {
                this.Set(ref currentDate, value);
            }
        }

        public TimeSpan CurrentTime
        {
            get
            {
                return currentTime;
            }
            set
            {
                this.Set(ref currentTime, value);
            }
        }

        public string SearchText
        {
            get
            {
                return searchText;
            }
            set
            {
                this.Set(ref searchText, value);
            }
        }

        public IEnumerable<IResult> ExecuteSearch()
        {
            yield return new MessageDialogResult("Search Executed", String.Format("Searching for {0}", SearchText));
        }

        public void Refresh()
        {
            CurrentDate = DateTime.Now.Date;
            CurrentTime = DateTime.Now.TimeOfDay;
        }

        public void FlyoutRefresh()
        {
            Refresh();
        }

        public void MenuFlyoutRefresh()
        {
            Refresh();
        }

        public void AttachedFlyoutRefresh()
        {
            Refresh();
        }
    }
}
