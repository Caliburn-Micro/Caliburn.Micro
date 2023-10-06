using System.Collections.Generic;

namespace Caliburn.Micro
{
    /// <summary>
    /// Results from the close strategy.
    /// </summary>
    public interface ICloseResult<T>
    {
        /// <summary>
        /// Indicates which children shbould close if the parent cannot.
        /// </summary>
        IEnumerable<T> Children { get; }

        /// <summary>
        /// Indicates whether a close can occur
        /// </summary>
        bool CloseCanOccur { get; }
    }
}
