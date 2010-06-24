namespace Caliburn.Micro
{
    /// <summary>
    /// A base class for various implementations of <see cref="IConductor"/>.
    /// </summary>
    /// <typeparam name="T">The type that is being conducted.</typeparam>
    public abstract class ConductorBase<T> : Screen, IConductor
    {
        T activeItem;

        /// <summary>
        /// The currently active item.
        /// </summary>
        public T ActiveItem
        {
            get { return activeItem; }
            set { ActivateItem(value); }
        }

        object IConductor.ActiveItem
        {
            get { return ActiveItem; }
            set { ActiveItem = (T)value; }
        }

        void IConductor.ActivateItem(object item)
        {
            ActivateItem((T)item);
        }

        void IConductor.CloseItem(object item)
        {
            CloseItem((T)item);
        }

        /// <summary>
        /// Activates the specified item.
        /// </summary>
        /// <param name="item">The item to activate.</param>
        public abstract void ActivateItem(T item);

        /// <summary>
        /// Closes the specified item.
        /// </summary>
        /// <param name="item">The item to close.</param>
        public abstract void CloseItem(T item);

        /// <summary>
        /// Changes the active item.
        /// </summary>
        /// <param name="newItem">The new item to activate.</param>
        /// <param name="closePrevious">Indicates whether or not to close the previous active item.</param>
        protected virtual void ChangeActiveItem(T newItem, bool closePrevious)
        {
            var deactivator = activeItem as IDeactivate;
            if (deactivator != null)
                deactivator.Deactivate(closePrevious);

            newItem = EnsureItem(newItem);

            if (IsActive)
            {
                var activator = newItem as IActivate;
                if (activator != null)
                    activator.Activate();
            }

            activeItem = newItem;

            NotifyOfPropertyChange("ActiveItem");
        }

        /// <summary>
        /// Ensures that an item is ready to be activated.
        /// </summary>
        /// <param name="newItem"></param>
        /// <returns>The item to be activated.</returns>
        protected virtual T EnsureItem(T newItem)
        {
            var node = newItem as IChild<IConductor>;
            if (node != null)
                node.Parent = this;

            return newItem;
        }
    }
}