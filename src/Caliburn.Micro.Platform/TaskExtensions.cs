#if !SILVERLIGHT || SL5 || WP8
namespace Caliburn.Micro {
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Extension methods to bring <see cref="System.Threading.Tasks.Task"/> and <see cref="Caliburn.Micro.IResult"/> together.
    /// </summary>
    public static class TaskExtensions {
        /// <summary>
        /// Executes an <see cref="Caliburn.Micro.IResult"/> asynchronous.
        /// </summary>
        /// <param name="result">The coroutine to execute.</param>
        /// <param name="context">The context to execute the coroutine within.</param>
        /// <returns>A task that represents the asynchronous coroutine.</returns>
        public static Task ExecuteAsync(this IResult result, ActionExecutionContext context = null) {
            return InternalExecuteAsync<object>(result, context);
        }

        /// <summary>
        /// Executes an <see cref="Caliburn.Micro.IResult&lt;TResult&gt;"/> asynchronous.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="result">The coroutine to execute.</param>
        /// <param name="context">The context to execute the coroutine within.</param>
        /// <returns>A task that represents the asynchronous coroutine.</returns>
        public static Task<TResult> ExecuteAsync<TResult>(this IResult<TResult> result,
                                                          ActionExecutionContext context = null) {
            return InternalExecuteAsync<TResult>(result, context);
        }

        static Task<TResult> InternalExecuteAsync<TResult>(IResult result, ActionExecutionContext context) {
            var taskSource = new TaskCompletionSource<TResult>();

            EventHandler<ResultCompletionEventArgs> completed = null;
            completed = (s, e) => {
                result.Completed -= completed;

                if (e.Error != null)
                    taskSource.SetException(e.Error);
                else if (e.WasCancelled)
                    taskSource.SetCanceled();
                else {
                    var rr = result as IResult<TResult>;
                    taskSource.SetResult(rr != null ? rr.Result : default(TResult));
                }
            };

            try {
                IoC.BuildUp(result);
                result.Completed += completed;
                result.Execute(context ?? new ActionExecutionContext());
            }
            catch (Exception ex) {
                result.Completed -= completed;
                taskSource.SetException(ex);
            }

            return taskSource.Task;
        }

        /// <summary>
        /// Encapsulates a task inside a couroutine.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns>The coroutine that encapsulates the task.</returns>
        public static TaskResult AsResult(this Task task) {
            return new TaskResult(task);
        }

        /// <summary>
        /// Encapsulates a task inside a couroutine.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="task">The task.</param>
        /// <returns>The coroutine that encapsulates the task.</returns>
        public static TaskResult<TResult> AsResult<TResult>(this Task<TResult> task) {
            return new TaskResult<TResult>(task);
        }
    }

    /// <summary>
    /// A couroutine that encapsulates an <see cref="System.Threading.Tasks.Task"/>.
    /// </summary>
    public class TaskResult : IResult {
        readonly Task innerTask;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskResult"/> class.
        /// </summary>
        /// <param name="task">The task.</param>
        public TaskResult(Task task) {
            innerTask = task;
        }

        /// <summary>
        /// Executes the result using the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public void Execute(ActionExecutionContext context) {
            if (innerTask.IsCompleted)
                OnCompleted(innerTask);
            else
                innerTask.ContinueWith(OnCompleted,
                                       System.Threading.SynchronizationContext.Current != null
                                           ? TaskScheduler.FromCurrentSynchronizationContext()
                                           : TaskScheduler.Current);
        }

        /// <summary>
        /// Called when the asynchronous task has completed.
        /// </summary>
        /// <param name="task">The completed task.</param>
        protected virtual void OnCompleted(Task task) {
            Completed(this, new ResultCompletionEventArgs {WasCancelled = task.IsCanceled, Error = task.Exception});
        }

        /// <summary>
        /// Occurs when execution has completed.
        /// </summary>
        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };
    }

    /// <summary>
    /// A couroutine that encapsulates an <see cref="System.Threading.Tasks.Task&lt;TResult&gt;"/>.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public class TaskResult<TResult> : TaskResult, IResult<TResult> {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaskResult{TResult}"/> class.
        /// </summary>
        /// <param name="task">The task.</param>
        public TaskResult(Task<TResult> task)
            : base(task) {
        }

        /// <summary>
        /// Gets the result of the asynchronous operation.
        /// </summary>
        public TResult Result { get; private set; }

        /// <summary>
        /// Called when the asynchronous task has completed.
        /// </summary>
        /// <param name="task">The completed task.</param>
        protected override void OnCompleted(Task task) {
            if (!task.IsFaulted && !task.IsCanceled)
                Result = ((Task<TResult>) task).Result;

            base.OnCompleted(task);
        }
    }

    /// <summary>
    ///  Denotes a class which can handle a particular type of message and uses a Task to do so.
    /// </summary>
    public interface IHandleWithTask<TMessage> : IHandle { //don't use contravariance here
        /// <summary>
        ///  Handle the message with a Task.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The Task that represents the operation.</returns>
        Task Handle(TMessage message);
    }
}
#endif
