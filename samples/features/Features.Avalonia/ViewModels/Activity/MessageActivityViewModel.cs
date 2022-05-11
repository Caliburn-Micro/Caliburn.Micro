using System;

namespace Features.Avalonia.ViewModels.Activity
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
