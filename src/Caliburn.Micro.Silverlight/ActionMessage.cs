namespace Caliburn.Micro
{
    using System;
    using System.ComponentModel;
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

        MethodInfo guard;
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

        protected override void OnAttached()
        {
            AssociatedObject.Loaded += ElementLoaded;
            Parameters.Attach(AssociatedObject);
            Parameters.Apply(x => x.MakeAwareOf(this));
            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Loaded -= ElementLoaded;

            var inpc = target as INotifyPropertyChanged;
            if(inpc != null)
                inpc.PropertyChanged -= CanExecuteChanged;

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

            var guardName = ConventionManager.DeriveGuardName(MethodName);
            var targetType = target.GetType();
            guard = targetType.GetMethod(guardName);

            if (guard == null)
            {
                var inpc = target as INotifyPropertyChanged;
                if(inpc == null)
                    return;

                guard = targetType.GetMethod("get_" + guardName);
#if SILVERLIGHT
                if(guard == null || !(AssociatedObject is Control))
                    return;
#else
                if (guard == null)
                    return;
#endif
                inpc.PropertyChanged += CanExecuteChanged;
            }

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

        void CanExecuteChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == guard.Name.Substring(4))
                UpdateAvailability();
        }

        /// <summary>
        /// Forces an update of the UI's Enabled/Disabled state based on the the preconditions associated with the method.
        /// </summary>
        public void UpdateAvailability()
        {
#if SILVERLIGHT
            if (guard == null || !(AssociatedObject is Control))
                return;
#else
            if (guard == null)
                return;
#endif

            Log.Info("{0} availability changed.", this);

            var result = (bool)guard.Invoke(
                target,
                MessageBinder.DetermineParameters(this, guard.GetParameters(), AssociatedObject, null)
                );

#if SILVERLIGHT
            ((Control)AssociatedObject).IsEnabled = result;
#else
            AssociatedObject.IsEnabled = result;
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