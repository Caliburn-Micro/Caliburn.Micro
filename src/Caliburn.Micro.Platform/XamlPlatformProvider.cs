﻿
namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
#if WINDOWS_UWP
    using System.Reflection;
    using Windows.UI.Core;
    using Windows.UI.Xaml;
#elif AVALONIA
    using Avalonia;
    using Avalonia.Threading;
    using FrameworkElement = Avalonia.Controls.Control;
#elif WinUI3
    using System.Reflection;
    using Windows.UI.Core;
    using Microsoft.UI.Xaml;
    using Microsoft.UI.Dispatching;
#else
    using System.Windows;
    using System.Windows.Threading;
#endif

    /// <summary>
    /// A <see cref="IPlatformProvider"/> implementation for the XAML platfrom.
    /// </summary>
    public class XamlPlatformProvider : IPlatformProvider
    {
#if WINDOWS_UWP
        private readonly CoreDispatcher dispatcher;
#elif WinUI3
        private readonly DispatcherQueue dispatcher;
#else
        private readonly Dispatcher dispatcher;
#endif

        /// <summary>
        /// Initializes a new instance of the <see cref="XamlPlatformProvider"/> class.
        /// </summary>
        public XamlPlatformProvider()
        {
#if WINDOWS_UWP
            dispatcher = Window.Current.Dispatcher;
#elif AVALONIA
            dispatcher = Dispatcher.UIThread;
#elif WinUI3
            dispatcher = DispatcherQueue.GetForCurrentThread();
#else
            dispatcher = Dispatcher.CurrentDispatcher;
#endif
        }

        /// <summary>
        /// Whether or not classes should execute property change notications on the UI thread.
        /// </summary>
        public virtual bool PropertyChangeNotificationsOnUIThread => true;

        /// <summary>
        /// Indicates whether or not the framework is in design-time mode.
        /// </summary>
        public virtual bool InDesignMode
        {
            get { return View.InDesignMode; }
        }

        private void ValidateDispatcher()
        {
            if (dispatcher == null)
                throw new InvalidOperationException("Not initialized with dispatcher.");
        }

        private bool CheckAccess()
        {
#if WINDOWS_UWP
            return dispatcher == null || Window.Current != null;
#elif WinUI3
            return dispatcher == null || dispatcher.HasThreadAccess;
#else
            return dispatcher == null || dispatcher.CheckAccess();
#endif
        }

        /// <summary>
        /// Executes the action on the UI thread asynchronously.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        public virtual void BeginOnUIThread(System.Action action)
        {
            ValidateDispatcher();
#if WINDOWS_UWP 
            _ = dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => action());
#elif AVALONIA
            dispatcher.Post(action);
#elif WinUI3
            _ = dispatcher.TryEnqueue(() => action());
#else
            dispatcher.BeginInvoke(action);
#endif
        }

        /// <summary>
        /// Executes the action on the UI thread asynchronously.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <returns></returns>
        public virtual Task OnUIThreadAsync(Func<Task> action)
        {
            ValidateDispatcher();
#if WINDOWS_UWP
            return dispatcher.RunTaskAsync(action);
#elif AVALONIA
            return dispatcher.InvokeAsync(action);
#elif WinUI3
            return dispatcher.RunAsync(action);
#else
            return dispatcher.InvokeAsync(action).Task.Unwrap();
#endif
        }

        /// <summary>
        /// Executes the action on the UI thread.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual void OnUIThread(System.Action action)
        {
            if (CheckAccess())
                action();
            else
            {
#if WINDOWS_UWP 
                dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => action()).AsTask().Wait();
#elif WinUI3
                dispatcher.RunAsync(action).Wait();
#else
                Exception exception = null;
                System.Action method = () => {
                    try {
                        action();
                    }
                    catch(Exception ex) {
                        exception = ex;
                    }
                };

                dispatcher.Invoke(method);

                if (exception != null)
                throw new System.Reflection.TargetInvocationException("An error occurred while dispatching a call to the UI Thread", exception);
#endif
            }
        }

        /// <summary>
        /// Used to retrieve the root, non-framework-created view.
        /// </summary>
        /// <param name="view">The view to search.</param>
        /// <returns>
        /// The root element that was not created by the framework.
        /// </returns>
        /// <remarks>
        /// In certain instances the services create UI elements.
        /// For example, if you ask the window manager to show a UserControl as a dialog, it creates a window to host the UserControl in.
        /// The WindowManager marks that element as a framework-created element so that it can determine what it created vs. what was intended by the developer.
        /// Calling GetFirstNonGeneratedView allows the framework to discover what the original element was.
        /// </remarks>
        public virtual object GetFirstNonGeneratedView(object view)
        {
            return View.GetFirstNonGeneratedView(view);
        }

