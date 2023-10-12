namespace Caliburn.Micro;

/// <summary>
/// Denotes a node within a parent/child hierarchy.
/// </summary>
/// <typeparam name="TParent">The type of parent.</typeparam>
public interface IChild<TParent> : IChild {
    /// <summary>
    /// Gets or Sets the Parent.
    /// </summary>
    new TParent Parent { get; set; }
}
