namespace Caliburn.Micro {
    using System;

    /// <summary>
    /// Denotes a class which is aware of its view(s).
    /// </summary>
    public interface IViewAware {
        /// <summary>
        /// Attaches a view to this instance.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="context">The context in which the view appears.</param>
        void AttachView(object view, object context = null);

        /// <summary>
        /// Gets a view previously attached to this instance.
        /// </summary>
        /// <param name="context">The context denoting which view to retrieve.</param>
        /// <returns>The view.</returns>
        object GetView(object context = null);

        /// <summary>
        /// Raised when a view is attached.
        /// </summary>
        event EventHandler<ViewAttachedEventArgs> ViewAttached;
    }

    /// <summary>
    /// The event args for the <see cref="IViewAware.ViewAttached"/> event.
    /// </summary>
    public class ViewAttachedEventArgs : EventArgs {
        /// <summary>
        /// The view.
        /// </summary>
        public object View;

        /// <summary>
        /// The context.
        /// </summary>
        public object Context;
    }
}