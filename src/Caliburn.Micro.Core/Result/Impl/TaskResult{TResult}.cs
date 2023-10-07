using System;
using System.Threading.Tasks;

namespace Caliburn.Micro
{
    /// <summary>
    /// A couroutine that encapsulates an <see cref="System.Threading.Tasks.Task&lt;TResult&gt;"/>.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public class TaskResult<TResult> : TaskResult, IResult<TResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaskResult{TResult}"/> class.
        /// </summary>
        /// <param name="task">The task.</param>
        public TaskResult(Task<TResult> task)
            : base(task)
        {
        }

        /// <summary>
        /// Gets the result of the asynchronous operation.
        /// </summary>
        public TResult Result { get; private set; }

        /// <summary>
        /// Called when the asynchronous task has completed.
        /// </summary>
        /// <param name="task">The completed task.</param>
        protected override void OnCompleted(Task task)
        {
            if (!task.IsFaulted && !task.IsCanceled)
            {
                Result = ((Task<TResult>)task).Result;
            }

            base.OnCompleted(task);
        }
    }
}
