namespace Caliburn.Micro
{
    /// <summary>
    /// Hosts extension methods for <see cref="IScreen"/> classes.
    /// </summary>
    public static class ScreenExtensions
    {
        /// <summary>
        /// Activates the item if it implements <see cref="IActivate"/>, otherwise does nothing.
        /// </summary>
        /// <param name="potentialActivatable">The potential activatable.</param>
        public static void TryActivate(object potentialActivatable)
        {
            var activator = potentialActivatable as IActivate;
            if (activator != null)
                activator.Activate();
        }

        /// <summary>
        /// Deactivates the item if it implements <see cref="IDeactivate"/>, otherwise does nothing.
        /// </summary>
        /// <param name="potentialDeactivatable">The potential deactivatable.</param>
        /// <param name="close">Indicates whether or not to close the item after deactivating it.</param>
        public static void TryDeactivate(object potentialDeactivatable, bool close)
        {
            var deactivator = potentialDeactivatable as IDeactivate;
            if (deactivator != null)
                deactivator.Deactivate(close);
        }

        /// <summary>
        /// Closes the specified item.
        /// </summary>
        /// <param name="conductor">The conductor.</param>
        /// <param name="item">The item to close.</param>
        public static void CloseItem(this IConductor conductor, object item)
        {
            conductor.DeactivateItem(item, true);
        }

        /// <summary>
        /// Closes the specified item.
        /// </summary>
        /// <param name="conductor">The conductor.</param>
        /// <param name="item">The item to close.</param>
        public static void CloseItem<T>(this ConductorBase<T> conductor, T item)
        {
            conductor.DeactivateItem(item, true);
        }
    }
}