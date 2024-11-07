using System;

namespace Features.Avalonia.Messages
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
