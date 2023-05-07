using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Caliburn.Micro;
using Features.CrossPlatform.Results;

namespace Features.CrossPlatform.ViewModels
{
    public class CoroutineViewModel : Screen
    {
        private bool _showProgressBar;

        public bool ShowProgressBar
        {
            get { return _showProgressBar; }
            set
            {
                _showProgressBar = value;
                NotifyOfPropertyChange(() => ShowProgressBar);
            }
        }

        public IEnumerable<IResult> Execute()
        {
            ShowProgressBar = true;

#if XAMARINFORMS
            yield return new BusyResult(true);

            yield return TaskHelper.Delay(2000).AsResult();

            yield return new BusyResult(false);
#elif AVALONIA
            yield return new VisualStateResult("Loading");

            yield return TaskHelper.Delay(2000).AsResult();

            yield return new VisualStateResult("LoadingComplete");
#else
            yield return new VisualStateResult("Loading");

            yield return TaskHelper.Delay(2000).AsResult();

            yield return new VisualStateResult("LoadingComplete");

#endif
            yield return new MessageDialogResult("This was executed from a custom IResult, MessageDialogResult.", "IResult Coroutines");

             ShowProgressBar = false;
       }
    }
}
