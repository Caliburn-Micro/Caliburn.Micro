using System;

namespace Features.CrossPlatform.ViewModels.Activity
{
    public class MessageActivityViewModel : ActivityBaseViewModel
    {
        public MessageActivityViewModel(string message)
        {
            Message = message;
        }

        public override string Title => "Message";
        public string Message { get; }
    }
}
