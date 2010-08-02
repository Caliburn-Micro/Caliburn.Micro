namespace Caliburn.Micro
{
    /// <summary>
    /// Enables a class to configure the task which it has requested execution for.
    /// </summary>
    /// <typeparam name="TTask">The type of task to be configured.</typeparam>
    public interface IConfigureTask<TTask>
    {
        /// <summary>
        /// Configures the task.
        /// </summary>
        /// <param name="task">The task.</param>
        void ConfigureTask(TTask task);
    }
}