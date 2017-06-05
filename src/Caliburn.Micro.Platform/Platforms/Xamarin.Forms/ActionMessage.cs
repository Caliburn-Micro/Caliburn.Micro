namespace Caliburn.Micro.Xamarin.Forms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using global::Xamarin.Forms;
    using UIElement = global::Xamarin.Forms.Element;
    using FrameworkElement = global::Xamarin.Forms.VisualElement;
    using DependencyProperty = global::Xamarin.Forms.BindableProperty;
    using DependencyObject = global::Xamarin.Forms.BindableObject;

    /// <summary>
    /// Used to send a message from the UI to a presentation model class, indicating that a particular Action should be invoked.
    /// </summary>
    [ContentProperty("Parameters")]
    public class ActionMessage : TriggerActionBase<VisualElement>, IHaveParameters
    {
        private static readonly ILog Log = LogManager.GetLog(typeof(ActionMessage));
        private ActionExecutionContext context;
        private object handler;

        ///<summary>
        /// Causes the action invocation to "double check" if the action should be invoked by executing the guard immediately before hand.
        ///</summary>
        /// <remarks>This is disabled by default. If multiple actions are attached to the same element, you may want to enable this so that each individaul action checks its guard regardless of how the UI state appears.</remarks>
        public static bool EnforceGuardsDuringInvocation = false;

        ///<summary>
        /// Causes the action to throw if it cannot locate the target or the method at invocation time.
        ///</summary>
        /// <remarks>True by default.</remarks>
        public static bool ThrowsExceptions = true;

        /// <summary>
        /// Creates an instance of <see cref="ActionMessage"/>.
        /// </summary>
        public ActionMessage()
        {
            Parameters = new AttachedCollection<Parameter>();
        }

        /// <summary>
        /// Gets or sets the name of the method to be invoked on the presentation model class.
        /// </summary>
        /// <value>The name of the method.</value>
        public string MethodName { get; set; }

        /// <summary>
        /// The handler for the action.
        /// </summary>
        public object Handler
        {
            get { return handler; }
            private set
            {
                if (handler == value)
                    return;

                handler = value;
                UpdateContext();
            }
        }

        /// <summary>
        /// Gets the parameters to pass as part of the method invocation.
        /// </summary>
        /// <value>The parameters.</value>
        public AttachedCollection<Parameter> Parameters { get; private set; }

        /// <summary>
        /// Occurs before the message detaches from the associated object.
        /// </summary>
        public event EventHandler Detaching = delegate { };

        /// <summary>
        ///  Called after the action is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached() {
            if (!View.InDesignMode) {
                Parameters.Attach(AssociatedObject);
                Parameters.OfType<Parameter>().Apply(x => x.MakeAwareOf(this));

                // This is a real hack, we don't have access to a Loaded event so
                // working out when the "visual tree" is active doesn't really happen
                // We don't have many events to choose from. Thankfully 

                EventHandler bindingContextChanged = null;

                bindingContextChanged = (s, e) => {
                    AssociatedObject.BindingContextChanged -= bindingContextChanged;
                    ElementLoaded();
                };

                AssociatedObject.BindingContextChanged += bindingContextChanged;

               
            }

            base.OnAttached();
        }

        /// <summary>
        /// Called when the action is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected override void OnDetaching()
        {
            if (!View.InDesignMode)
            {
                Detaching(this, EventArgs.Empty);
                Parameters.Detach();
            }

            base.OnDetaching();
        }

        private void ElementLoaded()
        {
            UpdateContext();

            FrameworkElement currentElement;

            if (context.View == null)
            {
                currentElement = AssociatedObject;

                while (currentElement != null)
                {
                    if (Action.HasTargetSet(currentElement))
                        break;

                    currentElement = currentElement.ParentView;
                }
            }
            else currentElement = context.View as FrameworkElement;

            Handler = currentElement;
        }

        void UpdateContext()
        {
            if (context != null)
                context.Dispose();

            context = new ActionExecutionContext
            {
                Message = this,
                Source = AssociatedObject
            };

            PrepareContext(context);
            UpdateAvailabilityCore();
        }

        /// <summary>
        /// Invokes the action.
        /// </summary>
        /// <param name="sender"></param>
        protected override void Invoke(VisualElement sender)
        {
            Log.Info("Invoking {0}.", this);

            if (AssociatedObject == null) {
                AssociatedObject = sender;
            }

            if (context == null)
            {
                UpdateContext();
            }

            if (context.Target == null || context.View == null)
            {
                PrepareContext(context);
                if (context.Target == null)
                {
                    var ex = new Exception(string.Format("No target found for method {0}.", context.Message.MethodName));
                    Log.Error(ex);

                    if (!ThrowsExceptions)
                        return;
                    throw ex;
                }

                if (!UpdateAvailabilityCore())
                {
                    return;
                }
            }

            if (context.Method == null)
            {
                var ex = new Exception(string.Format("Method {0} not found on target of type {1}.", context.Message.MethodName, context.Target.GetType()));
                Log.Error(ex);

                if (!ThrowsExceptions)
                    return;
                throw ex;
            }

            context.EventArgs = null; // Unfortunately we can't know it :-(

            if (EnforceGuardsDuringInvocation && context.CanExecute != null && !context.CanExecute())
            {
                return;
            }

            InvokeAction(context);
            context.EventArgs = null;
        }

        /// <summary>
        /// Forces an update of the UI's Enabled/Disabled state based on the the preconditions associated with the method.
        /// </summary>
        public virtual void UpdateAvailability()
        {
            if (context == null)
                return;

            if (context.Target == null || context.View == null)
                PrepareContext(context);

            UpdateAvailabilityCore();
        }

        bool UpdateAvailabilityCore()
        {
            Log.Info("{0} availability update.", this);
            return ApplyAvailabilityEffect(context);
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return "Action: " + MethodName;
        }

        /// <summary>
        /// Invokes the action using the specified <see cref="ActionExecutionContext"/>
        /// </summary>
        public static Action<ActionExecutionContext> InvokeAction = context =>
        {
            var values = MessageBinder.DetermineParameters(context, context.Method.GetParameters());
            var returnValue = context.Method.Invoke(context.Target, values);

            var task = returnValue as System.Threading.Tasks.Task;
            if (task != null)
            {
                returnValue = task.AsResult();
            }

            var result = returnValue as IResult;
            if (result != null)
            {
                returnValue = new[] { result };
            }

            var enumerable = returnValue as IEnumerable<IResult>;
            if (enumerable != null)
            {
                returnValue = enumerable.GetEnumerator();
            }

            var enumerator = returnValue as IEnumerator<IResult>;
            if (enumerator != null)
            {
                Coroutine.BeginExecute(enumerator,
                    new CoroutineExecutionContext
                    {
                        Source = context.Source,
                        View = context.View,
                        Target = context.Target
                    });
            }
        };

        /// <summary>
        /// Applies an availability effect, such as IsEnabled, to an element.
        /// </summary>
        /// <remarks>Returns a value indicating whether or not the action is available.</remarks>
        public static Func<ActionExecutionContext, bool> ApplyAvailabilityEffect = context =>
        {
            var source = context.Source;

            if (source == null)
            {
                return true;
            }

            var hasBinding = ConventionManager.HasBinding(source, VisualElement.IsEnabledProperty);

            if (!hasBinding && context.CanExecute != null)
            {
                source.IsEnabled = context.CanExecute();
            }

            return source.IsEnabled;
        };

        /// <summary>
        /// Finds the method on the target matching the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="target">The target.</param>
        /// <returns>The matching method, if available.</returns>
        public static Func<ActionMessage, object, MethodInfo> GetTargetMethod = (message, target) =>
        {
            return (from method in target.GetType().GetRuntimeMethods()
                    where method.Name == message.MethodName
                    let methodParameters = method.GetParameters()
                    where message.Parameters.Count == methodParameters.Length
                    select method).FirstOrDefault();
        };

        /// <summary>
        /// Sets the target, method and view on the context. Uses a bubbling strategy by default.
        /// </summary>
        public static Action<ActionExecutionContext> SetMethodBinding = context =>
        {
            var source = context.Source;

            FrameworkElement currentElement = source;

            while (currentElement != null)
            {
                if (Action.HasTargetSet(currentElement))
                {
                    var target = Message.GetHandler(currentElement);
                    if (target != null)
                    {
                        var method = GetTargetMethod(context.Message, target);
                        if (method != null)
                        {
                            context.Method = method;
                            context.Target = target;
                            context.View = currentElement;
                            return;
                        }
                    }
                    else
                    {
                        context.View = currentElement;
                        return;
                    }
                }

                currentElement = currentElement.ParentView;
            }

            if (source != null && source.BindingContext != null)
            {
                var target = source.BindingContext;
                var method = GetTargetMethod(context.Message, target);

                if (method != null)
                {
                    context.Target = target;
                    context.Method = method;
                    context.View = source;
                }
            }
        };

        /// <summary>
        /// Prepares the action execution context for use.
        /// </summary>
        public static Action<ActionExecutionContext> PrepareContext = context =>
        {
            SetMethodBinding(context);
            if (context.Target == null || context.Method == null) {
                return;
            }

            var possibleGuardNames = BuildPossibleGuardNames(context.Method).ToList();

            var guard = TryFindGuardMethod(context, possibleGuardNames);

            if (guard == null)
            {
                var inpc = context.Target as INotifyPropertyChanged;
                if (inpc == null)
                    return;

                var targetType = context.Target.GetType();
                string matchingGuardName = null;
                foreach (string possibleGuardName in possibleGuardNames)
                {
                    matchingGuardName = possibleGuardName;
                    guard = GetMethodInfo(targetType, "get_" + matchingGuardName);
                    if (guard != null) break;
                }

                if (guard == null)
                    return;

                PropertyChangedEventHandler handler = null;
                handler = (s, e) => {
                    if (string.IsNullOrEmpty(e.PropertyName) || e.PropertyName == matchingGuardName)
                    {
                        Caliburn.Micro.Execute.OnUIThread(() => {
                            var message = context.Message;
                            if (message == null)
                            {
                                inpc.PropertyChanged -= handler;
                                return;
                            }
                            message.UpdateAvailability();
                        });
                    }
                };

                inpc.PropertyChanged += handler;
                context.Disposing += delegate { inpc.PropertyChanged -= handler; };
                context.Message.Detaching += delegate { inpc.PropertyChanged -= handler; };
            }

            context.CanExecute = () => (bool)guard.Invoke(
                context.Target,
                MessageBinder.DetermineParameters(context, guard.GetParameters())
                );
        };

        ///  <summary>
        ///  Try to find a candidate for guard function, having:
        /// 		- a name in the form "CanXXX"
        /// 		- no generic parameters
        /// 		- a bool return type
        /// 		- no parameters or a set of parameters corresponding to the action method
        ///  </summary>
        ///  <param name="context">The execution context</param>
        /// <param name="possibleGuardNames">Method names to look for.</param>
        /// <returns>A MethodInfo, if found; null otherwise</returns>
        static MethodInfo TryFindGuardMethod(ActionExecutionContext context, IEnumerable<string> possibleGuardNames) {
            var targetType = context.Target.GetType();
            MethodInfo guard = null;
            foreach (string possibleGuardName in possibleGuardNames)
            {
                guard = GetMethodInfo(targetType, possibleGuardName);
                if (guard != null) break;
            }

            if (guard == null) return null;
            if (guard.ContainsGenericParameters) return null;
            if (!typeof(bool).Equals(guard.ReturnType)) return null;

            var guardPars = guard.GetParameters();
            var actionPars = context.Method.GetParameters();
            if (guardPars.Length == 0) return guard;
            if (guardPars.Length != actionPars.Length) return null;

            var comparisons = guardPars.Zip(
                context.Method.GetParameters(),
                (x, y) => x.ParameterType == y.ParameterType
                );

            if (comparisons.Any(x => !x))
            {
                return null;
            }

            return guard;
        }

        /// <summary>
        /// Returns the list of possible names of guard methods / properties for the given method.
        /// </summary>
        public static Func<MethodInfo, IEnumerable<string>> BuildPossibleGuardNames = method => {

            var guardNames = new List<string>();

            const string GuardPrefix = "Can";

            var methodName = method.Name;

            guardNames.Add(GuardPrefix + methodName);

            const string AsyncMethodSuffix = "Async";

            if (methodName.EndsWith(AsyncMethodSuffix, StringComparison.OrdinalIgnoreCase))
            {
                guardNames.Add(GuardPrefix + methodName.Substring(0, methodName.Length - AsyncMethodSuffix.Length));
            }

            return guardNames;
        };

        static MethodInfo GetMethodInfo(Type t, string methodName) {
            return t.GetRuntimeMethods().SingleOrDefault(m => m.Name == methodName);
        }
    }
}
