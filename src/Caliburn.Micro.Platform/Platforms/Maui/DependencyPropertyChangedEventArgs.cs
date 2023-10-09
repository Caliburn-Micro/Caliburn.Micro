using DependencyProperty = Microsoft.Maui.Controls.BindableProperty;

namespace Caliburn.Micro.Maui {
    /// <summary>
    /// Provides data for a PropertyChangedCallback implementation that is invoked when a dependency property changes its value. Also provides event data for the Control.IsEnabledChanged event and any other event that uses the DependencyPropertyChangedEventHandler delegate.
    /// </summary>
    public class DependencyPropertyChangedEventArgs {
        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyPropertyChangedEventArgs"/> class.
        /// </summary>
        /// <param name="newValue">The new value.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="property">The dependency property.</param>
        public DependencyPropertyChangedEventArgs(object newValue, object oldValue, DependencyProperty property) {
            NewValue = newValue;
            OldValue = oldValue;
            Property = property;
        }

        /// <summary>
        /// Gets the value of the dependency property after the reported change.
        /// </summary>
        ///
        /// <returns>
        /// The dependency property value after the change.
        /// </returns>
        public object NewValue { get; private set; }

        /// <summary>
        /// Gets the value of the dependency property before the reported change.
        /// </summary>
        ///
        /// <returns>
        /// The dependency property value before the change.
        /// </returns>
        public object OldValue { get; private set; }

        /// <summary>
        /// Gets the identifier for the dependency property where the value change occurred.
        /// </summary>
        ///
        /// <returns>
        /// The identifier field of the dependency property where the value change occurred.
        /// </returns>
        public DependencyProperty Property { get; private set; }
    }
}
