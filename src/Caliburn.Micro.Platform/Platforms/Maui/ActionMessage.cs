using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

using Microsoft.Maui.Controls;

using FrameworkElement = Microsoft.Maui.Controls.VisualElement;
using UIElement = Microsoft.Maui.Controls.Element;

namespace Caliburn.Micro.Maui {
    /// <summary>
    /// Used to send a message from the UI to a presentation model class, indicating that a particular Action should be invoked.
    /// </summary>
    [ContentProperty("Parameters")]
    public class ActionMessage : TriggerActionBase<VisualElement>, IHaveParameters {
        private static readonly ILog Log = LogManager.GetLog(typeof(ActionMessage));
        private ActionExecutionContext context;
        private object handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionMessage"/> class.
        /// </summary>
        public ActionMessage() {
            Parameters = new AttachedCollection<Parameter>();
            Parameters.CollectionChanged +=
                (s, e)
                    => e.NewItems.OfType<Parameter>().Apply(x => x.MakeAwareOf(this));
        }

        /// <summary>
        /// Occurs before the message detaches from the associated object.
        /// </summary>
        public event EventHandler Detaching = (sender, e) => { };

        /// <summary>
        /// Gets or sets a value indicating whether causes the action invocation to "double check" if the action should be invoked by executing the guard immediately before hand.
        /// </summary>
        /// <remarks>This is disabled by default. If multiple actions are attached to the same element, you may want to enable this so that each individaul action checks its guard regardless of how the UI state appears.</remarks>
        public static bool EnforceGuardsDuringInvocation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether causes the action to throw if it cannot locate the target or the method at invocation time.
        /// </summary>
        /// <remarks>True by default.</remarks>
        public static bool ThrowsExceptions { get; set; }
            = true;

        /// <summary>
        /// Gets or Sets funct to returns the list of possible names of guard methods / properties for the given method.
        /// </summary>
        public static Func<MethodInfo, IEnumerable<string>> BuildPossibleGuardNames { get; set; }
            = method => {
                const string GuardPrefix = "Can";
                const string AsyncMethodSuffix = "Async";

                var guardNames = new List<string>();
                string methodName = method.Name;
                guardNames.Add(GuardPrefix + methodName);
                if (methodName.EndsWith(AsyncMethodSuffix, StringComparison.OrdinalIgnoreCase)) {
                    guardNames.Add(GuardPrefix + methodName[..^AsyncMethodSuffix.Length]);
                }

                return guardNames;
            };

        /// <summary>
        /// Gets or sets func to prepares the action execution context for use.
        /// </summary>
        public static Action<ActionExecutionContext> PrepareContext { get; set; }
            = context => {
                SetMethodBinding(context);
                if (context.Target == null || context.Method == null) {
                    return;
                }

                var possibleGuardNames = BuildPossibleGuardNames(context.Method).ToList();
                MethodInfo guard = TryFindGuardMethod(context, possibleGuardNames);
                if (guard != null) {
                    context.CanExecute = () => (bool)guard.Invoke(context.Target, MessageBinder.DetermineParameters(context, guard.GetParameters()));

                    return;
                }

                if (!(context.Target is INotifyPropertyChanged inpc)) {
                    return;
                }

                Type targetType = context.Target.GetType();
                string matchingGuardName = null;
                foreach (string possibleGuardName in possibleGuardNames) {
                    matchingGuardName = possibleGuardName;
                    guard = GetMethodInfo(targetType, "get_" + matchingGuardName);
                    if (guard != null) {
                        break;
                    }
                }

                if (guard == null) {
                    return;
                }

                void OnPropertyChanged(object s, PropertyChangedEventArgs e) {
                    if (string.IsNullOrEmpty(e.PropertyName) || e.PropertyName == matchingGuardName) {
                        Execute.OnUIThread(() => {
                            ActionMessage message = context.Message;
                            if (message == null) {
                                inpc.PropertyChanged -= OnPropertyChanged;
                                return;
                            }

                            message.UpdateAvailability();
                        });
                    }
                }

                inpc.PropertyChanged += OnPropertyChanged;
                context.Disposing += (sender, e) => inpc.PropertyChanged -= OnPropertyChanged;
                context.Message.Detaching += (sender, e) => inpc.PropertyChanged -= OnPropertyChanged;
                context.CanExecute = () => (bool)guard.Invoke(context.Target, MessageBinder.DetermineParameters(context, guard.GetParameters()));
            };

