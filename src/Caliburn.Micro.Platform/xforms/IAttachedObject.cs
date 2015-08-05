namespace Caliburn.Micro.Xamarin.Forms 
{
    using global::Xamarin.Forms;

    /// <summary>
    /// Interaface usually from the Interactivity SDK's included here for completeness.
    /// </summary>
    public interface IAttachedObject {
        /// <summary>
        /// Attached the specified dependency object
        /// </summary>
        /// <param name="dependencyObject"></param>
        void Attach(BindableObject dependencyObject);

        /// <summary>
        /// Detach from the previously attached object.
        /// </summary>
        void Detach();

        /// <summary>
        /// The currently attached object.
        /// </summary>
        BindableObject AssociatedObject
        {
            get;
        }
    }
}
