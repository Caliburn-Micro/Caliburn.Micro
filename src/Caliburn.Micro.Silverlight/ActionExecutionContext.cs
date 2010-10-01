namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Windows;

    /// <summary>
    /// The context used during the execution of an Action or its guard.
    /// </summary>
    public class ActionExecutionContext
    {
        Dictionary<string, object> values;
        WeakReference target;
        WeakReference view;

        /// <summary>
        /// The message being executed.
        /// </summary>
        public ActionMessage Message;

        /// <summary>
        /// The source from which the message originates.
        /// </summary>
        public FrameworkElement Source;

        /// <summary>
        /// Any event arguments associated with the action's invocation.
        /// </summary>
        public object EventArgs;

        /// <summary>
        /// The instance on which the action is invoked.
        /// </summary>
        public object Target {
            get { return target == null ? null : target.Target; }
            set { target = new WeakReference(value); }
        }

        /// <summary>
        /// The view associated with the target.
        /// </summary>
        public DependencyObject View {
            get { return view == null ? null : view.Target as DependencyObject; }
            set { view = new WeakReference(value); }
        }

        /// <summary>
        /// The actual method info to be invoked.
        /// </summary>
        public MethodInfo Method;

        /// <summary>
        /// Determines whether the action can execute.
        /// </summary>
        /// <remarks>Returns true if the action can execute, false otherwise.</remarks>
        public Func<bool> CanExecute;

        /// <summary>
        /// Gets or sets additional data needed to invoke the action.
        /// </summary>
        /// <param name="key">The data key.</param>
        /// <returns>Custom data associated with the context.</returns>
        public object this[string key]
        {
            get
            {
                if(values == null)
                    values = new Dictionary<string, object>();

                return values[key];
            }
            set
            {
                if(values == null)
                    values = new Dictionary<string, object>();

                values[key] = value;
            }
        }
    }
}