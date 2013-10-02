namespace Caliburn.Micro {
    using System;

    /// <summary>
    /// Allows custom code to execute after the return of a action.
    /// </summary>
    public interface IResult {
        /// <summary>
        /// Executes the result using the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        void Execute(ActionExecutionContext context);

        /// <summary>
        /// Occurs when execution has completed.
        /// </summary>
        event EventHandler<ResultCompletionEventArgs> Completed;
    }

#if !SILVERLIGHT || SL5 || WP8
    /// <summary>
    /// Allows custom code to execute after the return of a action.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public interface IResult<out TResult> : IResult
    {
        /// <summary>
        /// Gets the result of the asynchronous operation.
        /// </summary>
        TResult Result { get; }
    }
#endif
}
