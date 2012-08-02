using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;

namespace Windows.UI.Interactivity
{
    /// <summary>
    /// Represents an attachable object that encapsulates a unit of functionality.
    /// 
    /// </summary>
    /// 
    /// <remarks>
    /// This is an infrastructure class. Action authors should derive from TriggerAction&lt;T&gt; instead of this class.
    /// </remarks>
    [DefaultTrigger(typeof(ButtonBase), typeof(EventTrigger), "Click")]
    [DefaultTrigger(typeof(UIElement), typeof(EventTrigger), "MouseLeftButtonDown")]
    public abstract class TriggerAction : InteractivityBase
    {
        public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.Register("IsEnabled", typeof(bool), typeof(TriggerAction), new PropertyMetadata((object)true));
        private bool isHosted;

        /// <summary>
        /// Gets or sets a value indicating whether this action will run when invoked. This is a dependency property.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// <c>True</c> if this action will be run when invoked; otherwise, <c>False</c>.
        /// 
        /// </value>
        [DefaultValue(true)]
        public bool IsEnabled
        {
            get
            {
                return (bool)this.GetValue(TriggerAction.IsEnabledProperty);
            }
            set
            {
                this.SetValue(TriggerAction.IsEnabledProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is attached.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// <c>True</c> if this instance is attached; otherwise, <c>False</c>.
        /// </value>
        internal bool IsHosted
        {
            get
            {
                return this.isHosted;
            }
            set
            {
                this.isHosted = value;
            }
        }

        internal TriggerAction(Type associatedObjectTypeConstraint)
        {
            this.AssociatedObjectTypeConstraint = associatedObjectTypeConstraint;
        }

        /// <summary>
        /// Attempts to invoke the action.
        /// 
        /// </summary>
        /// <param name="parameter">The parameter to the action. If the action does not require a parameter, the parameter may be set to a null reference.</param>
        internal void CallInvoke(object parameter)
        {
            if (!this.IsEnabled)
                return;
            this.Invoke(parameter);
        }

        /// <summary>
        /// Invokes the action.
        /// 
        /// </summary>
        /// <param name="parameter">The parameter to the action. If the action does not require a parameter, the parameter may be set to a null reference.</param>
        protected abstract void Invoke(object parameter);
        
        /// <summary>
        /// Attaches to the specified object.
        /// 
        /// </summary>
        /// <param name="frameworkElement">The object to attach to.</param><exception cref="T:System.InvalidOperationException">Cannot host the same TriggerAction on more than one object at a time.</exception><exception cref="T:System.InvalidOperationException">dependencyObject does not satisfy the TriggerAction type constraint.</exception>
        public override void Attach(FrameworkElement frameworkElement)
        {
            if (frameworkElement == this.AssociatedObject)
            {
                return;
            }
            if (this.AssociatedObject != null)
            {
                throw new InvalidOperationException("Cannot Host TriggerAction Multiple Times");
            }
            if (frameworkElement != null && !this.AssociatedObjectTypeConstraint.GetTypeInfo().IsAssignableFrom(frameworkElement.GetType().GetTypeInfo()))
            {
                throw new InvalidOperationException("Type Constraint Violated");
            }
            else
            {
                this.AssociatedObject = frameworkElement;
                this.OnAssociatedObjectChanged();
                //we need to fix the datacontext for databinding to work
                this.ConfigureDataContext();
                this.OnAttached();
            }
        }

        /// <summary>
        /// Detaches this instance from its associated object.
        /// 
        /// </summary>
        public override void Detach()
        {
            this.OnDetaching();
            this.AssociatedObject = null;
            this.OnAssociatedObjectChanged();
        }
    }
}
