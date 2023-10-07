using System;

namespace Caliburn.Micro;

/// <summary>
/// Allows custom code to execute after the return of a action.
/// </summary>
public interface IResult {
    /// <summary>
    /// Occurs when execution has completed.
    /// </summary>
    event EventHandler<ResultCompletionEventArgs> Completed;

    /// <summary>
    /// Executes the result using the specified context.
    /// </summary>
    /// <param name="context">The context.</param>
    void Execute(CoroutineExecutionContext context);
}
