namespace Caliburn.Micro {
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Used to gather the results from multiple child elements which may or may not prevent closing.
    /// </summary>
    /// <typeparam name="T">The type of child element.</typeparam>
    public interface ICloseStrategy<T> {
        /// <summary>
        /// Executes the strategy.
        /// </summary>
        /// <param name="toClose">Items that are requesting close.</param>
        /// <param name="callback">The action to call when all enumeration is complete and the close results are aggregated.
        /// The bool indicates whether close can occur. The enumerable indicates which children should close if the parent cannot.</param>
        void Execute(IEnumerable<T> toClose, Action<bool, IEnumerable<T>> callback);
    }
}
