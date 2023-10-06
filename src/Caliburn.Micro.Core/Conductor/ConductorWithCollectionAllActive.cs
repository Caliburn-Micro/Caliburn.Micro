﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Caliburn.Micro
{
    public partial class Conductor<T>
    {
        /// <summary>
        /// An implementation of <see cref="IConductor"/> that holds on many items.
        /// </summary>
        public partial class Collection
        {
            /// <summary>
            /// An implementation of <see cref="IConductor"/> that holds on to many items which are all activated.
            /// </summary>
            public class AllActive : ConductorBase<T>
            {
                private readonly BindableCollection<T> _items = new BindableCollection<T>();
                private readonly bool _openPublicItems;

                /// <summary>
                /// Initializes a new instance of the <see cref="Conductor&lt;T&gt;.Collection.AllActive"/> class.
                /// </summary>
                /// <param name="openPublicItems">if set to <c>true</c> opens public items that are properties of this class.</param>
                public AllActive(bool openPublicItems)
                    : this()
                {
                    _openPublicItems = openPublicItems;
                }

                /// <summary>
                /// Initializes a new instance of the <see cref="Conductor&lt;T&gt;.Collection.AllActive"/> class.
                /// </summary>
                public AllActive()
                {
                    _items.CollectionChanged += (s, e) =>
                    {
                        switch (e.Action)
                        {
                            case NotifyCollectionChangedAction.Add:
                                e.NewItems.OfType<IChild>().Apply(x => x.Parent = this);
                                break;
                            case NotifyCollectionChangedAction.Remove:
                                e.OldItems.OfType<IChild>().Apply(x => x.Parent = null);
                                break;
                            case NotifyCollectionChangedAction.Replace:
                                e.NewItems.OfType<IChild>().Apply(x => x.Parent = this);
                                e.OldItems.OfType<IChild>().Apply(x => x.Parent = null);
                                break;
                            case NotifyCollectionChangedAction.Reset:
                                _items.OfType<IChild>().Apply(x => x.Parent = this);
                                break;
                        }
                    };
                }

                /// <summary>
                /// Gets the items that are currently being conducted.
                /// </summary>
                public IObservableCollection<T> Items => _items;

                /// <summary>
                /// Called when activating.
                /// </summary>
                protected override Task OnActivateAsync(CancellationToken cancellationToken)
                {
                    return Task.WhenAll(_items.OfType<IActivate>().Select(x => x.ActivateAsync(cancellationToken)));
                }

                /// <summary>
                /// Called when deactivating.
                /// </summary>
                /// <param name="close">Indicates whether this instance will be closed.</param>
                /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
                /// <returns>A task that represents the asynchronous operation.</returns>
                protected override async Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
                {
                    foreach(var deactivate in _items.OfType<IDeactivate>())
                    {
                        await deactivate.DeactivateAsync(close, cancellationToken);
                    }

                    if (close)
                        _items.Clear();
                }

                /// <summary>
                /// Called to check whether or not this instance can close.
                /// </summary>
                /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
                /// <returns>A task that represents the asynchronous operation.</returns>
                public override async Task<bool> CanCloseAsync(CancellationToken cancellationToken = default)
                {
                    var closeResult = await CloseStrategy.ExecuteAsync(_items.ToList(), cancellationToken);

                    if (!closeResult.CloseCanOccur && closeResult.Children.Any())
                    {
                        foreach (var deactivate in closeResult.Children.OfType<IDeactivate>())
                        {
                            await deactivate.DeactivateAsync(true, cancellationToken);
                        }

                        _items.RemoveRange(closeResult.Children);
                    }

                    return closeResult.CloseCanOccur;
                }

                /// <summary>
                /// Called when initializing.
                /// </summary>
                protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
                {
                    if (_openPublicItems)
                        await Task.WhenAll(GetType().GetRuntimeProperties()
                            .Where(x => x.Name != "Parent" && typeof(T).GetTypeInfo().IsAssignableFrom(x.PropertyType.GetTypeInfo()))
                            .Select(x => x.GetValue(this, null))
                            .Cast<T>()
                            .Select(i => ActivateItemAsync(i, cancellationToken)));
                }

                /// <summary>
                /// Activates the specified item.
                /// </summary>
                /// <param name="item">The item to activate.</param>
                /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
                /// <returns>A task that represents the asynchronous operation.</returns>
                public override async Task ActivateItemAsync(T item, CancellationToken cancellationToken = default)
                {
                    if (item == null)
                        return;

                    item = EnsureItem(item);

                    if (IsActive)
                        await ScreenExtensions.TryActivateAsync(item, cancellationToken);

                    OnActivationProcessed(item, true);
                }

                /// <summary>
                /// Deactivates the specified item.
                /// </summary>
                /// <param name="item">The item to close.</param>
                /// <param name="close">Indicates whether or not to close the item after deactivating it.</param>
                /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
                /// <returns>A task that represents the asynchronous operation.</returns>
                public override async Task DeactivateItemAsync(T item, bool close, CancellationToken cancellationToken = default)
                {
                    if (item == null)
                        return;

                    if (close)
                    {
                        var closeResult = await CloseStrategy.ExecuteAsync(new[] { item }, CancellationToken.None);

                        if (closeResult.CloseCanOccur)
                            await CloseItemCoreAsync(item, cancellationToken);
                    }
                    else
                        await ScreenExtensions.TryDeactivateAsync(item, false, cancellationToken);
                }

                /// <summary>
                /// Gets the children.
                /// </summary>
                /// <returns>The collection of children.</returns>
                public override IEnumerable<T> GetChildren()
                {
                    return _items;
                }

                private async Task CloseItemCoreAsync(T item, CancellationToken cancellationToken = default)
                {
                    await ScreenExtensions.TryDeactivateAsync(item, true, cancellationToken);
                    _items.Remove(item);
                }

                /// <summary>
                /// Ensures that an item is ready to be activated.
                /// </summary>
                /// <param name="newItem">The item that is about to be activated.</param>
                /// <returns>The item to be activated.</returns>
                protected override T EnsureItem(T newItem)
                {
                    var index = _items.IndexOf(newItem);

                    if (index == -1)
                        _items.Add(newItem);
                    else
                        newItem = _items[index];

                    return base.EnsureItem(newItem);
                }
            }
        }
    }
}
