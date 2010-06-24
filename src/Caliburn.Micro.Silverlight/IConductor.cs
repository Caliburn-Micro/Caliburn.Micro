namespace Caliburn.Micro
{
    /// <summary>
    /// Denotes an instance which conducts other objects by managing an ActiveItem and maintaining a strict lifecycle.
    /// </summary>
    /// <remarks>Conducted instances can optin to the lifecycle by impelenting any of the follosing <see cref="IActivate"/>, <see cref="IDeactivate"/>, <see cref="IGuardClose."/></remarks>
    public interface IConductor
    {
        /// <summary>
        /// The currently active item.
        /// </summary>
        object ActiveItem { get; set; }

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
    }
}