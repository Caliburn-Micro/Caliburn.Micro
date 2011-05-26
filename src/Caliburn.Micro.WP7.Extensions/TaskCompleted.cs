namespace Caliburn.Micro {
    /// <summary>
    /// A message which is published when a task completes.
    /// </summary>
    /// <typeparam name="TTaskEventArgs">The type of the task event args.</typeparam>
    public class TaskCompleted<TTaskEventArgs> {
        /// <summary>
        /// Optional state provided by the original sender.
        /// </summary>
        public object State;

        /// <summary>
        /// The results of the task.
        /// </summary>
        public TTaskEventArgs Result;
    }
}