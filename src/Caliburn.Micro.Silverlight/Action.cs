namespace Caliburn.Micro
{
    using System.ComponentModel;
    using System.Windows;

    public static class Action
    {
        static readonly ILog Log = LogManager.GetLog(typeof(Action));

        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.RegisterAttached(
                "Target",
                typeof(object),
                typeof(Action),
                new PropertyMetadata(OnTargetChanged)
                );

        public static readonly DependencyProperty TargetWithoutContextProperty =
            DependencyProperty.RegisterAttached(
                "TargetWithoutContext",
                typeof(object),
                typeof(Action),
                new PropertyMetadata(OnTargetWithoutContextChanged)
                );

        public static void SetTarget(DependencyObject d, object target)
        {
            d.SetValue(TargetProperty, target);
        }

        public static object GetTarget(DependencyObject d)
        {
            return d.GetValue(TargetProperty);
        }

        public static void SetTargetWithoutContext(DependencyObject d, object target)
        {
            d.SetValue(TargetWithoutContextProperty, target);
        }

        public static object GetTargetWithoutContext(DependencyObject d)
        {
            return d.GetValue(TargetWithoutContextProperty);
        }

        private static void OnTargetWithoutContextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SetTargetCore(e, d, false);
        }

        private static void OnTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SetTargetCore(e, d, true);
        }

        private static void SetTargetCore(DependencyPropertyChangedEventArgs e, DependencyObject d, bool setContext)
        {
#if SILVERLIGHT
            if (Application.Current != null 
                && Application.Current.RootVisual != null
                    && (bool)Application.Current.RootVisual.GetValue(DesignerProperties.IsInDesignModeProperty))
                return;
#else
            if ((bool)d.GetValue(DesignerProperties.IsInDesignModeProperty))
                return;
#endif

            if (e.NewValue == e.OldValue || e.NewValue == null)
                return;

            var target = e.NewValue;
            var containerKey = e.NewValue as string;

            if (containerKey != null)
                target = IoC.GetInstance(null, containerKey);

            if (setContext && d is FrameworkElement)
            {
                Log.Info("Setting data context of {0} to {1}.", d, target);
                ((FrameworkElement)d).DataContext = target;
            }

            Log.Info("Attaching message handler {0} to {1}.", target, d);
            d.SetValue(Message.HandlerProperty, target);
        }
    }
}