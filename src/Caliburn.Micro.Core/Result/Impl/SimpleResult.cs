using System;

namespace Caliburn.Micro
{
    /// <summary>
    /// A simple result.
    /// </summary>
    public sealed class SimpleResult : IResult
    {
        private readonly bool wasCancelled;
        private readonly Exception error;

        private SimpleResult(bool wasCancelled, Exception error)
        {
            this.wasCancelled = wasCancelled;
            this.error = error;
        }

        /// <summary>
        /// A result that is always succeeded.
        /// </summary>
        public static IResult Succeeded()
        {
            return new SimpleResult(false, null);
        }

        /// <summary>
        /// A result that is always canceled.
        /// </summary>
        /// <returns>The result.</returns>
        public static IResult Cancelled()
        {
            return new SimpleResult(true, null);
        }

        /// <summary>
        /// A result that is always failed.
        /// </summary>
        public static IResult Failed(Exception error)
        {
            return new SimpleResult(false, error);
        }

        /// <summary>
        /// Executes the result using the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public void Execute(CoroutineExecutionContext context)
        {
            Completed(this, new ResultCompletionEventArgs { WasCancelled = wasCancelled, Error = error });
        }

        /// <summary>
        /// Occurs when execution has completed.
        /// </summary>
        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };
    }
}
