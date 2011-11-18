namespace Caliburn.Micro {
    /// <summary>
    /// A message that is published to signify a components request for the execution of a particular task.
    /// </summary>
    public class TaskExecutionRequested {
        /// <summary>
        /// Optional state to be passed along to the task completion message.
        /// </summary>
        public object State;

        /// <summary>
        /// The task instance.
        /// </summary>
        public object Task;
    }
}