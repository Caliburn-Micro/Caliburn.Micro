namespace Caliburn.Micro
{
    /// <summary>
    /// Denotes a node within a parent/child hierarchy.
    /// </summary>
    /// <typeparam name="TParent">The type of parent.</typeparam>
    public interface IChild<TParent>
    {
        /// <summary>
        /// Gets or Sets the Parent
        /// </summary>
        TParent Parent { get; set; }
    }
}