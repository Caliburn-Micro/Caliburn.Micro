namespace Caliburn.Micro
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// A base class for various implementations of <see cref="IConductor"/>.
    /// </summary>
    /// <typeparam name="T">The type that is being conducted.</typeparam>
    public abstract class ConductorBase<T> : Screen, IConductor
    {
        T activeItem;
        ICloseStrategy<T> closeStrategy;

        /// <summary>
        /// Gets or sets the close strategy.
        /// </summary>
        /// <value>The close strategy.</value>
        public ICloseStrategy<T> CloseStrategy
        {
            get { return closeStrategy ?? (closeStrategy = new DefaultCloseStrategy<T>()); }
            set { closeStrategy = value; }
        }

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

        IEnumerable IConductor.GetConductedItems()
        {
            return GetConductedItems();
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
        /// Occurs when an activation request is processed.
        /// </summary>
        public event EventHandler<ActivationProcessedEventArgs> ActivationProcessed = delegate { };

        /// <summary>
        /// Gets all the items currently being conducted.
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerable<T> GetConductedItems();

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
        /// Called by a subclass when an activation needs processing.
        /// </summary>
        /// <param name="item">The item on which activation was attempted.</param>
        /// <param name="success">if set to <c>true</c> activation was successful.</param>
        protected virtual void OnActivationProcessed(T item, bool success)
        {
            if (item == null)
                return;

            ActivationProcessed(this, new ActivationProcessedEventArgs
            {
                Item = item,
                Success = success
            });
        }

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
            OnActivationProcessed(activeItem, true);
        }

        /// <summary>
        /// Ensures that an item is ready to be activated.
        /// </summary>
        /// <param name="newItem"></param>
        /// <returns>The item to be activated.</returns>
        protected virtual T EnsureItem(T newItem)
        {
            var node = newItem as IChild<IConductor>;
            if (node != null && node.Parent != this)
                node.Parent = this;

            return newItem;
        }
    }
}