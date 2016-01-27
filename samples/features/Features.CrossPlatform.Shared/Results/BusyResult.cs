#if XAMARINFORMS
using System;
using Caliburn.Micro;
using Xamarin.Forms;

namespace Features.CrossPlatform.Results
{
    public class BusyResult : ResultBase
    {
        private readonly bool isBusy;

        public BusyResult(bool isBusy)
        {
            this.isBusy = isBusy;
        }

        public override void Execute(CoroutineExecutionContext context)
        {
            if (!(context.View is Page))
                throw new InvalidOperationException("View must be a Page to use MessageDialogResult");

            var page = (Page)context.View;

            page.IsBusy = isBusy;

            OnCompleted();
        }
    }
}
#endif
