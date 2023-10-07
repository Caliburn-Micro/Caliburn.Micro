using System.ComponentModel;

namespace Caliburn.Micro;

/// <summary>
/// Extends <see cref = "INotifyPropertyChanged" /> such that the change event can be raised by external parties.
/// </summary>
public interface INotifyPropertyChangedEx : INotifyPropertyChanged {
    /// <summary>
    /// Gets or sets a value indicating whether to enable/Disable property change notification.
    /// </summary>
    bool IsNotifying { get; set; }

    /// <summary>
    /// Notifies subscribers of the property change.
    /// </summary>
    /// <param name = "propertyName">Name of the property.</param>
    void NotifyOfPropertyChange(string propertyName);

    /// <summary>
    /// Raises a change notification indicating that all bindings should be refreshed.
    /// </summary>
    void Refresh();
}
