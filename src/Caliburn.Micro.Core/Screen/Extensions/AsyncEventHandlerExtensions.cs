using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caliburn.Micro;

/// <summary>
/// Async EventHandler Extensions.
/// </summary>
public static class AsyncEventHandlerExtensions {
    /// <summary>
    /// Get Invocation List of AsyncEventHandler.
    /// </summary>
    /// <typeparam name="TEventArgs">The Event args type.</typeparam>
    /// <param name="handler">Async EventHandler.</param>
    /// <returns>List of AsyncEventHandler.</returns>
    public static IEnumerable<AsyncEventHandler<TEventArgs>> GetHandlers<TEventArgs>(this AsyncEventHandler<TEventArgs> handler)
        where TEventArgs : EventArgs
        => handler.GetInvocationList().Cast<AsyncEventHandler<TEventArgs>>();

    /// <summary>
    /// Invoke all handlers of AsyncEventHandler.
    /// </summary>
    /// <typeparam name="TEventArgs">The Event args type.</typeparam>
    /// <param name="handler">Async EventHandler.</param>
    /// <param name="sender">The event source.</param>
    /// <param name="e">The Event args.</param>
    /// <returns>Task.</returns>
    public static Task InvokeAllAsync<TEventArgs>(this AsyncEventHandler<TEventArgs> handler, object sender, TEventArgs e)
        where TEventArgs : EventArgs
        => Task.WhenAll(
            handler.GetHandlers()
                .Select(handleAsync => handleAsync(sender, e)));
}
