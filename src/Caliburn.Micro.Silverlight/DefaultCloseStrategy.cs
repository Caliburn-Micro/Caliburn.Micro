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

    /// <summary>
    /// Used to gather the results from multiple child elements which may or may not prevent closing.
    /// </summary>
    /// <typeparam name="T">The type of child element.</typeparam>
    public class DefaultCloseStrategy<T> : ICloseStrategy<T> {
        List<T> closable;
        bool finalResult;
        readonly bool closeConductedItemsWhenConductorCannotClose;

        /// <summary>
        /// Creates an instance of the class.
        /// </summary>
        /// <param name="closeConductedItemsWhenConductorCannotClose">Indicates that even if all conducted items are not closable, those that are should be closed. The default is FALSE.</param>
        public DefaultCloseStrategy(bool closeConductedItemsWhenConductorCannotClose = false) {
            this.closeConductedItemsWhenConductorCannotClose = closeConductedItemsWhenConductorCannotClose;
        }

        /// <summary>
        /// Executes the strategy.
        /// </summary>
        /// <param name="toClose">Items that are requesting close.</param>
        /// <param name="callback">The action to call when all enumeration is complete and the close results are aggregated.
        /// The bool indicates whether close can occur. The enumerable indicates which children should close if the parent cannot.</param>
        public void Execute(IEnumerable<T> toClose, Action<bool, IEnumerable<T>> callback) {
            finalResult = true;
            closable = new List<T>();

            Evaluate(true, toClose.GetEnumerator(), callback);
        }

        void Evaluate(bool result, IEnumerator<T> enumerator, Action<bool, IEnumerable<T>> callback) {
            finalResult = finalResult && result;

            if (!enumerator.MoveNext()) {
                callback(finalResult, closeConductedItemsWhenConductorCannotClose ? closable : new List<T>());
                closable = null;
            }
            else {
                var current = enumerator.Current;
                var guard = current as IGuardClose;
                if (guard != null) {
                    guard.CanClose(canClose =>{
                        if (canClose) {
                            closable.Add(current);
                        }

                        Evaluate(canClose, enumerator, callback);
                    });
                } else {
                    closable.Add(current);
                    Evaluate(true, enumerator, callback);
                }
            }
        }
    }
}