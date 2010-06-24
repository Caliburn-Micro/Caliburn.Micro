namespace Caliburn.Micro
{
    using System;
    using System.Windows;

    /// <summary>
    /// Represents the conventions for a particular element type.
    /// </summary>
    public class ElementConvention
    {
        /// <summary>
        /// The type of element to which the conventions apply.
        /// </summary>
        public Type ElementType;

        /// <summary>
        /// The default property to be used in binding conventions.
        /// </summary>
        public DependencyProperty BindableProperty;

        /// <summary>
        /// The default trigger to be used when wiring actions on this element.
        /// </summary>
        public Func<System.Windows.Interactivity.TriggerBase> CreateTrigger;

        /// <summary>
        /// The default property to be used for parameters of this type in actions.
        /// </summary>
        public string ParameterProperty;
    }
}