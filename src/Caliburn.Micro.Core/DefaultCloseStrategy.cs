namespace Caliburn.Micro {
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Used to gather the results from multiple child elements which may or may not prevent closing.
    /// </summary>
    /// <typeparam name="T">The type of child element.</typeparam>
    public class DefaultCloseStrategy<T> : ICloseStrategy<T> {
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
            using (var enumerator = toClose.GetEnumerator()) {
                Evaluate(new EvaluationState(), enumerator, callback);
            }
        }

        void Evaluate(EvaluationState state, IEnumerator<T> enumerator, Action<bool, IEnumerable<T>> callback) {
            var guardPending = false;
            do {
                if (!enumerator.MoveNext()) {
                    callback(state.FinalResult, closeConductedItemsWhenConductorCannotClose ? state.Closable : new List<T>());
                    break;
                }

                var current = enumerator.Current;
                var guard = current as IGuardClose;
                if (guard != null) {
                    guardPending = true;
                    guard.CanClose(canClose => {
                        guardPending = false;
                        if (canClose) {
                            state.Closable.Add(current);
                        }

                        state.FinalResult = state.FinalResult && canClose;

                        if (state.GuardMustCallEvaluate) {
                            state.GuardMustCallEvaluate = false;
                            Evaluate(state, enumerator, callback);
                        }
                    });
                    state.GuardMustCallEvaluate = state.GuardMustCallEvaluate || guardPending;
                } else {
                    state.Closable.Add(current);
                }
            } while (!guardPending);
        }

        class EvaluationState {
            public readonly List<T> Closable = new List<T>();
            public bool FinalResult = true;
            public bool GuardMustCallEvaluate;
        }
    }
}
