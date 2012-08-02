namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
#if WinRT
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;
    using Windows.UI.Interactivity;
    using Windows.UI.Xaml.Markup;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Controls.Primitives;
    using Windows.UI.Xaml.Controls;
    using TriggerBase = Windows.UI.Interactivity.TriggerBase;
    using EventTrigger = Windows.UI.Interactivity.EventTrigger;
    using TriggerAction = Windows.UI.Interactivity.TriggerAction;
#else
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Interactivity;
    using System.Windows.Markup;
    using System.Windows.Media;
    using EventTrigger = System.Windows.Interactivity.EventTrigger;
#endif

    /// <summary>
    /// Used to send a message from the UI to a presentation model class, indicating that a particular Action should be invoked.
    /// </summary>
#if WinRT
    [ContentProperty(Name = "Parameters")]
#else
    [ContentProperty("Parameters")]
#endif

    [DefaultTrigger(typeof(FrameworkElement), typeof(EventTrigger), "MouseLeftButtonDown")]
    [DefaultTrigger(typeof(ButtonBase), typeof(EventTrigger), "Click")]

    [TypeConstraint(typeof(FrameworkElement))]
    public class ActionMessage : TriggerAction<FrameworkElement>, IHaveParameters
    {
        static readonly ILog Log = LogManager.GetLog(typeof(ActionMessage));
        ActionExecutionContext context;

#if WP71
        internal AppBarButton buttonSource;
        internal AppBarMenuItem menuItemSource;
#endif

        internal static readonly DependencyProperty HandlerProperty = DependencyProperty.RegisterAttached(
            "Handler",
            typeof(object),
            typeof(ActionMessage),
            new PropertyMetadata(null, HandlerPropertyChanged)
            );

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
        /// Represents the method name of an action message.
        /// </summary>
        public static readonly DependencyProperty MethodNameProperty =
            DependencyProperty.Register(
                "MethodName",
                typeof(string),
                typeof(ActionMessage),
                null
                );

        /// <summary>
        /// Represents the parameters of an action message.
        /// </summary>
        public static readonly DependencyProperty ParametersProperty =
            DependencyProperty.Register(
            "Parameters",
            typeof(AttachedCollection<Parameter>),
            typeof(ActionMessage),
            null
            );

        /// <summary>
        /// Creates an instance of <see cref="ActionMessage"/>.
        /// </summary>
        public ActionMessage()
        {
            SetValue(ParametersProperty, new AttachedCollection<Parameter>());
        }

        /// <summary>
        /// Gets or sets the name of the method to be invoked on the presentation model class.
        /// </summary>
        /// <value>The name of the method.</value>
#if !WinRT
        [Category("Common Properties")]
#endif
        public string MethodName
        {
            get { return (string)GetValue(MethodNameProperty); }
            set { SetValue(MethodNameProperty, value); }
        }

        /// <summary>
        /// Gets the parameters to pass as part of the method invocation.
        /// </summary>
        /// <value>The parameters.</value>
#if WinRT
        public AttachedCollection<Parameter> Parameters
        {
            get
            {
                return (AttachedCollection<Parameter>)GetValue(ParametersProperty);
            }
        }
#else
        [Category("Common Properties")]
        public AttachedCollection<Parameter> Parameters {
            get { return (AttachedCollection<Parameter>)GetValue(ParametersProperty); }
        }
#endif

        /// <summary>
        /// Occurs before the message detaches from the associated object.
        /// </summary>
        public event EventHandler Detaching = delegate { };

        /// <summary>
        /// Called after the action is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached()
        {
            if (!Execute.InDesignMode)
            {
                Parameters.Attach(AssociatedObject);
                Parameters.Apply(x => x.MakeAwareOf(this));

                if (View.ExecuteOnLoad(AssociatedObject, ElementLoaded))
                {
                    var trigger = Interaction.GetTriggers(AssociatedObject)
                        .FirstOrDefault(t => t.Actions.Contains(this)) as EventTrigger;
                    if (trigger != null && trigger.EventName == "Loaded")
                        Invoke(new RoutedEventArgs());
                }
            }

            base.OnAttached();
        }

        static void HandlerPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ActionMessage)d).UpdateContext();
        }

        /// <summary>
        /// Called when the action is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected override void OnDetaching()
        {
            if (!Execute.InDesignMode)
            {
                Detaching(this, EventArgs.Empty);
                AssociatedObject.Loaded -= ElementLoaded;
                Parameters.Detach();
            }

            base.OnDetaching();
        }

        void ElementLoaded(object sender, RoutedEventArgs e)
        {
            UpdateContext();

            DependencyObject currentElement;
            if (context.View == null)
            {
                currentElement = AssociatedObject;
                while (currentElement != null)
                {
                    if (Action.HasTargetSet(currentElement))
                        break;

                    currentElement = VisualTreeHelper.GetParent(currentElement);
                }
            }
            else currentElement = context.View;

#if NET
            var binding = new Binding {
                Path = new PropertyPath(Message.HandlerProperty), 
                Source = currentElement
            };
#elif WinRT
            var binding = new Binding
            {
                Source = currentElement
            };
#else
            const string bindingText = "<Binding xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation\' xmlns:cal='clr-namespace:Caliburn.Micro;assembly=Caliburn.Micro' Path='(cal:Message.Handler)' />";

            var binding = (Binding)XamlReader.Load(bindingText);
            binding.Source = currentElement;
#endif
            BindingOperations.SetBinding(this, HandlerProperty, binding);

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
        /// <param name="eventArgs">The parameter to the action. If the action does not require a parameter, the parameter may be set to a null reference.</param>
        protected override void Invoke(object eventArgs)
        {
            Log.Info("Invoking {0}.", this);

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

            context.EventArgs = eventArgs;

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
        public void UpdateAvailability()
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

            var result = returnValue as IResult;
            if (result != null)
            {
                returnValue = new[] { result };
            }

            var enumerable = returnValue as IEnumerable<IResult>;
            if (enumerable != null)
            {
                Coroutine.BeginExecute(enumerable.GetEnumerator(), context);
            }
            else if (returnValue is IEnumerator<IResult>)
            {
                Coroutine.BeginExecute((IEnumerator<IResult>)returnValue, context);
            }
        };

        /// <summary>
        /// Applies an availability effect, such as IsEnabled, to an element.
        /// </summary>
        /// <remarks>Returns a value indicating whether or not the action is available.</remarks>
        public static Func<ActionExecutionContext, bool> ApplyAvailabilityEffect = context =>
        {
#if WP71
            if (context.Message.buttonSource != null) {
                if(context.CanExecute != null)
                    context.Message.buttonSource.IsEnabled = context.CanExecute();
                return context.Message.buttonSource.IsEnabled;
            }

            if (context.Message.menuItemSource != null) {
                if(context.CanExecute != null)
                    context.Message.menuItemSource.IsEnabled = context.CanExecute();
                return context.Message.menuItemSource.IsEnabled;
            }
#endif

#if SILVERLIGHT || WinRT
            if (!(context.Source is Control))
            {
                return true;
            }
#endif

#if SILVERLIGHT || WinRT
            var source = (Control)context.Source;
            if (ConventionManager.HasBinding(source, Control.IsEnabledProperty))
            {
                return source.IsEnabled;
            }
#else
            var source = context.Source;
            if (ConventionManager.HasBinding(source, UIElement.IsEnabledProperty)){
                return source.IsEnabled;
            }
#endif
            if (context.CanExecute != null)
            {
                source.IsEnabled = context.CanExecute();
            }

            return source.IsEnabled;
        };
#if WinRT
        /// <summary>
        /// Finds the method on the target matching the specified message.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="message">The message.</param>
        /// <returns>The matching method, if available.</returns>
        public static Func<ActionMessage, object, MethodInfo> GetTargetMethod = (message, target) =>
        {
            return (from method in target.GetType().GetRuntimeMethods()
                    where method.Name == message.MethodName
                    let methodParameters = method.GetParameters()
                    where message.Parameters.Count == methodParameters.Length
                    select method).FirstOrDefault();
        };
#else
        /// <summary>
        /// Finds the method on the target matching the specified message.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="message">The message.</param>
        /// <returns>The matching method, if available.</returns>
        public static Func<ActionMessage, object, MethodInfo> GetTargetMethod = (message, target) => {
            return (from method in target.GetType().GetMethods()
                    where method.Name == message.MethodName
                    let methodParameters = method.GetParameters()
                    where message.Parameters.Count == methodParameters.Length
                    select method).FirstOrDefault();
        };
#endif

        /// <summary>
        /// Sets the target, method and view on the context. Uses a bubbling strategy by default.
        /// </summary>
        public static Action<ActionExecutionContext> SetMethodBinding = context =>
        {
            DependencyObject currentElement = context.Source;

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

                currentElement = VisualTreeHelper.GetParent(currentElement);
            }

            if (context.Source.DataContext != null)
            {
                var target = context.Source.DataContext;
                var method = GetTargetMethod(context.Message, target);

                if (method != null)
                {
                    context.Target = target;
                    context.Method = method;
                    context.View = context.Source;
                }
            }
        };

        /// <summary>
        /// Prepares the action execution context for use.
        /// </summary>
        public static Action<ActionExecutionContext> PrepareContext = context =>
        {
            SetMethodBinding(context);
            if (context.Target == null || context.Method == null)
            {
                return;
            }

            var guardName = "Can" + context.Method.Name;
            var targetType = context.Target.GetType();
            var guard = TryFindGuardMethod(context);

            if (guard == null)
            {
                var inpc = context.Target as INotifyPropertyChanged;
                if (inpc == null)
                    return;
#if WinRT
                guard = targetType.GetRuntimeMethods().SingleOrDefault(m => m.Name == "get_" + guardName);
#else
                guard = targetType.GetMethod("get_" + guardName);
#endif
                if (guard == null)
                    return;

                PropertyChangedEventHandler handler = null;
                handler = (s, e) =>
                {
                    if (string.IsNullOrEmpty(e.PropertyName) || e.PropertyName == guardName)
                    {
                        if (context.Message == null)
                        {
                            inpc.PropertyChanged -= handler;
                            return;
                        }
                        context.Message.UpdateAvailability();
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
#if WinRT
        /// <summary>
        /// Try to find a candidate for guard function, having:
        ///		- a name in the form "CanXXX"
        ///		- no generic parameters
        ///		- a bool return type
        ///		- no parameters or a set of parameters corresponding to the action method
        /// </summary>
        /// <param name="context">The execution context</param>
        /// <returns>A MethodInfo, if found; null otherwise</returns>
        static MethodInfo TryFindGuardMethod(ActionExecutionContext context)
        {
            var guardName = "Can" + context.Method.Name;
            var targetType = context.Target.GetType();
            var guard = targetType.GetRuntimeMethods().SingleOrDefault(m => m.Name == guardName);

            if (guard == null) return null;
            if (guard.ContainsGenericParameters) return null;
            if (!typeof(bool).Equals(guard.ReturnType)) return null;

            var guardPars = guard.GetParameters();
            var actionPars = context.Method.GetParameters();
            if (guardPars.Length == 0) return guard;
            if (guardPars.Length != actionPars.Length) return null;

            var comparisons = guardPars.Zip(
                context.Method.GetParameters(),
                (x, y) => x.ParameterType.Equals(y.ParameterType)
                );

            if (comparisons.Any(x => !x))
            {
                return null;
            }

            return guard;
        }
#else
        /// <summary>
		/// Try to find a candidate for guard function, having:
		///		- a name in the form "CanXXX"
		///		- no generic parameters
		///		- a bool return type
		///		- no parameters or a set of parameters corresponding to the action method
		/// </summary>
		/// <param name="context">The execution context</param>
		/// <returns>A MethodInfo, if found; null otherwise</returns>
		static MethodInfo TryFindGuardMethod(ActionExecutionContext context) {
			var guardName = "Can" + context.Method.Name;
            var targetType = context.Target.GetType();
            var guard = targetType.GetMethod(guardName);

			if (guard ==null) return null;
			if (guard.ContainsGenericParameters) return null;
			if (!typeof(bool).Equals(guard.ReturnType)) return null;

			var guardPars = guard.GetParameters();
			var actionPars = context.Method.GetParameters();
			if (guardPars.Length == 0) return guard;
			if (guardPars.Length != actionPars.Length) return null;

		    var comparisons = guardPars.Zip(
		        context.Method.GetParameters(),
		        (x, y) => x.ParameterType.Equals(y.ParameterType)
		        );

			if (comparisons.Any(x => !x)) {
			    return null;
			}

			return guard;
		}
#endif
    }
}