        /// <summary>
        /// Gets or sets action to invokes the action using the specified <see cref="ActionExecutionContext"/>.
        /// </summary>
        public static Action<ActionExecutionContext> InvokeAction { get; set; }
            = context => {
                object[] values = MessageBinder.DetermineParameters(context, context.Method.GetParameters());
                object returnValue = context.Method.Invoke(context.Target, values);

                if (returnValue is System.Threading.Tasks.Task task) {
                    returnValue = task.AsResult();
                }

                if (returnValue is IResult result) {
                    returnValue = new[] { result };
                }

                if (returnValue is IEnumerable<IResult> enumerable) {
                    returnValue = enumerable.GetEnumerator();
                }

                if (!(returnValue is IEnumerator<IResult> enumerator)) {
                    return;
                }

                Coroutine.BeginExecute(
                    enumerator,
                    new CoroutineExecutionContext {
                        Source = context.Source,
                        View = context.View,
                        Target = context.Target,
                    });
            };

        /// <summary>
        /// Gets or sets func to applies an availability effect, such as IsEnabled, to an element.
        /// </summary>
        /// <remarks>Returns a value indicating whether or not the action is available.</remarks>
        public static Func<ActionExecutionContext, bool> ApplyAvailabilityEffect { get; set; }
            = context => {
                FrameworkElement source = context.Source;

                if (source == null) {
                    return true;
                }

                bool hasBinding = ConventionManager.HasBinding(source, VisualElement.IsEnabledProperty);
                if (!hasBinding && context.CanExecute != null) {
                    source.IsEnabled = context.CanExecute();
                }

                return source.IsEnabled;
            };

        /// <summary>
        /// Gets or sets func to finds the method on the target matching the specified message.
        /// </summary>
        /// <returns>The matching method, if available.</returns>
        public static Func<ActionMessage, object, MethodInfo> GetTargetMethod { get; set; }
            = (message, target) => (from method in target.GetType().GetRuntimeMethods()
                                    where method.Name == message.MethodName
                                    let methodParameters = method.GetParameters()
                                    where message.Parameters.Count == methodParameters.Length
                                    select method).FirstOrDefault();

        /// <summary>
        /// Gets or sets Action to Sets the target, method and view on the context. Uses a bubbling strategy by default.
        /// </summary>
        public static Action<ActionExecutionContext> SetMethodBinding { get; set; }
            = context => {
                FrameworkElement source = context.Source;
                UIElement currentElement = source;
                while (currentElement != null) {
                    if (!Action.HasTargetSet(currentElement)) {
                        currentElement = currentElement.Parent;

                        continue;
                    }

                    object targetLocal = Message.GetHandler(currentElement);
                    if (targetLocal == null) {
                        context.View = currentElement;

                        return;
                    }

                    MethodInfo methodLocal = GetTargetMethod(context.Message, targetLocal);
                    if (methodLocal == null) {
                        currentElement = currentElement.Parent;
                        continue;
                    }

                    context.Method = methodLocal;
                    context.Target = targetLocal;
                    context.View = currentElement;

                    return;
                }

                if (source == null || source.BindingContext == null) {
                    return;
                }

                object target = source.BindingContext;
                MethodInfo method = GetTargetMethod(context.Message, target);
                if (method == null) {
                    return;
                }

                context.Target = target;
                context.Method = method;
                context.View = source;
            };

        /// <summary>
        /// Gets or sets the name of the method to be invoked on the presentation model class.
        /// </summary>
        /// <value>The name of the method.</value>
        public string MethodName { get; set; }

        /// <summary>
        /// Gets the parameters to pass as part of the method invocation.
        /// </summary>
        /// <value>The parameters.</value>
        public AttachedCollection<Parameter> Parameters { get; private set; }

        /// <summary>
        /// Gets the handler for the action.
        /// </summary>
        public object Handler {
            get => handler;
            private set {
                if (handler.Equals(value)) {
                    return;
                }

                handler = value;
                UpdateContext();
            }
        }

        /// <summary>
        /// Forces an update of the UI's Enabled/Disabled state based on the the preconditions associated with the method.
        /// </summary>
        public virtual void UpdateAvailability() {
            if (context == null) {
                return;
            }

            if (context.Target == null || context.View == null) {
                PrepareContext(context);
            }

            UpdateAvailabilityCore();
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the current <see cref="object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> that represents the current <see cref="object"/>.
        /// </returns>
        public override string ToString()
            => "Action: " + MethodName;

        /// <summary>
        ///  Called after the action is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached() {
            if (View.InDesignMode) {
                base.OnAttached();

                return;
            }

            Parameters.Attach(AssociatedObject);
            Parameters.OfType<Parameter>().Apply(x => x.MakeAwareOf(this));

            // This is a real hack, we don't have access to a Loaded event so
            // working out when the "visual tree" is active doesn't really happen
            // We don't have many events to choose from. Thankfully
            void OnBindingContextChanged(object s, EventArgs e) {
                AssociatedObject.BindingContextChanged -= OnBindingContextChanged;
                ElementLoaded();
            }

            AssociatedObject.BindingContextChanged += OnBindingContextChanged;

            base.OnAttached();
        }

