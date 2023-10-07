using System.Collections.Generic;

namespace Caliburn.Micro;

/// <summary>
/// Results from the close strategy.
/// </summary>
public interface ICloseResult<T> {
    /// <summary>
    /// Gets list of children that should close if the parent cannot.
    /// </summary>
    IEnumerable<T> Children { get; }

    /// <summary>
    /// Gets a value indicating whether a close can occur.
    /// </summary>
    bool CloseCanOccur { get; }
}
