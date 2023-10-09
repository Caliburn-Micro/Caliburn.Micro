using System.Collections;
using System.Collections.Generic;

namespace Caliburn.Micro;

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
