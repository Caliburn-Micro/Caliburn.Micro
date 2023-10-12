using Microsoft.Maui.Controls;

namespace Caliburn.Micro.Maui {
    /// <summary>
    /// Interaface usually from the Interactivity SDK's included here for completeness.
    /// </summary>
    public interface IAttachedObject {
        /// <summary>
        /// Gets the currently attached object.
        /// </summary>
        BindableObject AssociatedObject { get; }

        /// <summary>
        /// Attached the specified dependency object.
        /// </summary>
        /// <param name="dependencyObject">The bindable object.</param>
        void Attach(BindableObject dependencyObject);

        /// <summary>
        /// Detach from the previously attached object.
        /// </summary>
        void Detach();
    }
}
