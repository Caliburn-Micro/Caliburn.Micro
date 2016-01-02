using System;

namespace Features.CrossPlatform.ViewModels.Activity
{
    public class PhotoActivityViewModel : ActivityBaseViewModel
    {
        public PhotoActivityViewModel(string caption)
        {
            Caption = caption;
        }

        public override string Title => "Photo";
        public string Caption { get; }
    }
}
