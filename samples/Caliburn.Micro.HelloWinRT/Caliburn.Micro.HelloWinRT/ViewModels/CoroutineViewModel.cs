using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

            // You can use an async operation inside a Coroutine by using AsResult() extension methods
            yield return Task.Delay(2000).AsResult();
            
            yield return new VisualStateResult("LoadingComplete");
            yield return new MessageDialogResult("This was executed from a custom IResult, MessageDialogResult.", "IResult Coroutines");
        }
    }
}
