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

        PropertyInfo canExecute;
        MethodInfo execute;
        object target;
        DependencyObject view;

        public ActionMessage()
        {
            var parameters = new AttachedCollection<Parameter>();
            SetValue(ParametersProperty, parameters);
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

            if (string.IsNullOrEmpty(MethodName))
                MethodName = execute.Name;

            var inpc = target as INotifyPropertyChanged;
            if (inpc != null)
            {
                canExecute = target.GetType().GetProperty("Can" + MethodName);
                if (canExecute != null && target is Control)
                {
                    inpc.PropertyChanged += CanExecuteChanged;
                    ((Control)AssociatedObject).IsEnabled = (bool)canExecute.GetValue(target, null);
                }
            }
        }

        protected override void Invoke(object eventArgs)
        {
            var parameterValues = MessageBinder.DetermineParameters(this, execute.GetParameters(), AssociatedObject, eventArgs);
            var returnValue = execute.Invoke(target, parameterValues);

            if (returnValue == null)
                return;

            var result = MessageBinder.CreateResult(returnValue);
            if (result == null)
                return;

            if (view == null && target is IViewAware)
                view = ((IViewAware)target).GetView() as DependencyObject;

            Log.Info("Invoking {0}.", this);
            result.Execute(new ResultExecutionContext(AssociatedObject, this, target, view));
        }

        void CanExecuteChanged(object sender, PropertyChangedEventArgs e)
        {
            if (canExecute != null && e.PropertyName == canExecute.Name)
            {
                Log.Info("Execution changed for {0}", this);
                ((Control)AssociatedObject).IsEnabled = (bool)canExecute.GetValue(target, null);
            }
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