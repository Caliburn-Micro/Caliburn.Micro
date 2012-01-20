namespace Caliburn.Micro {
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;

    public partial class Conductor<T> {
        /// <summary>
        /// An implementation of <see cref="IConductor"/> that holds on many items.
        /// </summary>
        public partial class Collection {
            /// <summary>
            /// An implementation of <see cref="IConductor"/> that holds on many items but only activates on at a time.
            /// </summary>
            public class OneActive : ConductorBaseWithActiveItem<T> {
                readonly BindableCollection<T> items = new BindableCollection<T>();

                /// <summary>
                /// Initializes a new instance of the <see cref="Conductor&lt;T&gt;.Collection.OneActive"/> class.
                /// </summary>
                public OneActive() {
                    items.CollectionChanged += (s, e) => {
                        switch(e.Action) {
                            case NotifyCollectionChangedAction.Add:
                            case NotifyCollectionChangedAction.Replace:
                                e.NewItems.OfType<IChild>().Apply(x => x.Parent = this);
                                break;
                            case NotifyCollectionChangedAction.Reset:
                                items.OfType<IChild>().Apply(x => x.Parent = this);
                                break;
                        }
                    };
                }

                /// <summary>
                /// Gets the items that are currently being conducted.
                /// </summary>
                public IObservableCollection<T> Items {
                    get { return items; }
                }

                /// <summary>
                /// Gets the children.
                /// </summary>
                /// <returns>The collection of children.</returns>
                public override IEnumerable<T> GetChildren() {
                    return items;
                }

                /// <summary>
                /// Activates the specified item.
                /// </summary>
                /// <param name="item">The item to activate.</param>
                public override void ActivateItem(T item) {
                    if(item != null && item.Equals(ActiveItem)) {
                        if (IsActive) {
                            ScreenExtensions.TryActivate(item);
                            OnActivationProcessed(item, true);
                        }

                        return;
                    }

                    ChangeActiveItem(item, false);
                }

                /// <summary>
                /// Deactivates the specified item.
                /// </summary>
                /// <param name="item">The item to close.</param>
                /// <param name="close">Indicates whether or not to close the item after deactivating it.</param>
                public override void DeactivateItem(T item, bool close) {
                    if (item == null) {
                        return;
                    }

                    if (!close) {
                        ScreenExtensions.TryDeactivate(item, false);
                    }
                    else {
                        CloseStrategy.Execute(new[] { item }, (canClose, closable) => {
                            if (canClose) {
                                CloseItemCore(item);
                            }
                        });
                    }
                }

                void CloseItemCore(T item) {
                    if(item.Equals(ActiveItem)) {
                        var index = items.IndexOf(item);
                        var next = DetermineNextItemToActivate(items, index);

                        ChangeActiveItem(next, true);
                    }
                    else {
                        ScreenExtensions.TryDeactivate(item, true);
                    }

                    items.Remove(item);
                }

                /// <summary>
                /// Determines the next item to activate based on the last active index.
                /// </summary>
                /// <param name="list">The list of possible active items.</param>
                /// <param name="lastIndex">The index of the last active item.</param>
                /// <returns>The next item to activate.</returns>
                /// <remarks>Called after an active item is closed.</remarks>
                protected virtual T DetermineNextItemToActivate(IList<T> list, int lastIndex) {
                    var toRemoveAt = lastIndex - 1;

                    if (toRemoveAt == -1 && list.Count > 1) {
                        return list[1];
                    }

                    if (toRemoveAt > -1 && toRemoveAt < list.Count - 1) {
                        return list[toRemoveAt];
                    }

                    return default(T);
                }

                /// <summary>
                /// Called to check whether or not this instance can close.
                /// </summary>
                /// <param name="callback">The implementor calls this action with the result of the close check.</param>
                public override void CanClose(Action<bool> callback) {
                    CloseStrategy.Execute(items, (canClose, closable) => {
                        if(!canClose && closable.Any()) {
                            if(closable.Contains(ActiveItem)) {
                                var list = items.ToList();
                                var next = ActiveItem;
                                do {
                                    var previous = next;
                                    next = DetermineNextItemToActivate(list, list.IndexOf(previous));
                                    list.Remove(previous);
                                } while(closable.Contains(next));

                                var previousActive = ActiveItem;
                                ChangeActiveItem(next, true);
                                items.Remove(previousActive);

                                var stillToClose = closable.ToList();
                                stillToClose.Remove(previousActive);
                                closable = stillToClose;
                            }

                            closable.OfType<IDeactivate>().Apply(x => x.Deactivate(true));
                            items.RemoveRange(closable);
                        }

                        callback(canClose);
                    });
                }

                /// <summary>
                /// Called when activating.
                /// </summary>
                protected override void OnActivate() {
                    ScreenExtensions.TryActivate(ActiveItem);
                }

                /// <summary>
                /// Called when deactivating.
                /// </summary>
                /// <param name="close">Inidicates whether this instance will be closed.</param>
                protected override void OnDeactivate(bool close) {
                    if (close) {
                        items.OfType<IDeactivate>().Apply(x => x.Deactivate(true));
                        items.Clear();
                    }
                    else {
                        ScreenExtensions.TryDeactivate(ActiveItem, false);
                    }
                }

                /// <summary>
                /// Ensures that an item is ready to be activated.
                /// </summary>
                /// <param name="newItem"></param>
                /// <returns>The item to be activated.</returns>
                protected override T EnsureItem(T newItem) {
                    if (newItem == null) {
                        newItem = DetermineNextItemToActivate(items, ActiveItem != null ? items.IndexOf(ActiveItem) : 0);
                    }
                    else {
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