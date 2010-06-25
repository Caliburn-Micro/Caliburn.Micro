namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// An implementation of <see cref="IResult"/> that enables sequential execution of multiple results.
    /// </summary>
    public class SequentialResult : IResult
    {
        readonly IEnumerator<IResult> enumerator;
        ResultExecutionContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="SequentialResult"/> class.
        /// </summary>
        /// <param name="children">The children.</param>
        public SequentialResult(IEnumerable<IResult> children)
        {
            enumerator = children.GetEnumerator();
        }

        /// <summary>
        /// Occurs when execution has completed.
        /// </summary>
        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };

        /// <summary>
        /// Executes the result using the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public void Execute(ResultExecutionContext context)
        {
            this.context = context;
            IoC.BuildUp(this);
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