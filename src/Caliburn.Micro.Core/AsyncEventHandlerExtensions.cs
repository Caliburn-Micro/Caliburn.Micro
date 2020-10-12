using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caliburn.Micro
{
    public static class AsyncEventHandlerExtensions
    {
        public static IEnumerable<AsyncEventHandler<TEventArgs>> GetHandlers<TEventArgs>(
            this AsyncEventHandler<TEventArgs> handler)
            where TEventArgs : EventArgs
            => handler.GetInvocationList().Cast<AsyncEventHandler<TEventArgs>>();

        public static Task InvokeAllAsync<TEventArgs>(
            this AsyncEventHandler<TEventArgs> handler,
            object sender,
            TEventArgs e)
            where TEventArgs : EventArgs
            => Task.WhenAll(
                handler.GetHandlers()
                    .Select(handleAsync => handleAsync(sender, e)));
    }
}
