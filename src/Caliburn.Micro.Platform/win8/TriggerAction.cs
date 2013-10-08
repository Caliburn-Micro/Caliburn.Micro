using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Microsoft.Xaml.Interactivity;

namespace Caliburn.Micro
{
    public class TriggerAction<T> : DependencyObject, IAction
    {
        public static readonly DependencyProperty AssociatedObjectProperty =
            DependencyProperty.Register("AssociatedObject", typeof (FrameworkElement), typeof (TriggerAction<T>), new PropertyMetadata(null, OnAssociatedObjectChanged));

        public FrameworkElement AssociatedObject
        {
            get
            {
                return (FrameworkElement)GetValue(AssociatedObjectProperty);
            }
            set
            {
                SetValue(AssociatedObjectProperty, value);
            }
        }

        protected virtual void Invoke(object eventArgs)
        {
            
        }

        public virtual object Execute(object sender, object parameter)
        {
            Invoke(parameter);

            return null;
        }

        protected virtual void OnAttached()
        {
            
        }

        protected virtual void OnDetaching()
        {
            
        }

        private static void OnAssociatedObjectChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var triggerAction = (TriggerAction<T>) d;

            if (triggerAction == null)
                return;

            if (e.OldValue != null)
                triggerAction.OnDetaching();

            if (e.NewValue != null)
                triggerAction.OnAttached();
        }
    }
}
