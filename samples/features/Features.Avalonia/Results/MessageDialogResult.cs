﻿using System;
using Caliburn.Micro;


namespace Features.Avalonia.Results
{
    public class MessageDialogResult : ResultBase
    {
        private readonly string content;
        private readonly string title;

        public MessageDialogResult(string content, string title)
        {
            this.content = content;
            this.title = title;
        }

        public override async void Execute(CoroutineExecutionContext context)
        { 
            var dialog = new MessageDialog(content, title);

            await dialog.ShowAsync();

            OnCompleted();
        }
    }
}
