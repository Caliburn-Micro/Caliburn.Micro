using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Features.CrossPlatform.Results;
using Features.CrossPlatform.ViewModels.Activity;

namespace Features.CrossPlatform.ViewModels
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
