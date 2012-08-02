using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Markup;

namespace Windows.UI.Interactivity
{
    /// <summary>
    /// Represents an object that can invoke Actions conditionally.
    /// 
    /// </summary>
    /// 
    /// <remarks>
    /// This is an infrastructure class. Trigger authors should derive from Trigger&lt;T&gt; instead of this class.
    /// </remarks>
    [ContentProperty(Name = "Actions")]
    public abstract class TriggerBase : InteractivityBase
    {
        public static readonly DependencyProperty ActionsProperty = DependencyProperty.Register("Actions", typeof(TriggerActionCollection), typeof(TriggerBase), null);
        
        /// <summary>
        /// Gets the actions associated with this trigger.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The actions associated with this trigger.
        /// </value>
        public TriggerActionCollection Actions
        {
            get
            {
                return (TriggerActionCollection)this.GetValue(TriggerBase.ActionsProperty);
            }
        }

        /// <summary>
        /// Event handler for registering to PreviewInvoke.
        /// 
        /// </summary>
        public event EventHandler<PreviewInvokeEventArgs> PreviewInvoke;

        internal TriggerBase(Type associatedObjectTypeConstraint)
        {
            this.AssociatedObjectTypeConstraint = associatedObjectTypeConstraint;
            TriggerActionCollection actionCollection = new TriggerActionCollection();
            this.SetValue(TriggerBase.ActionsProperty, actionCollection);
        }

        /// <summary>
        /// Invoke all actions associated with this trigger.
        /// 
        /// </summary>
        /// 
        /// <remarks>
        /// Derived classes should call this to fire the trigger.
        /// </remarks>
        protected void InvokeActions(object parameter)
        {
            if (this.PreviewInvoke != null)
            {
                PreviewInvokeEventArgs e = new PreviewInvokeEventArgs();
                this.PreviewInvoke((object)this, e);
                if (e.Cancelling)
                {
                    return;
                }
            }
            foreach (TriggerAction triggerAction in this.Actions)
            {
                triggerAction.CallInvoke(parameter);
            }
        }

        /// <summary>
        /// Attaches to the specified object.
        /// 
        /// </summary>
        /// <param name="frameworkElement">The object to attach to.</param><exception cref="T:System.InvalidOperationException">Cannot host the same trigger on more than one object at a time.</exception><exception cref="T:System.InvalidOperationException">dependencyObject does not satisfy the trigger type constraint.</exception>
        public override void Attach(FrameworkElement frameworkElement)
        {
            if (frameworkElement == this.AssociatedObject)
            {
                return;
            }
            if (this.AssociatedObject != null)
            {
                throw new InvalidOperationException("Cannot Host Trigger Multiple Times");
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
                this.Actions.Attach(frameworkElement);
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
            this.Actions.Detach();
            this.OnAssociatedObjectChanged();
        }
    }
}
