using System;
using Windows.System;

namespace Caliburn.Micro
{
    public class UriSettingsCommand : SettingsCommandBase
    {
        private readonly Uri uri;

        public UriSettingsCommand(string label, Uri uri) : base(label)
        {
            this.uri = uri;
        }

        public Uri Uri
        {
            get { return uri; }
        }

        public override async void OnSelected()
        {
            await Launcher.LaunchUriAsync(Uri);
        }
    }
}
