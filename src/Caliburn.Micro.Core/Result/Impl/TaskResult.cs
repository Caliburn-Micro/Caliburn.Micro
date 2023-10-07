using System;
using System.Threading.Tasks;

namespace Caliburn.Micro
{
    /// <summary>
    /// A couroutine that encapsulates an <see cref="Task"/>.
    /// </summary>
    public class TaskResult : IResult
    {
        private readonly Task _innerTask;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskResult"/> class.
        /// </summary>
        /// <param name="task">The task.</param>
        public TaskResult(Task task) => _innerTask = task;

        /// <summary>
        /// Executes the result using the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public void Execute(CoroutineExecutionContext context)
        {
            if (_innerTask.IsCompleted)
            {
                OnCompleted(_innerTask);
            }
            else
            {
                _innerTask.ContinueWith(OnCompleted,
                    System.Threading.SynchronizationContext.Current != null
                        ? TaskScheduler.FromCurrentSynchronizationContext()
                        : TaskScheduler.Current);
            }
        }

        /// <summary>
        /// Called when the asynchronous task has completed.
        /// </summary>
        /// <param name="task">The completed task.</param>
        protected virtual void OnCompleted(Task task) => Completed(this, new ResultCompletionEventArgs { WasCancelled = task.IsCanceled, Error = task.Exception });

        /// <summary>
        /// Occurs when execution has completed.
        /// </summary>
        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };
    }
}
