namespace Caliburn.Micro
{
    using System;

    public partial class Conductor<T> : ConductorBase<T>
    {
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

        public override void CanClose(Action<bool> callback)
        {
            var guard = ActiveItem as IGuardClose;

            if(guard == null)
                callback(true);
            else guard.CanClose(callback);
        }

        protected override void OnActivate()
        {
            var activator = ActiveItem as IActivate;

            if(activator != null)
                activator.Activate();
        }

        protected override void OnDeactivate(bool close)
        {
            var deactivator = ActiveItem as IDeactivate;

            if(deactivator != null)
                deactivator.Deactivate(close);
        }
    }
}