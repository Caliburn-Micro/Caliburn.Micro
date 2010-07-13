namespace Caliburn.Micro
{
    using System;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
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

        Func<bool> guard;
        MethodInfo execute;
        object target;
        DependencyObject view;

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
        public string MethodName
        {
            get { return (string)GetValue(MethodNameProperty); }
            set { SetValue(MethodNameProperty, value); }
        }

        /// <summary>
        /// Gets the parameters to pass as part of the method invocation.
        /// </summary>
        /// <value>The parameters.</value>
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
            AssociatedObject.Loaded += ElementLoaded;
            Parameters.Attach(AssociatedObject);
            Parameters.Apply(x => x.MakeAwareOf(this));
            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            Detaching(this, EventArgs.Empty);
            AssociatedObject.Loaded -= ElementLoaded;
            Parameters.Detach();
            base.OnDetaching();
        }

        void ElementLoaded(object sender, RoutedEventArgs e)
        {
            var found = GetMethodBinding();
            if(found.Item1 == null || found.Item2 == null)
            {
                var ex = new Exception(string.Format("No target found for method {0}.", MethodName));
                Log.Error(ex);
                throw ex;
            }

            target = found.Item1;
            execute = found.Item2;
            view = found.Item3;
            guard = ConventionManager.CreateActionGuard(this, AssociatedObject, target);

            UpdateAvailability();
        }

        protected override void Invoke(object eventArgs)
        {
            Log.Info("Executing {0}.", this);

            var values = MessageBinder.DetermineParameters(this, execute.GetParameters(), AssociatedObject, eventArgs);
            var outcome = execute.Invoke(target, values);

            var result = MessageBinder.CreateResult(outcome);
            if (result == null)
                return;

            result.Execute(new ResultExecutionContext {
                Source = AssociatedObject,
                Message = this,
                Target = target,
                View = view
            });
        }

        /// <summary>
        /// Forces an update of the UI's Enabled/Disabled state based on the the preconditions associated with the method.
        /// </summary>
        public void UpdateAvailability()
        {
            if (guard == null)
                return;

#if SILVERLIGHT
            if (!(AssociatedObject is Control))
                return;
#endif
            Log.Info("{0} availability changed.", this);

#if SILVERLIGHT
            ((Control)AssociatedObject).IsEnabled = guard();
#else
            AssociatedObject.IsEnabled = guard();
#endif
        }

        Tuple<object, MethodInfo, DependencyObject> GetMethodBinding()
        {
            DependencyObject currentElement = AssociatedObject;
            MethodInfo actionMethod = null;
            object currentTarget = null;

            while(currentElement != null && actionMethod == null)
            {
                currentTarget = currentElement.GetValue(Message.HandlerProperty);

                if(currentTarget != null)
                    actionMethod = currentTarget.GetType().GetMethod(MethodName);

                if(actionMethod == null)
                    currentElement = VisualTreeHelper.GetParent(currentElement);
            }

            if(actionMethod == null && AssociatedObject.DataContext != null)
            {
                currentTarget = AssociatedObject.DataContext;
                actionMethod = AssociatedObject.DataContext.GetType().GetMethod(MethodName);
                currentElement = AssociatedObject;
            }

            return new Tuple<object, MethodInfo, DependencyObject>(currentTarget, actionMethod, currentElement);
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
    }
}