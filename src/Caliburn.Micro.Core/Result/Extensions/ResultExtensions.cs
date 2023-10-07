using System;
using System.Threading.Tasks;

namespace Caliburn.Micro;

/// <summary>
/// Extension methods for <see cref="IResult"/> instances.
/// </summary>
public static class ResultExtensions {
    /// <summary>
    /// Adds behavior to the result which is executed when the <paramref name ="result"/> was cancelled.
    /// </summary>
    /// <param name="result">The result to decorate.</param>
    /// <param name="coroutine">The coroutine to execute when <paramref name="result"/> was canceled.</param>
    /// <returns></returns>
    public static IResult WhenCancelled(this IResult result, Func<IResult> coroutine)
        => new ContinueResultDecorator(result, coroutine);

    /// <summary>
    /// Overrides <see cref="ResultCompletionEventArgs.WasCancelled"/> of the decorated <paramref name="result"/> instance.
    /// </summary>
    /// <param name="result">The result to decorate.</param>
    /// <returns></returns>
    public static IResult OverrideCancel(this IResult result)
        => new OverrideCancelResultDecorator(result);

    /// <summary>
    /// Rescues <typeparamref name="TException"/> from the decorated <paramref name="result"/> by executing a <paramref name="rescue"/> coroutine.
    /// </summary>
    /// <typeparam name = "TException">The type of the exception we want to perform the rescue on.</typeparam>
    /// <param name="result">The result to decorate.</param>
    /// <param name="rescue">The rescue coroutine.</param>
    /// <param name="cancelResult">Set to true to cancel the result after executing rescue.</param>
    /// <returns></returns>
    public static IResult Rescue<TException>(this IResult result, Func<TException, IResult> rescue, bool cancelResult = true)
        where TException : Exception
        => new RescueResultDecorator<TException>(result, rescue, cancelResult);

    /// <summary>
    /// Rescues any exception from the decorated <paramref name="result"/> by executing a <paramref name="rescue"/> coroutine.
    /// </summary>
    /// <param name="result">The result to decorate.</param>
    /// <param name="rescue">The rescue coroutine.</param>
    /// <param name="cancelResult">Set to true to cancel the result after executing rescue.</param>
    /// <returns></returns>
    public static IResult Rescue(this IResult result, Func<Exception, IResult> rescue, bool cancelResult = true)
        => Rescue<Exception>(result, rescue, cancelResult);

    /// <summary>
    /// Executes an <see cref="Caliburn.Micro.IResult"/> asynchronous.
    /// </summary>
    /// <param name="result">The coroutine to execute.</param>
    /// <param name="context">The context to execute the coroutine within.</param>
    /// <returns>A task that represents the asynchronous coroutine.</returns>
    public static Task ExecuteAsync(this IResult result, CoroutineExecutionContext context = null)
        => InternalExecuteAsync<object>(result, context);

    /// <summary>
    /// Executes an <see cref="Caliburn.Micro.IResult&lt;TResult&gt;"/> asynchronous.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="result">The coroutine to execute.</param>
    /// <param name="context">The context to execute the coroutine within.</param>
    /// <returns>A task that represents the asynchronous coroutine.</returns>
    public static Task<TResult> ExecuteAsync<TResult>(this IResult<TResult> result, CoroutineExecutionContext context = null)
        => InternalExecuteAsync<TResult>(result, context);

    /// <summary>
    /// Encapsulates a task inside a couroutine.
    /// </summary>
    /// <param name="task">The task.</param>
    /// <returns>The coroutine that encapsulates the task.</returns>
    public static TaskResult AsResult(this Task task)
        => new(task);

    /// <summary>
    /// Encapsulates a task inside a couroutine.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="task">The task.</param>
    /// <returns>The coroutine that encapsulates the task.</returns>
    public static TaskResult<TResult> AsResult<TResult>(this Task<TResult> task)
        => new(task);

    private static Task<TResult> InternalExecuteAsync<TResult>(IResult result, CoroutineExecutionContext context) {
        var taskSource = new TaskCompletionSource<TResult>();

        void OnCompleted(object s, ResultCompletionEventArgs e) {
            result.Completed -= OnCompleted;

            if (e.Error != null) {
                taskSource.SetException(e.Error);

                return;
            }

            if (e.WasCancelled) {
                taskSource.SetCanceled();

                return;
            }

            taskSource.SetResult(result is IResult<TResult> rr ? rr.Result : default);
        }

        try {
            IoC.BuildUp(result);
            result.Completed += OnCompleted;
            result.Execute(context ?? new CoroutineExecutionContext());
        } catch (Exception ex) {
            result.Completed -= OnCompleted;
            taskSource.SetException(ex);
        }

        return taskSource.Task;
    }
}
