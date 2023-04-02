using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Caliburn.Micro;
using Features.Avalonia.Results;

namespace Features.Avalonia.ViewModels
{
    public class CoroutineViewModel : Screen
    {
        public IEnumerable<IResult> Execute()
        {

            yield return new VisualStateResult("Loading");

            yield return TaskHelper.Delay(2000).AsResult();

            yield return new VisualStateResult("LoadingComplete");
            yield return new MessageDialogResult("This was executed from a custom IResult, MessageDialogResult.", "IResult Coroutines");
        }
    }
}
