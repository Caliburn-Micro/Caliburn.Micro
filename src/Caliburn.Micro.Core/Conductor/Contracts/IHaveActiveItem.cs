namespace Caliburn.Micro;

/// <summary>
/// Denotes an instance which maintains an active item.
/// </summary>
public interface IHaveActiveItem {
    /// <summary>
    /// Gets or sets the currently active item.
    /// </summary>
    object ActiveItem { get; set; }
}
