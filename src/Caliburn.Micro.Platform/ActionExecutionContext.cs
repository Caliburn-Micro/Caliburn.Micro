using System;
using System.Collections.Generic;
using System.Reflection;

#if WINDOWS_UWP
using Windows.UI.Xaml;
#elif XFORMS
using Xamarin.Forms;

using DependencyObject = Xamarin.Forms.BindableObject;
using DependencyProperty = Xamarin.Forms.BindableProperty;
using FrameworkElement = Xamarin.Forms.VisualElement;
#elif MAUI
using DependencyObject = Microsoft.Maui.Controls.BindableObject;
using FrameworkElement = Microsoft.Maui.Controls.VisualElement;
#else
using System.Windows;
#endif

#if XFORMS
namespace Caliburn.Micro.Xamarin.Forms
#elif MAUI
namespace Caliburn.Micro.Maui
#else
namespace Caliburn.Micro
#endif
{
    /// <summary>
    /// The context used during the execution of an Action or its guard.
    /// </summary>
    public class ActionExecutionContext : IDisposable {
        private WeakReference message;
        private WeakReference source;
        private WeakReference target;
        private WeakReference view;
        private Dictionary<string, object> values;
        private bool _isDisposed;

        /// <summary>
        /// Called when the execution context is disposed
        /// </summary>
        public event EventHandler Disposing = (sender, e) => { };

        /// <summary>
        /// Gets or sets func to determines whether the action can execute.
        /// </summary>
        /// <remarks>Returns true if the action can execute, false otherwise.</remarks>
        public Func<bool> CanExecute { get; set; }

        /// <summary>
        /// Gets or sets any event arguments associated with the action's invocation.
        /// </summary>
        public object EventArgs { get; set; }

        /// <summary>
        /// Gets or sets the actual method info to be invoked.
        /// </summary>
        public MethodInfo Method { get; set; }

        /// <summary>
        /// Gets or sets the message being executed.
        /// </summary>
        public ActionMessage Message {
            get => message == null ? null : message.Target as ActionMessage;
            set => message = new WeakReference(value);
        }

        /// <summary>
        /// Gets or sets the source from which the message originates.
        /// </summary>
        public FrameworkElement Source {
            get => source == null ? null : source.Target as FrameworkElement;
            set => source = new WeakReference(value);
        }

        /// <summary>
        /// Gets or sets the instance on which the action is invoked.
        /// </summary>
        public object Target {
            get => target?.Target;
            set => target = new WeakReference(value);
        }

        /// <summary>
        /// Gets or sets the view associated with the target.
        /// </summary>
        public DependencyObject View {
            get => view == null ? null : view.Target as DependencyObject;
            set => view = new WeakReference(value);
        }

        /// <summary>
        /// Gets or sets additional data needed to invoke the action.
        /// </summary>
        /// <param name="key">The data key.</param>
        /// <returns>Custom data associated with the context.</returns>
        public object this[string key] {
            get {
                if (values == null) {
                    values = new Dictionary<string, object>();
                }

                values.TryGetValue(key, out object result);

                return result;
            }

            set {
                if (values == null) {
                    values = new Dictionary<string, object>();
                }

                values[key] = value;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Perform Dispose.
        /// </summary>
        /// <param name="disposing">Dispose managed resources.</param>
        protected virtual void Dispose(bool disposing) {
            if (_isDisposed) {
                return;
            }

            if (disposing) {
                Disposing(this, System.EventArgs.Empty);
            }

            message = null;
            source = null;
            target = null;
            view = null;
            values = null;

            _isDisposed = true;
        }
    }
}
