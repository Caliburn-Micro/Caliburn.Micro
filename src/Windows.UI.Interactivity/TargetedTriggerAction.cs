using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Windows.UI.Interactivity
{
    /// <summary>
    /// Represents an action that can be targeted to affect an object other than its AssociatedObject.
    /// 
    /// </summary>
    /// 
    /// <remarks>
    /// This is an infrastructure class. Action authors should derive from TargetedTriggerAction&lt;T&gt; instead of this class.
    /// </remarks>
    public abstract class TargetedTriggerAction : TriggerAction
    {
        public static readonly DependencyProperty TargetObjectProperty = DependencyProperty.Register("TargetObject", typeof(object), typeof(TargetedTriggerAction), new PropertyMetadata(new PropertyChangedCallback(TargetedTriggerAction.OnTargetObjectChanged)));
        public static readonly DependencyProperty TargetNameProperty = DependencyProperty.Register("TargetName", typeof(string), typeof(TargetedTriggerAction), new PropertyMetadata(new PropertyChangedCallback(TargetedTriggerAction.OnTargetNameChanged)));
        private Type targetTypeConstraint;
        private bool isTargetChangedRegistered;
        private NameResolver targetResolver;

        /// <summary>
        /// Gets or sets the target object. If TargetObject is not set, the target will look for the object specified by TargetName. If an element referred to by TargetName cannot be found, the target will default to the AssociatedObject. This is a dependency property.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The target object.
        /// </value>
        public object TargetObject
        {
            get
            {
                return this.GetValue(TargetedTriggerAction.TargetObjectProperty);
            }
            set
            {
                this.SetValue(TargetedTriggerAction.TargetObjectProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the name of the object this action targets. If Target is set, this property is ignored. If Target is not set and TargetName is not set or cannot be resolved, the target will default to the AssociatedObject. This is a dependency property.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The name of the target object.
        /// </value>
        public string TargetName
        {
            get
            {
                return (string)this.GetValue(TargetedTriggerAction.TargetNameProperty);
            }
            set
            {
                this.SetValue(TargetedTriggerAction.TargetNameProperty, (object)value);
            }
        }

        /// <summary>
        /// Gets the target object. If TargetObject is set, returns TargetObject. Else, if TargetName is not set or cannot be resolved, defaults to the AssociatedObject.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The target object.
        /// </value>
        /// 
        /// <remarks>
        /// In general, this property should be used in place of AssociatedObject in derived classes.
        /// </remarks>
        /// <exception cref="T:System.InvalidOperationException">The Target element does not satisfy the type constraint.</exception>
        protected object Target
        {
            get
            {
                object obj = this.AssociatedObject;
                if (this.TargetObject != null)
                {
                    obj = this.TargetObject;
                }
                else if (this.IsTargetNameSet)
                {
                    obj = this.TargetResolver.Object;
                }
                if (obj == null || this.TargetTypeConstraint.GetTypeInfo().IsAssignableFrom(obj.GetType().GetTypeInfo()))
                {
                    return obj;
                }
                throw new InvalidOperationException("Retargeted Type Constraint Violated");
            }
        }

        /// <summary>
        /// Gets the associated object type constraint.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The associated object type constraint.
        /// </value>
        /// 
        /// <remarks>
        /// Define a TypeConstraintAttribute on a derived type to constrain the types it may be attached to.
        /// </remarks>
        protected override sealed Type AssociatedObjectTypeConstraint
        {
            get
            {
                IEnumerable<Attribute> customAttributes = this.GetType().GetTypeInfo().GetCustomAttributes(typeof(TypeConstraintAttribute), true);
                int index = 0;
                if (index < customAttributes.Count())
                    return ((TypeConstraintAttribute)customAttributes.First()).Constraint;
                else
                    return typeof(FrameworkElement);
            }
        }

        /// <summary>
        /// Gets the target type constraint.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The target type constraint.
        /// </value>
        protected Type TargetTypeConstraint
        {
            get
            {
                return this.targetTypeConstraint;
            }
        }

        private bool IsTargetNameSet
        {
            get
            {
                if (string.IsNullOrEmpty(this.TargetName))
                    return this.ReadLocalValue(TargetedTriggerAction.TargetNameProperty) != DependencyProperty.UnsetValue;
                else
                    return true;
            }
        }

        private NameResolver TargetResolver
        {
            get
            {
                return this.targetResolver;
            }
        }

        private bool IsTargetChangedRegistered
        {
            get
            {
                return this.isTargetChangedRegistered;
            }
            set
            {
                this.isTargetChangedRegistered = value;
            }
        }

        static TargetedTriggerAction()
        {
        }

        internal TargetedTriggerAction(Type targetTypeConstraint)
            : base(typeof(FrameworkElement))
        {
            this.targetTypeConstraint = targetTypeConstraint;
            this.targetResolver = new NameResolver();
            this.RegisterTargetChanged();
        }

        /// <summary>
        /// Called when the target changes.
        /// 
        /// </summary>
        /// <param name="oldTarget">The old target.</param><param name="newTarget">The new target.</param>
        /// <remarks>
        /// This function should be overriden in derived classes to hook and unhook functionality from the changing source objects.
        /// </remarks>
        internal virtual void OnTargetChangedImpl(object oldTarget, object newTarget)
        {
        }

        /// <summary>
        /// Called after the action is attached to an AssociatedObject.
        /// 
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
            FrameworkElement associatedObject = this.AssociatedObject;
            Behavior behavior = associatedObject as Behavior;
            this.RegisterTargetChanged();
            if (behavior != null)
            {
                associatedObject = ((IAttachedObject)behavior).AssociatedObject;
                behavior.AssociatedObjectChanged += new EventHandler(this.OnBehaviorHostChanged);
            }
            this.TargetResolver.NameScopeReferenceElement = associatedObject;
        }

        /// <summary>
        /// Called when the action is being detached from its AssociatedObject, but before it has actually occurred.
        /// 
        /// </summary>
        protected override void OnDetaching()
        {
            Behavior behavior = this.AssociatedObject as Behavior;
            base.OnDetaching();
            this.OnTargetChangedImpl(this.TargetResolver.Object, null);
            this.UnregisterTargetChanged();
            if (behavior != null)
            {
                behavior.AssociatedObjectChanged -= new EventHandler(this.OnBehaviorHostChanged);
            }
            this.TargetResolver.NameScopeReferenceElement = null;
        }

        private void OnBehaviorHostChanged(object sender, EventArgs e)
        {
            this.TargetResolver.NameScopeReferenceElement = ((IAttachedObject)sender).AssociatedObject;
        }

        private void RegisterTargetChanged()
        {
            if (this.IsTargetChangedRegistered)
            {
                return;
            }
            this.TargetResolver.ResolvedElementChanged += new EventHandler<NameResolvedEventArgs>(this.OnTargetChanged);
            this.IsTargetChangedRegistered = true;
        }

        private void UnregisterTargetChanged()
        {
            if (!this.IsTargetChangedRegistered)
            {
                return;
            }
            this.TargetResolver.ResolvedElementChanged -= new EventHandler<NameResolvedEventArgs>(this.OnTargetChanged);
            this.IsTargetChangedRegistered = false;
        }

        private static void OnTargetObjectChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ((TargetedTriggerAction)obj).OnTargetChanged(obj, new NameResolvedEventArgs(args.OldValue, args.NewValue));
        }

        private static void OnTargetNameChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ((TargetedTriggerAction)obj).TargetResolver.Name = (string)args.NewValue;
        }

        private void OnTargetChanged(object sender, NameResolvedEventArgs e)
        {
            if (this.AssociatedObject == null)
            {
                return;
            }
            this.OnTargetChangedImpl(e.OldObject, e.NewObject);
        }
    }
}
