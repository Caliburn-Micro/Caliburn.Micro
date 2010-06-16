namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;

    public class SequentialResult : IResult
    {
        readonly IEnumerator<IResult> enumerator;
        ResultExecutionContext context;

        public SequentialResult(IEnumerable<IResult> children)
        {
            enumerator = children.GetEnumerator();
        }

        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };

        public void Execute(ResultExecutionContext context)
        {
            this.context = context;
            ChildCompleted(null, new ResultCompletionEventArgs());
        }

        void ChildCompleted(object sender, ResultCompletionEventArgs args)
        {
            if(args.Error != null || args.WasCancelled)
            {
                OnComplete(args.Error, args.WasCancelled);
                return;
            }

            var previous = sender as IResult;

            if(previous != null)
                previous.Completed -= ChildCompleted;

            if(enumerator.MoveNext())
            {
                try
                {
                    var next = enumerator.Current;
                    IoC.BuildUp(next);
                    next.Completed += ChildCompleted;
                    next.Execute(context);
                }
                catch(Exception ex)
                {
                    OnComplete(ex, false);
                    return;
                }
            }
            else OnComplete(null, false);
        }

        void OnComplete(Exception error, bool wasCancelled)
        {
            Completed(this, new ResultCompletionEventArgs { Error = error,  WasCancelled = wasCancelled });
        }
    }
}