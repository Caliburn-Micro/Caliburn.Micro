namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Interactivity;
    using System.Windows.Markup;
    using System.Windows.Media;

    /// <summary>
    /// Used to send a message from the UI to a presentation model class, indicating that a particular Action should be invoked.
    /// </summary>
    [DefaultTrigger(typeof(FrameworkElement), typeof(System.Windows.Interactivity.EventTrigger), "MouseLeftButtonDown")]
    [DefaultTrigger(typeof(ButtonBase), typeof(System.Windows.Interactivity.EventTrigger), "Click")] 
    [ContentProperty("Parameters")]
    [TypeConstraint(typeof(FrameworkElement))]
    public class ActionMessage : TriggerAction<FrameworkElement>
    {
        static readonly ILog Log = LogManager.GetLog(typeof(ActionMessage));

        internal static readonly DependencyProperty HandlerProperty = DependencyProperty.RegisterAttached(
                    "Handler",
                    typeof(object),
                    typeof(ActionMessage),
                    new PropertyMetadata(HandlerPropertyChanged));

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

        ActionExecutionContext context;

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
        [Category("Common Properties")]
        public string MethodName
        {
            get { return (string)GetValue(MethodNameProperty); }
            set { SetValue(MethodNameProperty, value); }
        }

        /// <summary>
        /// Gets the parameters to pass as part of the method invocation.
        /// </summary>
        /// <value>The parameters.</value>
        [Category("Common Properties")]
        public AttachedCollection<Parameter> Parameters
        {
            get { return (AttachedCollection<Parameter>)GetValue(ParametersProperty); }
        }

        /// <summary>
        /// Occurs before the message detaches from the associated object.
        /// </summary>
        public event EventHandler Detaching = delegate { };

        protected override void OnAttached()
        {
            if (!Bootstrapper.IsInDesignMode)
            {
                Parameters.Attach(AssociatedObject);
                Parameters.Apply(x => x.MakeAwareOf(this));

                if((bool)AssociatedObject.GetValue(View.IsLoadedProperty))
                    ElementLoaded(null, null);
                else AssociatedObject.Loaded += ElementLoaded;

                BindingOperations.SetBinding(this, HandlerProperty,
                    new Binding {Path = new PropertyPath(Message.HandlerProperty), Source = AssociatedObject});
            }

            base.OnAttached();
        }

        static void HandlerPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ActionMessage)d).UpdateContext();
        }

        protected override void OnDetaching()
        {
            if (!Bootstrapper.IsInDesignMode)
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
        }

        void UpdateContext()
        {
            context = new ActionExecutionContext {
                Message = this, 
                Source = AssociatedObject
            };

            PrepareContext(context);
            UpdateAvailabilityCore();
        }

        protected override void Invoke(object eventArgs) {
            Log.Info("Invoking {0}.", this);

            if(context.Target == null || context.View == null) {
                PrepareContext(context);
                if (context.Target == null)
                {
                    var ex = new Exception(string.Format("No target found for method {0}.", context.Message.MethodName));
                    Log.Error(ex);
                    throw ex;
                }
                if (!UpdateAvailabilityCore())
                    return;
            }

            if (context.Method == null)
            {
                var ex = new Exception(string.Format("Method {0} not found on target of type {1}.",
                    context.Message.MethodName, context.Target.GetType()));
                Log.Error(ex);
                throw ex;
            }

            context.EventArgs = eventArgs;
            InvokeAction(context);
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

        bool UpdateAvailabilityCore() {
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
        public static Action<ActionExecutionContext> InvokeAction = context =>{
            var values = MessageBinder.DetermineParameters(context, context.Method.GetParameters());
            var returnValue = context.Method.Invoke(context.Target, values);

            if (returnValue is IResult)
                returnValue = new[] { returnValue as IResult };

            if (returnValue is IEnumerable<IResult>)
                Coroutine.Execute(((IEnumerable<IResult>)returnValue).GetEnumerator(), context);
            else if (returnValue is IEnumerator<IResult>)
                Coroutine.Execute(((IEnumerator<IResult>)returnValue), context);
        };

        /// <summary>
        /// Applies an availability effect, such as IsEnabled, to an element.
        /// </summary>
        /// <remarks>Returns a value indicating whether or not the action is available.</remarks>
        public static Func<ActionExecutionContext, bool> ApplyAvailabilityEffect = context =>{
#if SILVERLIGHT
            if(!(context.Source is Control))
                return true;
#endif

#if SILVERLIGHT
            return ((Control)context.Source).IsEnabled = context.CanExecute();
#else
            return context.Source.IsEnabled = context.CanExecute();
#endif
        };

        /// <summary>
        /// Sets the target, method and view on the context. Uses a bubbling strategy by default.
        /// </summary>
        public static Action<ActionExecutionContext> SetMethodBinding = context => {
            DependencyObject currentElement = context.Source;
            MethodInfo actionMethod = null;
            object currentTarget = null;

            while(currentElement != null && actionMethod == null) {
                currentTarget = currentElement.GetValue(Message.HandlerProperty);

                if(currentTarget != null)
                    actionMethod = currentTarget.GetType().GetMethod(context.Message.MethodName);

                if(actionMethod == null)
                    currentElement = VisualTreeHelper.GetParent(currentElement);
            }

            if(actionMethod == null && context.Source.DataContext != null) {
                currentTarget = context.Source.DataContext;
                actionMethod = context.Source.DataContext.GetType().GetMethod(context.Message.MethodName);
                currentElement = context.Source;
            }

            context.Target = currentTarget;
            context.Method = actionMethod;
            context.View = currentElement;
        };

        /// <summary>
        /// Prepares the action execution context for use.
        /// </summary>
        public static Action<ActionExecutionContext> PrepareContext = context =>{
            SetMethodBinding(context);
            if (context.Target == null || context.Method == null)
            {
                return;
            }

            var guardName = "Can" + context.Method.Name;
            var targetType = context.Target.GetType();
            var guard = targetType.GetMethod(guardName);

            if(guard == null)
            {
                var inpc = context.Target as INotifyPropertyChanged;
                if(inpc == null)
                    return;

                guard = targetType.GetMethod("get_" + guardName);
                if(guard == null)
                    return;

                PropertyChangedEventHandler handler = (s, e) =>{
                    if(string.IsNullOrEmpty(e.PropertyName) || e.PropertyName == guardName)
                        context.Message.UpdateAvailability();
                };

                inpc.PropertyChanged += handler;
                context.Message.Detaching += delegate { inpc.PropertyChanged -= handler; };
            }

            context.CanExecute = () => (bool)guard.Invoke(
                context.Target,
                MessageBinder.DetermineParameters(context, guard.GetParameters())
                );
        };
    }
}