using System;
using Windows.ApplicationModel.DataTransfer;

namespace Caliburn.Micro.WinRT.Sample.ViewModels
{
    public class ShareSourceViewModel : ViewModelBase, ISupportSharing
    {
        private readonly ISharingService sharingService;

        public ShareSourceViewModel(INavigationService navigationService, ISharingService sharingService)
            : base(navigationService)
        {
            this.sharingService = sharingService;
        }

        public void OnShareRequested(DataRequest dataRequest)
        {
            dataRequest.Data.Properties.Title = "Share Source Example";
            dataRequest.Data.Properties.Description = "An example of sharing data from a view model";

            dataRequest.Data.SetUri(new Uri("http://www.markermetro.com"));
        }

        public void Open()
        {
            sharingService.ShowShareUI();
        }
    }
}