#if AVALONIA
        private static readonly AvaloniaProperty PreviouslyAttachedProperty = AvaloniaProperty.RegisterAttached<AvaloniaObject, bool>("PreviouslyAttached", typeof(XamlPlatformProvider));
#else
        private static readonly DependencyProperty PreviouslyAttachedProperty = DependencyProperty.RegisterAttached(
            "PreviouslyAttached",
            typeof(bool),
            typeof(XamlPlatformProvider),
            null
            );
#endif

        /// <summary>
        /// Executes the handler the fist time the view is loaded.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="handler">The handler.</param>
        public virtual void ExecuteOnFirstLoad(object view, Action<object> handler)
        {
            var element = view as FrameworkElement;
            if (element != null && !(bool)element.GetValue(PreviouslyAttachedProperty))
            {
                element.SetValue(PreviouslyAttachedProperty, true);
                View.ExecuteOnLoad(element, (s, e) => handler(s));
            }
        }

        /// <summary>
        /// Executes the handler the next time the view's LayoutUpdated event fires.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="handler">The handler.</param>
        public virtual void ExecuteOnLayoutUpdated(object view, Action<object> handler)
        {
            var element = view as FrameworkElement;
            if (element != null)
            {
                View.ExecuteOnLayoutUpdated(element, (s, e) => handler(s));
            }
        }

        /// <summary>
        /// Get the close action for the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model to close.</param>
        /// <param name="views">The associated views.</param>
        /// <param name="dialogResult">The dialog result.</param>
        /// <returns>
        /// An <see cref="Action" /> to close the view model.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual Func<CancellationToken, Task> GetViewCloseAction(object viewModel, ICollection<object> views, bool? dialogResult)
        {
            foreach (var contextualView in views)
            {
                var viewType = contextualView.GetType();
#if WINDOWS_UWP
                var closeMethod = viewType.GetRuntimeMethod("Close", new Type[0]);
#elif AVALONIA
                var closeMethod = dialogResult != null ? viewType.GetMethod("Close", new Type[] { typeof(object) }) : viewType.GetMethod("Close", new Type[0]);
#else
                var closeMethod = viewType.GetMethod("Close", new Type[0]);
#endif
                if (closeMethod != null)
                    return ct =>
                    {
#if AVALONIA
                        if (dialogResult != null)
                        {
                            closeMethod.Invoke(contextualView, new object[] { dialogResult });
                        }
                        else
                        {
                            closeMethod.Invoke(contextualView, null);
                        }
#elif !WINDOWS_UWP
                        var isClosed = false;
                        if (dialogResult != null)
                        {
                            var resultProperty = contextualView.GetType().GetProperty("DialogResult");
                            if (resultProperty != null)
                            {
                                resultProperty.SetValue(contextualView, dialogResult, null);
                                isClosed = true;
                            }
                        }

                        if (!isClosed)
                        {
                            closeMethod.Invoke(contextualView, null);
                        }
#else
                        closeMethod.Invoke(contextualView, null);
#endif
                        return Task.FromResult(true);
                    };

#if WINDOWS_UWP
                var isOpenProperty = viewType.GetRuntimeProperty("IsOpen");
#else
                var isOpenProperty = viewType.GetProperty("IsOpen");
#endif
                if (isOpenProperty != null)
                {
                    return ct =>
                    {
                        isOpenProperty.SetValue(contextualView, false, null);

                        return Task.FromResult(true);
                    };
                }
            }

            return ct =>
            {
                LogManager.GetLog(typeof(Screen)).Info("TryClose requires a parent IConductor or a view with a Close method or IsOpen property.");
                return Task.FromResult(true);
            };
        }
    }
}
