namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;

    public class SequentialResult : IResult
    {
        static readonly ILog Log = LogManager.GetLog(typeof(SequentialResult));
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
            if(args.Error != null)
            {
                OnComplete(args.Error, false);
                Log.Error(args.Error);
                return;
            }

            if(args.WasCancelled)
            {
                OnComplete(null, true);
                Log.Info("{0} was cancelled.", sender);
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
                    next.Completed += ChildCompleted;
                    Log.Info("Executing {0}.", next);
                    next.Execute(context);
                }
                catch(Exception ex)
                {
                    OnComplete(ex, false);
                    Log.Error(ex);
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