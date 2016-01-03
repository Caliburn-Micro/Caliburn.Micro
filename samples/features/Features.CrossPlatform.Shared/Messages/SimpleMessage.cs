using System;

namespace Features.CrossPlatform.Messages
{
    public class SimpleMessage
    {
        public SimpleMessage(string text)
        {
            Text = text;
        }

        public string Text { get; }
    }
}
