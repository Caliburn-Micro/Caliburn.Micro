using System;
using System.Collections.Generic;

namespace Caliburn.Micro
{
    /// <summary>
    ///   An implementation of <see cref = "IResult" /> that enables sequential execution of multiple results.
    /// </summary>
    public class SequentialResult : IResult
    {
        private readonly IEnumerator<IResult> enumerator;
        private CoroutineExecutionContext context;

        /// <summary>
        ///   Initializes a new instance of the <see cref = "SequentialResult" /> class.
        /// </summary>
        /// <param name = "enumerator">The enumerator.</param>
        public SequentialResult(IEnumerator<IResult> enumerator)
        {
            this.enumerator = enumerator;
        }

        /// <summary>
        ///   Occurs when execution has completed.
        /// </summary>
        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };

        /// <summary>
        ///   Executes the result using the specified context.
        /// </summary>
        /// <param name = "context">The context.</param>
        public void Execute(CoroutineExecutionContext context)
        {
            this.context = context;
            ChildCompleted(null, new ResultCompletionEventArgs());
        }

        private void ChildCompleted(object sender, ResultCompletionEventArgs args)
        {
            var previous = sender as IResult;
            if (previous != null)
            {
                previous.Completed -= ChildCompleted;
            }

            if (args.Error != null || args.WasCancelled)
            {
                OnComplete(args.Error, args.WasCancelled);
                return;
            }

            var moveNextSucceeded = false;
            try
            {
                moveNextSucceeded = enumerator.MoveNext();
            }
            catch (Exception ex)
            {
                OnComplete(ex, false);
                return;
            }

            if (moveNextSucceeded)
            {
                try
                {
                    var next = enumerator.Current;
                    IoC.BuildUp(next);
                    next.Completed += ChildCompleted;
                    next.Execute(context);
                }
                catch (Exception ex)
                {
                    OnComplete(ex, false);
                    return;
                }
            }
            else
            {
                OnComplete(null, false);
            }
        }

        private void OnComplete(Exception error, bool wasCancelled)
        {
            enumerator.Dispose();
            Completed(this, new ResultCompletionEventArgs { Error = error, WasCancelled = wasCancelled });
        }
    }
}
