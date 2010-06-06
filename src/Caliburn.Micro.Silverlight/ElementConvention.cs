namespace Caliburn.Micro
{
    using System;
    using System.Windows;

    public class ElementConvention
    {
        readonly Type elementType;
        readonly DependencyProperty bindableProperty;
        readonly Func<System.Windows.Interactivity.TriggerBase> triggerCreator;
        readonly string parameterProperty;

        public ElementConvention(Type elementType, DependencyProperty bindableProperty, string parameterProperty, Func<System.Windows.Interactivity.TriggerBase> triggerCreator)
        {
            this.elementType = elementType;
            this.bindableProperty = bindableProperty;
            this.parameterProperty = parameterProperty;
            this.triggerCreator = triggerCreator;
        }

        public Type ElementType
        {
            get { return elementType; }
        }

        public DependencyProperty BindableProperty
        {
            get { return bindableProperty; }
        }

        public string ParameterProperty
        {
            get { return parameterProperty; }
        }

        public System.Windows.Interactivity.TriggerBase CreateTrigger()
        {
            return triggerCreator();
        }
    }
}