namespace Caliburn.Micro;

/// <summary>
/// Denotes a node within a parent/child hierarchy.
/// </summary>
public interface IChild {
    /// <summary>
    /// Gets or Sets the Parent.
    /// </summary>
    object Parent { get; set; }
}
