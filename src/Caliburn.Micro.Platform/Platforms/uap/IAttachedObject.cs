using Windows.UI.Xaml;

namespace Caliburn.Micro {
    /// <summary>
    /// Interaface usually from the Interactivity SDK's included here for completeness.
    /// </summary>
    public interface IAttachedObject {
        /// <summary>
        /// Gets the currently attached object.
        /// </summary>
        DependencyObject AssociatedObject { get; }

        /// <summary>
        /// Attached the specified dependency object.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        void Attach(DependencyObject dependencyObject);

        /// <summary>
        /// Detach from the previously attached object.
        /// </summary>
        void Detach();
    }
}
