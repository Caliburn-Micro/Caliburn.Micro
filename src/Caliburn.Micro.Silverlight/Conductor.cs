namespace Caliburn.Micro
{
    using System;

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
            if(item != null && item.Equals(ActiveItem))
                return;

            var guard = ActiveItem as IGuardClose;

            if(guard == null)
                ChangeActiveItem(item, true);
            else
            {
                guard.CanClose(canClose =>{
                    if(canClose)
                        ChangeActiveItem(item, true);
                });
            }
        }

        /// <summary>
        /// Closes the specified item.
        /// </summary>
        /// <param name="item">The item to close.</param>
        public override void CloseItem(T item)
        {
            if(item == null || !item.Equals(ActiveItem))
                return;

            var guard = ActiveItem as IGuardClose;

            if(guard == null)
                ChangeActiveItem(default(T), true);
            else
            {
                guard.CanClose(result =>{
                    if(result)
                        ChangeActiveItem(default(T), true);
                });
            }
        }

        /// <summary>
        /// Called to check whether or not this instance can close.
        /// </summary>
        /// <param name="callback">The implementor calls this action with the result of the close check.</param>
        public override void CanClose(Action<bool> callback)
        {
            var guard = ActiveItem as IGuardClose;

            if(guard == null)
                callback(true);
            else guard.CanClose(callback);
        }

        /// <summary>
        /// Called when activating.
        /// </summary>
        protected override void OnActivate()
        {
            var activator = ActiveItem as IActivate;

            if(activator != null)
                activator.Activate();
        }

        /// <summary>
        /// Called when deactivating.
        /// </summary>
        /// <param name="close">Inidicates whether this instance will be closed.</param>
        protected override void OnDeactivate(bool close)
        {
            var deactivator = ActiveItem as IDeactivate;

            if(deactivator != null)
                deactivator.Deactivate(close);
        }
    }
}