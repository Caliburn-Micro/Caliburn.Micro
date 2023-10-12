namespace Caliburn.Micro;

/// <summary>
/// The context used during the execution of a Coroutine.
/// </summary>
public class CoroutineExecutionContext {
    /// <summary>
    /// Gets or sets the source from which the message originates.
    /// </summary>
    public object Source { get; set; }

    /// <summary>
    /// Gets or sets the view associated with the target.
    /// </summary>
    public object View { get; set; }

    /// <summary>
    /// Gets or sets the instance on which the action is invoked.
    /// </summary>
    public object Target { get; set; }
}
