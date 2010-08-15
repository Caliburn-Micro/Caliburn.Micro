namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// An implementation of <see cref="IConductor"/> that holds on to and activates only one item at a time.
    /// </summary>
    public partial class Conductor<T> : ConductorBase<T>
    {
        /// <summary>
        /// Activates the specified item.
        /// </summary>
        /// <param name="item">The item to activate.</param>
        public override void ActivateItem(T item)
        {
            if (item != null && item.Equals(ActiveItem))
            {
                OnActivationProcessed(item, true);
                return;
            }

            CloseStrategy.Execute(new[] { ActiveItem }, (canClose, items) =>
            {
                if (canClose)
                    ChangeActiveItem(item, true);
                else OnActivationProcessed(item, false);
            });
        }

        /// <summary>
        /// Closes the specified item.
        /// </summary>
        /// <param name="item">The item to close.</param>
        public override void CloseItem(T item)
        {
            if (item == null || !item.Equals(ActiveItem))
                return;

            CloseStrategy.Execute(new[] { ActiveItem }, (canClose, items) => 
            {
                if (canClose)
                    ChangeActiveItem(default(T), true);
            });
        }

        /// <summary>
        /// Called to check whether or not this instance can close.
        /// </summary>
        /// <param name="callback">The implementer calls this action with the result of the close check.</param>
        public override void CanClose(Action<bool> callback)
        {
            CloseStrategy.Execute(new[] { ActiveItem }, (canClose, items) => callback(canClose));
        }

        /// <summary>
        /// Called when activating.
        /// </summary>
        protected override void OnActivate()
        {
            var activator = ActiveItem as IActivate;
            if (activator != null)
                activator.Activate();
        }

        /// <summary>
        /// Called when deactivating.
        /// </summary>
        /// <param name="close">Indicates whether this instance will be closed.</param>
        protected override void OnDeactivate(bool close)
        {
            var deactivator = ActiveItem as IDeactivate;
            if (deactivator != null)
                deactivator.Deactivate(close);
        }

        /// <summary>
        /// Gets all the items currently being conducted.
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<T> GetConductedItems()
        {
            return new[] { ActiveItem };
        }
    }
}