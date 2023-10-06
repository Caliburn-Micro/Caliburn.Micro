using System;

namespace Caliburn.Micro
{
    /// <summary>
    /// A result that executes an <see cref="System.Action"/>.
    /// </summary>
    public class DelegateResult : IResult
    {
        private readonly Action toExecute;

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateResult"/> class.
        /// </summary>
        /// <param name="action">The action.</param>
        public DelegateResult(Action action)
        {
            toExecute = action;
        }

        /// <summary>
        /// Executes the result using the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public void Execute(CoroutineExecutionContext context)
        {
            var eventArgs = new ResultCompletionEventArgs();

            try
            {
                toExecute();
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

    /// <summary>
    /// A result that executes a <see cref="System.Func&lt;TResult&gt;"/>
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public class DelegateResult<TResult> : IResult<TResult>
    {
        private readonly Func<TResult> toExecute;

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateResult&lt;TResult&gt;"/> class.
        /// </summary>
        /// <param name="action">The action.</param>
        public DelegateResult(Func<TResult> action)
        {
            toExecute = action;
        }

        /// <summary>
        /// Executes the result using the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public void Execute(CoroutineExecutionContext context)
        {
            var eventArgs = new ResultCompletionEventArgs();

            try
            {
                Result = toExecute();
            }
            catch (Exception ex)
            {
                eventArgs.Error = ex;
            }

            Completed(this, eventArgs);
        }

        /// <summary>
        /// Gets the result.
        /// </summary>
        public TResult Result { get; private set; }

        /// <summary>
        /// Occurs when execution has completed.
        /// </summary>
        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };
    }
}
