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
        bool guardMustCallEvaluate;
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
            guardMustCallEvaluate = false;

            Evaluate(true, toClose.GetEnumerator(), callback);
        }

        void Evaluate(bool result, IEnumerator<T> enumerator, Action<bool, IEnumerable<T>> callback) {
            finalResult = finalResult && result;

            var guardPending = false;
            do {
                if (!enumerator.MoveNext()) {
                    callback(finalResult, closeConductedItemsWhenConductorCannotClose ? closable : new List<T>());
                    closable = null;
                    break;
                }

                var current = enumerator.Current;
                var guard = current as IGuardClose;
                if (guard != null) {
                    guardPending = true;
                    guard.CanClose(canClose =>{
                        guardPending = false;
                        if (canClose) {
                            closable.Add(current);
                        }
                        if (guardMustCallEvaluate) {
                            guardMustCallEvaluate = false;
                            Evaluate(canClose, enumerator, callback);
                        } else {
                            finalResult = finalResult && canClose;  
                        }
                    });
                    guardMustCallEvaluate = guardMustCallEvaluate || guardPending;
                } else {
                    closable.Add(current);
                }
            } while (!guardPending);
        }
    }
}
