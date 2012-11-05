using System;

namespace Caliburn.Micro.WinRT.Sample.Results
{
    public abstract class ResultBase : IResult
    {
        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };

        protected virtual void OnCompleted()
        {
            OnCompleted(new ResultCompletionEventArgs());
        }

        protected virtual void OnError(Exception error)
        {
            OnCompleted(new ResultCompletionEventArgs
            {
                Error = error
            });
        }

        protected virtual void OnCancelled()
        {
            OnCompleted(new ResultCompletionEventArgs
            {
                WasCancelled = true
            });
        }

        protected virtual void OnCompleted(ResultCompletionEventArgs e)
        {
            Micro.Execute.OnUIThread(() => Completed(this, e));
        }

        public abstract void Execute(ActionExecutionContext context);
    }
}
