namespace Caliburn.Micro
{
    using System;
    using System.Collections;

    /// <summary>
    /// Denotes an instance which conducts other objects by managing an ActiveItem and maintaining a strict lifecycle.
    /// </summary>
    /// <remarks>Conducted instances can opt-in to the lifecycle by implementing any of the following <see cref="IActivate"/>, <see cref="IDeactivate"/>, <see cref="IGuardClose"/></remarks>
    public interface IConductor : INotifyPropertyChangedEx
    {
        /// <summary>
        /// The currently active item.
        /// </summary>
        object ActiveItem { get; set; }

        /// <summary>
        /// Gets all the items that are being conducted.
        /// </summary>
        IEnumerable GetConductedItems();

        /// <summary>
        /// Activates the specified item.
        /// </summary>
        /// <param name="item">The item to activate.</param>
        void ActivateItem(object item);

        /// <summary>
        /// Closes the specified item.
        /// </summary>
        /// <param name="item">The item to close.</param>
        void CloseItem(object item);

        /// <summary>
        /// Occurs when an activation request is processed.
        /// </summary>
        event EventHandler<ActivationProcessedEventArgs> ActivationProcessed;
    }
}