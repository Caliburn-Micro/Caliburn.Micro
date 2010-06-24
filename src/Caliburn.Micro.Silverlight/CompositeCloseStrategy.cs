namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Used to gather the results from multiple child elements which may or may not prevent closing.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CompositeCloseStrategy<T>
    {
        readonly Action<bool> callback;
        readonly IEnumerator<T> enumerator;
        bool finalResult = true;

        /// <summary>
        /// Creates a new instance of <see cref="CompositeCloseStrategy{T}"/>
        /// </summary>
        /// <param name="enumerator">Enumerates the items that are requesting close.</param>
        /// <param name="callback">The action to call when all enumeration is complete and the close results are aggregated.</param>
        public CompositeCloseStrategy(IEnumerator<T> enumerator, Action<bool> callback)
        {
            this.callback = callback;
            this.enumerator = enumerator;
        }

        /// <summary>
        /// Executes the strategy.
        /// </summary>
        /// <param name="result">The result of the previous guard check.</param>
        public void Execute(bool result = true)
        {
            finalResult = finalResult && result;

            if(!enumerator.MoveNext())
                callback(finalResult);
            else
            {
                var guard = enumerator.Current as IGuardClose;
                if(guard != null)
                    guard.CanClose(Execute);
                else Execute();
            }
        }
    }
}