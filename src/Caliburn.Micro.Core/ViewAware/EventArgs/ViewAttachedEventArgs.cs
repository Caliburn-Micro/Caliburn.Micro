using System;

namespace Caliburn.Micro;

/// <summary>
/// The event args for the <see cref="IViewAware.ViewAttached"/> event.
/// </summary>
public class ViewAttachedEventArgs : EventArgs {
    /// <summary>
    /// Gets or sets the view.
    /// </summary>
    public object View { get; set; }

    /// <summary>
    /// Gets or sets the context.
    /// </summary>
    public object Context { get; set; }
}
