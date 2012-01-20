namespace Caliburn.Micro {
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    ///   Interface used to define an object associated to a collection of children.
    /// </summary>
    public interface IParent {
        /// <summary>
        ///   Gets the children.
        /// </summary>
        /// <returns>
        ///   The collection of children.
        /// </returns>
        IEnumerable GetChildren();
    }

    /// <summary>
    /// Interface used to define a specialized parent.
    /// </summary>
    /// <typeparam name="T">The type of children.</typeparam>
#if NET
    public interface IParent<out T> : IParent 
#else
    public interface IParent<T> : IParent       
#endif
    {
        /// <summary>
        ///   Gets the children.
        /// </summary>
        /// <returns>
        ///   The collection of children.
        /// </returns>
        new IEnumerable<T> GetChildren();
    }

    /// <summary>
    /// Denotes an instance which maintains an active item.
    /// </summary>
    public interface IHaveActiveItem {
        /// <summary>
        /// The currently active item.
        /// </summary>
        object ActiveItem { get; set; }
    }

    /// <summary>
    /// Denotes an instance which conducts other objects by managing an ActiveItem and maintaining a strict lifecycle.
    /// </summary>
    /// <remarks>Conducted instances can optin to the lifecycle by impelenting any of the follosing <see cref="IActivate"/>, <see cref="IDeactivate"/>, <see cref="IGuardClose"/>.</remarks>
    public interface IConductor : IParent, INotifyPropertyChangedEx {
        /// <summary>
        /// Activates the specified item.
        /// </summary>
        /// <param name="item">The item to activate.</param>
        void ActivateItem(object item);

        /// <summary>
        /// Deactivates the specified item.
        /// </summary>
        /// <param name="item">The item to close.</param>
        /// <param name="close">Indicates whether or not to close the item after deactivating it.</param>
        void DeactivateItem(object item, bool close);

        /// <summary>
        /// Occurs when an activation request is processed.
        /// </summary>
        event EventHandler<ActivationProcessedEventArgs> ActivationProcessed;
    }

    /// <summary>
    /// An <see cref="IConductor"/> that also implements <see cref="IHaveActiveItem"/>.
    /// </summary>
    public interface IConductActiveItem : IConductor, IHaveActiveItem { }
}