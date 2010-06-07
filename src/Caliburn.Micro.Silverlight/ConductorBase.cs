namespace Caliburn.Micro
{
    public abstract class ConductorBase<T> : Screen, IConductor
    {
        T activeItem;

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

        public abstract void ActivateItem(T item);
        public abstract void CloseItem(T item);

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

            Log.Info("Changed active item of {0} to {1}", this, newItem);
            NotifyOfPropertyChange(() => ActiveItem);
        }

        protected virtual T EnsureItem(T newItem)
        {
            var node = newItem as IChild<IConductor>;
            if (node != null)
                node.Parent = this;

            return newItem;
        }
    }
}