        /// <summary>
        /// Called when the action is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected override void OnDetaching() {
            if (!View.InDesignMode) {
                Detaching(this, EventArgs.Empty);
                Parameters.Detach();
            }

            base.OnDetaching();
        }

        /// <summary>
        /// Invokes the action.
        /// </summary>
        /// <param name="sender">The Visual Element invoking the event.</param>
        protected override void Invoke(FrameworkElement sender) {
            Log.Info("Invoking {0}.", this);

            AssociatedObject ??= sender;

            if (context == null) {
                UpdateContext();
            }

            if (context.Target == null || context.View == null) {
                PrepareContext(context);
                if (context.Target == null) {
                    var ex = new Exception(string.Format(CultureInfo.InvariantCulture, "No target found for method {0}.", context.Message.MethodName));
                    Log.Error(ex);

                    if (!ThrowsExceptions) {
                        return;
                    }

                    throw ex;
                }

                if (!UpdateAvailabilityCore()) {
                    return;
                }
            }

            if (context.Method == null) {
                var ex = new Exception(string.Format(CultureInfo.InvariantCulture, "Method {0} not found on target of type {1}.", context.Message.MethodName, context.Target.GetType()));
                Log.Error(ex);

                if (!ThrowsExceptions) {
                    return;
                }

                throw ex;
            }

            context.EventArgs = null; // Unfortunately we can't know it :-(

            if (EnforceGuardsDuringInvocation && context.CanExecute != null && !context.CanExecute()) {
                return;
            }

            InvokeAction(context);
            context.EventArgs = null;
        }

        private static MethodInfo GetMethodInfo(Type t, string methodName)
            => t.GetRuntimeMethods().SingleOrDefault(m => m.Name == methodName);

        /// <summary>
        /// Try to find a candidate for guard function, having:
        ///         - a name in the form "CanXXX"
        ///         - no generic parameters
        ///         - a bool return type
        ///         - no parameters or a set of parameters corresponding to the action method.
        /// </summary>
        /// <param name="context">The execution context.</param>
        /// <param name="possibleGuardNames">Method names to look for.</param>
        /// <returns>A MethodInfo, if found; null otherwise.</returns>
        private static MethodInfo TryFindGuardMethod(ActionExecutionContext context, IEnumerable<string> possibleGuardNames) {
            Type targetType = context.Target.GetType();
            MethodInfo guard = null;
            foreach (string possibleGuardName in possibleGuardNames) {
                guard = GetMethodInfo(targetType, possibleGuardName);
                if (guard != null) {
                    break;
                }
            }

            if (guard == null) {
                return null;
            }

            if (guard.ContainsGenericParameters) {
                return null;
            }

            if (!typeof(bool).Equals(guard.ReturnType)) {
                return null;
            }

            ParameterInfo[] guardPars = guard.GetParameters();
            ParameterInfo[] actionPars = context.Method.GetParameters();
            if (guardPars.Length == 0) {
                return guard;
            }

            if (guardPars.Length != actionPars.Length) {
                return null;
            }

            IEnumerable<bool> comparisons = guardPars.Zip(
                context.Method.GetParameters(),
                (x, y) => x.ParameterType == y.ParameterType);

            return comparisons.Any(x => !x) ? null : guard;
        }

        private void ElementLoaded() {
            UpdateContext();

            UIElement currentElement;

            if (context.View == null) {
                currentElement = AssociatedObject;

                while (currentElement != null) {
                    if (Action.HasTargetSet(currentElement)) {
                        break;
                    }

                    currentElement = currentElement.Parent;
                }
            } else {
                currentElement = context.View as FrameworkElement;
            }

            Handler = currentElement;
        }

        private void UpdateContext() {
            context?.Dispose();

            context = new ActionExecutionContext {
                Message = this,
                Source = AssociatedObject,
            };

            PrepareContext(context);
            UpdateAvailabilityCore();
        }

        private bool UpdateAvailabilityCore() {
            Log.Info("{0} availability update.", this);
            return ApplyAvailabilityEffect(context);
        }
    }
}
