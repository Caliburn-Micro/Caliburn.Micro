using System;
using System.Collections.Generic;
using Caliburn.Micro.WinRT.Sample.Results;

namespace Caliburn.Micro.WinRT.Sample.ViewModels
{
    public class CoroutineViewModel : ViewModelBase
    {
        public CoroutineViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }

        public IEnumerable<IResult> Execute()
        {
            yield return new VisualStateResult("Loading");
            yield return new DelayResult(2000);
            yield return new VisualStateResult("LoadingComplete");
            yield return new MessageDialogResult("This was executed from a custom IResult, MessageDialogResult.", "IResult Coroutines");
        }
    }
}
