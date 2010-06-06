namespace Caliburn.Micro
{
    using System;
    using System.Linq;

    public partial class Conductor<T>
    {
        public class Collection
        {
            public class OneActive : ConductorBase<T>
            {
                readonly BindableCollection<T> items = new BindableCollection<T>();

                public BindableCollection<T> Items
                {
                    get { return items; }
                }

                public override void ActivateItem(T item)
                {
                    if(item != null && item.Equals(ActiveItem))
                        return;

                    ChangeActiveItem(item, false);
                }

                public override void CloseItem(T item)
                {
                    if(item == null)
                        return;

                    var guard = item as IGuardClose;

                    if(guard == null)
                        CloseItemCore(item);
                    else
                    {
                        guard.CanClose(result =>{
                            if(result)
                                CloseItemCore(item);
                        });
                    }
                }

                void CloseItemCore(T item)
                {
                    if(item.Equals(ActiveItem))
                    {
                        var index = Items.IndexOf(item);
                        var next = DetermineNextItemToActivate(index);
                        
                        ChangeActiveItem(next, true);
                    }
                    else
                    {
                        var deactivator = item as IDeactivate;
                        if (deactivator != null)
                            deactivator.Deactivate(true);
                    }

                    Items.Remove(item);
                }

                protected virtual T DetermineNextItemToActivate(int lastIndex)
                {
                    var toRemoveAt = lastIndex - 1;

                    if(toRemoveAt == -1 && Items.Count > 1)
                        return Items[1];
                    if(toRemoveAt > -1 && toRemoveAt < Items.Count - 1)
                        return Items[toRemoveAt];
                    return default(T);
                }

                public override void CanClose(Action<bool> callback)
                {
                    new CompositeCloseStrategy<T>(Items.GetEnumerator(), callback).Execute();
                }

                protected override void OnActivate()
                {
                    var activator = ActiveItem as IActivate;

                    if(activator != null)
                        activator.Activate();
                }

                protected override void OnDeactivate(bool close)
                {
                    if(close)
                        items.OfType<IDeactivate>().Apply(x => x.Deactivate(true));
                    else
                    {
                        var deactivator = ActiveItem as IDeactivate;

                        if(deactivator != null)
                            deactivator.Deactivate(false);
                    }
                }

                protected override T EnsureItem(T item)
                {
                    var index = Items.IndexOf(item);

                    if(index == -1)
                        Items.Add(item);
                    else item = Items[index];

                    return base.EnsureItem(item);
                }
            }
        }
    }
}