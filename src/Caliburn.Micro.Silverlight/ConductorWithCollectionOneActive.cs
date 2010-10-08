namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;

    public partial class Conductor<T>
    {
        /// <summary>
        /// An implementation of <see cref="IConductor"/> that holds on to many items.
        /// </summary>
        public partial class Collection
        {
            /// <summary>
            /// An implementation of <see cref="IConductor"/> that holds on to many items but only activates one at a time.
            /// </summary>
            public class OneActive : ConductorBase<T>
            {
                readonly BindableCollection<T> items = new BindableCollection<T>();

                /// <summary>
                /// Initializes a new instance of the <see cref="Conductor&lt;T&gt;.Collection.OneActive"/> class.
                /// </summary>
                public OneActive()
                {
                    items.CollectionChanged += (s, e) =>
                    {
                        switch (e.Action)
                        {
                            case NotifyCollectionChangedAction.Add:
                            case NotifyCollectionChangedAction.Replace:
                                e.NewItems.OfType<IChild<IConductor>>().Apply(x => x.Parent = this);
                                break;
                            case NotifyCollectionChangedAction.Reset:
                                items.OfType<IChild<IConductor>>().Apply(x => x.Parent = this);
                                break;
                        }
                    };
                }

                /// <summary>
                /// Gets the items that are currently being conducted.
                /// </summary>
                public IObservableCollection<T> Items
                {
                    get { return items; }
                }

                /// <summary>
                /// Gets all the items currently being conducted.
                /// </summary>
                /// <returns></returns>
                protected override IEnumerable<T> GetConductedItems()
                {
                    return items;
                }

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

                    ChangeActiveItem(item, false);
                }

                /// <summary>
                /// Closes the specified item.
                /// </summary>
                /// <param name="item">The item to close.</param>
                public override void CloseItem(T item)
                {
                    if (item == null)
                        return;

                    CloseStrategy.Execute(new[] { item }, (canClose, closable) =>
                    {
                        if (canClose)
                            CloseItemCore(item);
                    });
                }

                void CloseItemCore(T item)
                {
                    if (item.Equals(ActiveItem))
                    {
                        var index = items.IndexOf(item);
                        var next = DetermineNextItemToActivate(index);

                        ChangeActiveItem(next, true);
                    }
                    else
                    {
                        var deactivator = item as IDeactivate;
                        if (deactivator != null)
                            deactivator.Deactivate(true);
                    }

                    items.Remove(item);
                }

                /// <summary>
                /// Determines the next item to activate based on the last active index.
                /// </summary>
                /// <param name="lastIndex">The index of the last active item.</param>
                /// <returns>The next item to activate.</returns>
                /// <remarks>Called after an active item is closed.</remarks>
                protected virtual T DetermineNextItemToActivate(int lastIndex)
                {
                    var toRemoveAt = lastIndex - 1;

                    if (toRemoveAt == -1 && items.Count > 1)
                        return items[1];
                    if (toRemoveAt > -1 && toRemoveAt < items.Count - 1)
                        return items[toRemoveAt];
                    return default(T);
                }

                /// <summary>
                /// Called to check whether or not this instance can close.
                /// </summary>
                /// <param name="callback">The implementer calls this action with the result of the close check.</param>
                public override void CanClose(Action<bool> callback)
                {
                    CloseStrategy.Execute(items, (canClose, closable) => {
                        closable.Apply(CloseItemCore);
                        callback(canClose);
                    });
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
                    if (close)
                        items.OfType<IDeactivate>().Apply(x => x.Deactivate(true));
                    else
                    {
                        var deactivator = ActiveItem as IDeactivate;
                        if (deactivator != null)
                            deactivator.Deactivate(false);
                    }
                }

                /// <summary>
                /// Ensures that an item is ready to be activated.
                /// </summary>
                /// <param name="newItem"></param>
                /// <returns>The item to be activated.</returns>
                protected override T EnsureItem(T newItem)
                {
                    if (newItem == null)
                        newItem = DetermineNextItemToActivate(ActiveItem != null ? items.IndexOf(ActiveItem) : 0);
                    else
                    {
                        var index = items.IndexOf(newItem);

                        if (index == -1)
                            items.Add(newItem);
                        else newItem = items[index];
                    }

                    return base.EnsureItem(newItem);
                }
            }
        }
    }
}