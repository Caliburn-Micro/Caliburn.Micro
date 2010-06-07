namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;

    public class CompositeCloseStrategy<T>
    {
        readonly Action<bool> callback;
        readonly IEnumerator<T> enumerator;
        bool finalResult = true;

        public CompositeCloseStrategy(IEnumerator<T> enumerator, Action<bool> callback)
        {
            this.callback = callback;
            this.enumerator = enumerator;
        }

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