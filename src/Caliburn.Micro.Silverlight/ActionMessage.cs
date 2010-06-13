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

    [DefaultTrigger(typeof(FrameworkElement), typeof(System.Windows.Interactivity.EventTrigger), "MouseLeftButtonDown")]
    [DefaultTrigger(typeof(ButtonBase), typeof(System.Windows.Interactivity.EventTrigger), "Click")] 
    [ContentProperty("Parameters")]
    [TypeConstraint(typeof(FrameworkElement))]
    public class ActionMessage : TriggerAction<FrameworkElement>
    {
        static readonly  ILog Log = LogManager.GetLog(typeof(ActionMessage));

        public static readonly DependencyProperty MethodNameProperty =
            DependencyProperty.Register(
                "MethodName",
                typeof(string),
                typeof(ActionMessage),
                null
                );

        public static readonly DependencyProperty ParametersProperty = 
            DependencyProperty.Register(
            "Parameters",
            typeof(AttachedCollection<Parameter>),
            typeof(ActionMessage), 
            null
            );

        MethodInfo canExecute;
        MethodInfo execute;
        object target;
        DependencyObject view;

        public ActionMessage()
        {
            SetValue(ParametersProperty, new AttachedCollection<Parameter>());
        }

        public string MethodName
        {
            get { return (string)GetValue(MethodNameProperty); }
            set { SetValue(MethodNameProperty, value); }
        }

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

            var canName = "Can" + MethodName;
            var targetType = target.GetType();
            canExecute = targetType.GetMethod(canName);

            if (canExecute == null)
            {
                var inpc = target as INotifyPropertyChanged;
                if(inpc == null)
                    return;

                canExecute = targetType.GetMethod("get_" + canName);
#if SILVERLIGHT
                if(canExecute == null || !(AssociatedObject is Control))
                    return;
#else
                if (canExecute == null)
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

            result.Execute(new ResultExecutionContext(AssociatedObject, this, target, view));
        }

        void CanExecuteChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == canExecute.Name.Substring(4))
                UpdateAvailability();
        }

        public void UpdateAvailability()
        {
#if SILVERLIGHT
            if (canExecute == null || !(AssociatedObject is Control))
                return;
#else
            if (canExecute == null)
                return;
#endif

            Log.Info("Execution changed for {0}.", this);

            var result = (bool)canExecute.Invoke(
                target,
                MessageBinder.DetermineParameters(this, canExecute.GetParameters(), AssociatedObject, null)
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

        public override string ToString()
        {
            return execute != null ? execute.Name : base.ToString();
        }
    }
}