namespace Caliburn.Micro
{
    using System;

    /// <summary>
    /// Implemented by classes that request the execution of a class.
    /// </summary>
    public interface ILaunchTask
    {
        /// <summary>
        /// Raised to signal the request for task execution.
        /// </summary>
        event EventHandler<TaskLaunchEventArgs> TaskLaunchRequested;
    }
}