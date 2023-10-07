using System;

namespace Caliburn.Micro
{
    /// <summary>
    /// A result that executes an <see cref="System.Action"/>.
    /// </summary>
    public class DelegateResult : IResult
    {
        private readonly Action _toExecute;

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateResult"/> class.
        /// </summary>
        /// <param name="action">The action.</param>
        public DelegateResult(Action action) => _toExecute = action;

        /// <summary>
        /// Executes the result using the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public void Execute(CoroutineExecutionContext context)
        {
            var eventArgs = new ResultCompletionEventArgs();

            try
            {
                _toExecute();
            }
            catch (Exception ex)
            {
                eventArgs.Error = ex;
            }

            Completed(this, eventArgs);
        }

        /// <summary>
        /// Occurs when execution has completed.
        /// </summary>
        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };
    }
}
