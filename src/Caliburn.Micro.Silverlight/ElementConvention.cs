namespace Caliburn.Micro
{
    using System;
    using System.Windows;

    public class ElementConvention
    {
        public Type ElementType;
        public DependencyProperty BindableProperty;
        public Func<System.Windows.Interactivity.TriggerBase> CreateTrigger;
        public string ParameterProperty;
    }
}