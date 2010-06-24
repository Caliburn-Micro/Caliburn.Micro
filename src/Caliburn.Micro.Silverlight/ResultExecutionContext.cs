namespace Caliburn.Micro
{
    using System.Windows;

    /// <summary>
    /// The context in which an <see cref="IResult"/> executes.
    /// </summary>
    public class ResultExecutionContext
    {
        /// <summary>
        /// Gets or Sets the message.
        /// </summary>
        /// <value>The message.</value>
        public ActionMessage Message;

        /// <summary>
        /// Gets or Sets the element which caused the message to be sent.
        /// </summary>
        public FrameworkElement Source;

        /// <summary>
        /// The instance that received the message.
        /// </summary>
        public object Target;

        /// <summary>
        /// The view that is rendering the target.
        /// </summary>
        public DependencyObject View;
    }
}