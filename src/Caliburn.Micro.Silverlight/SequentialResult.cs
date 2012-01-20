namespace Caliburn.Micro {
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///   An implementation of <see cref = "IResult" /> that enables sequential execution of multiple results.
    /// </summary>
    public class SequentialResult : IResult {
        readonly IEnumerator<IResult> enumerator;
        ActionExecutionContext context;

        /// <summary>
        ///   Initializes a new instance of the <see cref = "SequentialResult" /> class.
        /// </summary>
        /// <param name = "enumerator">The enumerator.</param>
        public SequentialResult(IEnumerator<IResult> enumerator) {
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
        public void Execute(ActionExecutionContext context) {
            this.context = context;
            ChildCompleted(null, new ResultCompletionEventArgs());
        }

        void ChildCompleted(object sender, ResultCompletionEventArgs args) {
            var previous = sender as IResult;
            if (previous != null) {
                previous.Completed -= ChildCompleted;
            }

            if(args.Error != null || args.WasCancelled) {
                OnComplete(args.Error, args.WasCancelled);
                return;
            }

            var moveNextSucceeded = false;
            try {
                moveNextSucceeded = enumerator.MoveNext();
            }
            catch(Exception ex) {
                OnComplete(ex, false);
                return;
            }

            if(moveNextSucceeded) {
                try {
                    var next = enumerator.Current;
                    IoC.BuildUp(next);
                    next.Completed += ChildCompleted;
                    next.Execute(context);
                }
                catch(Exception ex) {
                    OnComplete(ex, false);
                    return;
                }
            }
            else {
                OnComplete(null, false);
            }
        }

        void OnComplete(Exception error, bool wasCancelled) {
            enumerator.Dispose();
            Completed(this, new ResultCompletionEventArgs { Error = error, WasCancelled = wasCancelled });
        }
    }
}