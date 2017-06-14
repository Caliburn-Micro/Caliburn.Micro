namespace Caliburn.Micro {
    using System;

    /// <summary>
    /// Denotes an instance which requires activation.
    /// </summary>
    public interface IActivate {
        ///<summary>
        /// Indicates whether or not this instance is active.
        ///</summary>
        bool IsActive { get; }

        /// <summary>
        /// Activates this instance.
        /// </summary>
        void Activate();

        /// <summary>
        /// Raised after activation occurs.
        /// </summary>
        event EventHandler<ActivationEventArgs> Activated;
    }
}
