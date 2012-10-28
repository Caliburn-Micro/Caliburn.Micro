using System;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.DataTransfer.ShareTarget;

namespace Caliburn.Micro.WinRT.Sample.ViewModels
{
    public class ShareTargetViewModel : Screen
    {
        private readonly ShareOperation operation;
        private string uri;
        private string text;

        public ShareTargetViewModel(ShareOperation operation)
        {
            this.operation = operation;
        }

        protected async override void OnInitialize()
        {
            base.OnInitialize();

            if (operation.Data.Contains(StandardDataFormats.Uri))
            {
                var sharedUri = await operation.Data.GetUriAsync();

                Uri = sharedUri.ToString();
            }

            if (operation.Data.Contains(StandardDataFormats.Text))
            {
                Text = await operation.Data.GetTextAsync();
            }
        }

        public void Share()
        {
            operation.ReportCompleted();
        }

        public string Uri
        {
            get
            {
                return uri;
            }
            set
            {
                uri = value;
                NotifyOfPropertyChange();
            }
        }

        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                NotifyOfPropertyChange();
            }
        }
    }
}
