using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace Caliburn.Micro;

/// <summary>
/// A base class that implements the infrastructure for property change notification and automatically performs UI thread marshalling.
/// </summary>
[DataContract]
public class PropertyChangedBase : INotifyPropertyChangedEx {
    /// <summary>
    /// Initializes a new instance of the <see cref="PropertyChangedBase"/> class.
    /// </summary>
    public PropertyChangedBase()
        => IsNotifying = true;

    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    public virtual event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Gets or sets a value indicating whether to Enable/Disable property change notification.
    /// Virtualized in order to help with document oriented view models.
    /// </summary>
    public virtual bool IsNotifying { get; set; }

    /// <summary>
    /// Raises a change notification indicating that all bindings should be refreshed.
    /// </summary>
    public virtual void Refresh()
        => NotifyOfPropertyChange(string.Empty);

    /// <summary>
    /// Notifies subscribers of the property change.
    /// </summary>
    /// <param name = "propertyName">Name of the property.</param>
    public virtual void NotifyOfPropertyChange([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null) {
        if (!IsNotifying || PropertyChanged == null) {
            return;
        }

        if (!PlatformProvider.Current.PropertyChangeNotificationsOnUIThread) {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));

            return;
        }

        OnUIThread(() => OnPropertyChanged(new PropertyChangedEventArgs(propertyName)));
    }

    /// <summary>
    /// Notifies subscribers of the property change.
    /// </summary>
    /// <typeparam name = "TProperty">The type of the property.</typeparam>
    /// <param name = "property">The property expression.</param>
    public void NotifyOfPropertyChange<TProperty>(Expression<Func<TProperty>> property)
        => NotifyOfPropertyChange(property.GetMemberInfo().Name);

    /// <summary>
    /// Sets a backing field value and if it's changed raise a notification.
    /// </summary>
    /// <typeparam name="T">The type of the value being set.</typeparam>
    /// <param name="oldValue">A reference to the field to update.</param>
    /// <param name="newValue">The new value.</param>
    /// <param name="propertyName">The name of the property for change notifications.</param>
    public virtual bool Set<T>(ref T oldValue, T newValue, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = null) {
        if (EqualityComparer<T>.Default.Equals(oldValue, newValue)) {
            return false;
        }

        oldValue = newValue;

        NotifyOfPropertyChange(propertyName ?? string.Empty);

        return true;
    }

    /// <summary>
    /// Raises the <see cref="PropertyChanged" /> event directly.
    /// </summary>
    /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    protected void OnPropertyChanged(PropertyChangedEventArgs e)
        => PropertyChanged?.Invoke(this, e);

    /// <summary>
    /// Executes the given action on the UI thread.
    /// </summary>
    /// <remarks>An extension point for subclasses to customise how property change notifications are handled.</remarks>
    protected virtual void OnUIThread(System.Action action)
        => action.OnUIThread();
}
