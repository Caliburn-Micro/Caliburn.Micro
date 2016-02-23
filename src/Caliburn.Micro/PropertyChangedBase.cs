namespace Caliburn.Micro {
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Runtime.Serialization;

    /// <summary>
    /// A base class that implements the infrastructure for property change notification and automatically performs UI thread marshalling.
    /// </summary>
    [DataContract]
    public class PropertyChangedBase : INotifyPropertyChangedEx {
        /// <summary>
        /// Creates an instance of <see cref = "PropertyChangedBase" />.
        /// </summary>
        public PropertyChangedBase() {
            IsNotifying = true;
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public virtual event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Enables/Disables property change notification.
        /// Virtualized in order to help with document oriented view models.
        /// </summary>
        public virtual bool IsNotifying { get; set; }

        /// <summary>
        /// Raises a change notification indicating that all bindings should be refreshed.
        /// </summary>
        public virtual void Refresh() {
            NotifyOfPropertyChange(string.Empty);
        }

        /// <summary>
        /// Notifies subscribers of the property change.
        /// </summary>
        /// <param name = "propertyName">Name of the property.</param>
#if NET || SILVERLIGHT
        public virtual void NotifyOfPropertyChange(string propertyName) {
#else
        public virtual void NotifyOfPropertyChange([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null) {
#endif
            if (IsNotifying && PropertyChanged != null) {
                Execute.OnUIThread(() => OnPropertyChanged(new PropertyChangedEventArgs(propertyName)));
            }
        }

        /// <summary>
        /// Notifies subscribers of the property change.
        /// </summary>
        /// <typeparam name = "TProperty">The type of the property.</typeparam>
        /// <param name = "property">The property expression.</param>
        public void NotifyOfPropertyChange<TProperty>(Expression<Func<TProperty>> property) {
            NotifyOfPropertyChange(property.GetMemberInfo().Name);
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged" /> event directly.
        /// </summary>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void OnPropertyChanged(PropertyChangedEventArgs e) {
            var handler = PropertyChanged;
            if (handler != null) {
                handler(this, e);
            }
        }
    }
}
