namespace Caliburn.Micro
{
    using System;

    /// <summary>
    /// Used by InstanceActivator to carry event specific information when task launch is requested.
    /// </summary>
    public class TaskLaunchEventArgs : EventArgs
    {
        /// <summary>
        /// The type of task to launch.
        /// </summary>
        public Type TaskType { get; set; }

        /// <summary>
        /// Creates an instance of the event args.
        /// </summary>
        /// <typeparam name="TTask">The type of task to launch.</typeparam>
        /// <returns>The event args.</returns>
        public static TaskLaunchEventArgs For<TTask>()
        {
            return new TaskLaunchEventArgs {
                TaskType = typeof(TTask)
            };
        }
    }
}