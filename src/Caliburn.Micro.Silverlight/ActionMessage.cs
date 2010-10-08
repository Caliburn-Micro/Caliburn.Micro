using System.Reflection;

namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
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
            new PropertyMetadata(HandlerPropertyChanged)
            );

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

#if WP7
        internal AppBarButton buttonSource;
        internal AppBarMenuItem menuItemSource;
#endif

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
#if !WP7
            DependencyObject currentElement;
            if(context.View == null) {
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
#else
            const string bindingText = "<Binding xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation\' xmlns:cal='clr-namespace:Caliburn.Micro;assembly=Caliburn.Micro' Path='(cal:Message.Handler)' />";

            var binding = (Binding)XamlReader.Load(bindingText);
            binding.Source = currentElement;
#endif

            BindingOperations.SetBinding(this, HandlerProperty, binding);
#endif
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
        public static Func<ActionExecutionContext, bool> ApplyAvailabilityEffect = context =>
        {
#if WP7
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

#if SILVERLIGHT
            if (!(context.Source is Control))
                return true;
#endif

#if SILVERLIGHT
            var source = (Control)context.Source;
#else
            var source = context.Source;
#endif
            if (context.CanExecute != null) 
                source.IsEnabled = context.CanExecute();
            return source.IsEnabled;
        };

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

        /// <summary>
        /// Sets the target, method and view on the context. Uses a bubbling strategy by default.
        /// </summary>
        public static Action<ActionExecutionContext> SetMethodBinding = context => {
            DependencyObject currentElement = context.Source;

            while(currentElement != null) {
                if (Action.HasTargetSet(currentElement)) {
                    var target = Message.GetHandler(currentElement);
                    if(target != null) {
                        var method = GetTargetMethod(context.Message, target);
                        if (method != null)
                        {
                            context.Method = method;
                            context.Target = target;
                            context.View = currentElement;
                            return;
                        }
                    }
                    else {
                        context.View = currentElement;
                        return;
                    }
                }

                currentElement = VisualTreeHelper.GetParent(currentElement);
            }

            if(context.Source.DataContext != null) {
                var target = context.Source.DataContext;
                var method = GetTargetMethod(context.Message, target);

                if(method != null) {
                    context.Target = target;
                    context.Method = method;
                    context.View = context.Source;
                }
            }
        };

        /// <summary>
        /// Prepares the action execution context for use.
        /// </summary>
        public static Action<ActionExecutionContext> PrepareContext = context =>{
            SetMethodBinding(context);
            if (context.Target == null || context.Method == null)
                return;

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