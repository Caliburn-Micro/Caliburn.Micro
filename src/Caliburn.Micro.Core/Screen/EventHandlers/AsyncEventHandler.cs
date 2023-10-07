using System;
using System.Threading.Tasks;

namespace Caliburn.Micro;

/// <summary>
/// Async Event Handler.
/// </summary>
/// <typeparam name="TEventArgs">The Event Args type.</typeparam>
/// <param name="sender">Event source.</param>
/// <param name="e">Event argument.</param>
/// <returns>Task.</returns>
public delegate Task AsyncEventHandler<TEventArgs>(object sender, TEventArgs e)
    where TEventArgs : EventArgs;
