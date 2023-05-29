using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Features.Avalonia.Results;
using Features.Avalonia.ViewModels.Activity;

namespace Features.Avalonia.ViewModels
{
    public class BubblingViewModel : Screen
    {
        public BindableCollection<MessageActivityViewModel> Phrases { get; } = new BindableCollection<MessageActivityViewModel>
        {
            new MessageActivityViewModel(Lipsum.Get(32)),
            new MessageActivityViewModel(Lipsum.Get(32)),
            new MessageActivityViewModel(Lipsum.Get(32)),
            new MessageActivityViewModel(Lipsum.Get(32))
        };

        public IEnumerable<IResult> SelectPhrase(MessageActivityViewModel phrase)
        {
            yield return new MessageDialogResult($"The selected phrase was {phrase.Message}.", "Phrase Selected");
        }
    }
}
