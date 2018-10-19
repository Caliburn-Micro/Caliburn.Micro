namespace Caliburn.Micro
{
    /// <summary>
    /// The context used during the execution of a Coroutine.
    /// </summary>
    public class CoroutineExecutionContext
    {
        /// <summary>
        /// The source from which the message originates.
        /// </summary>
        public object Source { get; set; }

        /// <summary>
        /// The view associated with the target.
        /// </summary>
        public object View { get; set; }

        /// <summary>
        /// The instance on which the action is invoked.
        /// </summary>
        public object Target { get; set; }
    }
}
