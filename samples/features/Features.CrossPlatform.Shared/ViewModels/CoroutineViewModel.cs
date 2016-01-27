using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Caliburn.Micro;
using Features.CrossPlatform.Results;

namespace Features.CrossPlatform.ViewModels
{
    public class CoroutineViewModel : Screen
    {
        public IEnumerable<IResult> Execute()
        {
#if XAMARINFORMS
            yield return new BusyResult(true);

            yield return TaskHelper.Delay(2000).AsResult();

            yield return new BusyResult(false);
#else
            yield return new VisualStateResult("Loading");

            yield return TaskHelper.Delay(2000).AsResult();

            yield return new VisualStateResult("LoadingComplete");
#endif
            yield return new MessageDialogResult("This was executed from a custom IResult, MessageDialogResult.", "IResult Coroutines");
        }
    }
}
