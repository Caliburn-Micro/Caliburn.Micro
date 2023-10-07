using System;

namespace Caliburn.Micro;

/// <summary>
/// EventArgs sent during activation.
/// </summary>
public class ActivationEventArgs : EventArgs {
    /// <summary>
    /// Gets or sets a value indicating whether the sender was initialized in addition to being activated.
    /// </summary>
    public bool WasInitialized { get; set; }
}
