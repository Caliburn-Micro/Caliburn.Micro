using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Windows.UI.Interactivity
{
    /// <summary>
    /// Represents a trigger that can listen to an object other than its AssociatedObject.
    /// 
    /// </summary>
    /// 
    /// <remarks>
    /// This is an infrastructure class. Trigger authors should derive from EventTriggerBase&lt;T&gt; instead of this class.
    /// </remarks>
    public abstract class EventTriggerBase : TriggerBase
    {
        public static readonly DependencyProperty SourceObjectProperty = DependencyProperty.Register("SourceObject", typeof(object), typeof(EventTriggerBase), new PropertyMetadata(null,new PropertyChangedCallback(EventTriggerBase.OnSourceObjectChanged)));
        public static readonly DependencyProperty SourceNameProperty = DependencyProperty.Register("SourceName", typeof(string), typeof(EventTriggerBase), new PropertyMetadata(null,new PropertyChangedCallback(EventTriggerBase.OnSourceNameChanged)));
        private Type sourceTypeConstraint;
        private bool isSourceChangedRegistered;
        private NameResolver sourceNameResolver;
        private MethodInfo eventHandlerMethodInfo;

        /// <summary>
        /// Gets the type constraint of the associated object.
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
                {
                    return ((TypeConstraintAttribute)customAttributes.First()).Constraint;
                }
                else
                {
                    return typeof(FrameworkElement);
                }
            }
        }

        /// <summary>
        /// Gets the source type constraint.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The source type constraint.
        /// </value>
        protected Type SourceTypeConstraint
        {
            get
            {
                return this.sourceTypeConstraint;
            }
        }

        /// <summary>
        /// Gets or sets the target object. If TargetObject is not set, the target will look for the object specified by TargetName. If an element referred to by TargetName cannot be found, the target will default to the AssociatedObject. This is a dependency property.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The target object.
        /// </value>
        public object SourceObject
        {
            get
            {
                return this.GetValue(EventTriggerBase.SourceObjectProperty);
            }
            set
            {
                this.SetValue(EventTriggerBase.SourceObjectProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the name of the element this EventTriggerBase listens for as a source. If the name is not set or cannot be resolved, the AssociatedObject will be used.  This is a dependency property.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The name of the source element.
        /// </value>
        public string SourceName
        {
            get
            {
                return (string)this.GetValue(EventTriggerBase.SourceNameProperty);
            }
            set
            {
                this.SetValue(EventTriggerBase.SourceNameProperty, (object)value);
            }
        }

        /// <summary>
        /// Gets the resolved source. If <c ref="SourceName"/> is not set or cannot be resolved, defaults to AssociatedObject.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The resolved source object.
        /// </value>
        /// 
        /// <remarks>
        /// In general, this property should be used in place of AssociatedObject in derived classes.
        /// </remarks>
        /// <exception cref="T:System.InvalidOperationException">The element pointed to by <c cref="P:System.Windows.Interactivity.EventTriggerBase.Source"/> does not satisify the type constraint.</exception>
        public object Source
        {
            get
            {
                object obj = this.AssociatedObject;
                if (this.SourceObject != null)
                {
                    obj = this.SourceObject;
                }
                else if (this.IsSourceNameSet)
                {
                    obj = (object)this.SourceNameResolver.Object;
                    if (obj != null && !this.SourceTypeConstraint.GetTypeInfo().IsAssignableFrom(obj.GetType().GetTypeInfo()))
                    {
                        throw new InvalidOperationException("Retargeted type constraint violated.");
                    }
                }
                return obj;
            }
        }

        private NameResolver SourceNameResolver
        {
            get
            {
                return this.sourceNameResolver;
            }
        }

        private bool IsSourceChangedRegistered
        {
            get
            {
                return this.isSourceChangedRegistered;
            }
            set
            {
                this.isSourceChangedRegistered = value;
            }
        }

        private bool IsSourceNameSet
        {
            get
            {
                if (string.IsNullOrEmpty(this.SourceName))
                    return this.ReadLocalValue(EventTriggerBase.SourceNameProperty) != DependencyProperty.UnsetValue;
                else
                    return true;
            }
        }

        private bool IsLoadedRegistered { get; set; }

        static EventTriggerBase()
        {
        }

        internal EventTriggerBase(Type sourceTypeConstraint)
            : base(typeof(FrameworkElement))
        {
            this.sourceTypeConstraint = sourceTypeConstraint;
            this.sourceNameResolver = new NameResolver();
            this.RegisterSourceChanged();
        }

        /// <summary>
        /// Specifies the name of the Event this EventTriggerBase is listening for.
        /// 
        /// </summary>
        /// 
        /// <returns/>
        protected abstract string GetEventName();

        /// <summary>
        /// Called when the event associated with this EventTriggerBase is fired. By default, this will invoke all actions on the trigger.
        /// 
        /// </summary>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// Override this to provide more granular control over when actions associated with this trigger will be invoked.
        /// </remarks>
        protected virtual void OnEvent(RoutedEventArgs eventArgs)
        {
            this.InvokeActions((object)eventArgs);
        }

        private void OnSourceChanged(object oldSource, object newSource)
        {
            if (this.AssociatedObject == null)
            {
                return;
            }
            this.OnSourceChangedImpl(oldSource, newSource);
        }

        /// <summary>
        /// Called when the source changes.
        /// 
        /// </summary>
        /// <param name="oldSource">The old source.</param><param name="newSource">The new source.</param>
        /// <remarks>
        /// This function should be overridden in derived classes to hook functionality to and unhook functionality from the changing source objects.
        /// </remarks>
        internal virtual void OnSourceChangedImpl(object oldSource, object newSource)
        {
            if (string.IsNullOrEmpty(this.GetEventName()) || string.Compare(this.GetEventName(), "Loaded", StringComparison.Ordinal) == 0)
            {
                return;
            }
            if (oldSource != null && this.SourceTypeConstraint.GetTypeInfo().IsAssignableFrom(oldSource.GetType().GetTypeInfo()))
            {
                this.UnregisterEvent(oldSource, this.GetEventName());
            }
            if (newSource == null || !this.SourceTypeConstraint.GetTypeInfo().IsAssignableFrom(newSource.GetType().GetTypeInfo()))
            {
                return;
            }
            this.RegisterEvent(newSource, this.GetEventName());
        }

        /// <summary>
        /// Called after the trigger is attached to an AssociatedObject.
        /// 
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
            FrameworkElement associatedObject = this.AssociatedObject;
            Behavior behavior = associatedObject as Behavior;
            this.RegisterSourceChanged();
            if (behavior != null)
            {
                FrameworkElement associatedObject2 = ((IAttachedObject)behavior).AssociatedObject;
                behavior.AssociatedObjectChanged += new EventHandler(this.OnBehaviorHostChanged);
            }
            else
            {
                if (this.SourceObject == null)
                {
                    if (associatedObject != null)
                    {
                        this.SourceNameResolver.NameScopeReferenceElement = associatedObject;
                        goto label_7;
                    }
                }
                try
                {
                    this.OnSourceChanged((object)null, this.Source);
                }
                catch (InvalidOperationException ex)
                {
                }
            }
        label_7:
            if (string.Compare(this.GetEventName(), "Loaded", StringComparison.Ordinal) != 0 || associatedObject == null || Interaction.IsElementLoaded(associatedObject))
            {
                return;
            }
            this.RegisterLoaded(associatedObject);
        }

        /// <summary>
        /// Called when the trigger is being detached from its AssociatedObject, but before it has actually occurred.
        /// 
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching();
            Behavior behavior = this.AssociatedObject as Behavior;
            FrameworkElement associatedElement = this.AssociatedObject as FrameworkElement;
            try
            {
                this.OnSourceChanged(this.Source, (object)null);
            }
            catch (InvalidOperationException ex)
            {
            }
            this.UnregisterSourceChanged();
            if (behavior != null)
                behavior.AssociatedObjectChanged -= new EventHandler(this.OnBehaviorHostChanged);
            this.SourceNameResolver.NameScopeReferenceElement = null;
            if (string.Compare(this.GetEventName(), "Loaded", StringComparison.Ordinal) != 0 || associatedElement == null)
                return;
            this.UnregisterLoaded(associatedElement);
        }

        private void OnBehaviorHostChanged(object sender, EventArgs e)
        {
            this.SourceNameResolver.NameScopeReferenceElement = ((IAttachedObject)sender).AssociatedObject as FrameworkElement;
        }

        private static void OnSourceObjectChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            EventTriggerBase eventTriggerBase = (EventTriggerBase)obj;
            object newSource = (object)eventTriggerBase.SourceNameResolver.Object;
            if (args.NewValue == null)
            {
                eventTriggerBase.OnSourceChanged(args.OldValue, newSource);
            }
            else
            {
                if (args.OldValue == null && newSource != null)
                    eventTriggerBase.UnregisterEvent(newSource, eventTriggerBase.GetEventName());
                eventTriggerBase.OnSourceChanged(args.OldValue, args.NewValue);
            }
        }

        private static void OnSourceNameChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            ((EventTriggerBase)obj).SourceNameResolver.Name = (string)args.NewValue;
        }

        private void RegisterSourceChanged()
        {
            if (this.IsSourceChangedRegistered)
                return;
            this.SourceNameResolver.ResolvedElementChanged += new EventHandler<NameResolvedEventArgs>(this.OnSourceNameResolverElementChanged);
            this.IsSourceChangedRegistered = true;
        }

        private void UnregisterSourceChanged()
        {
            if (!this.IsSourceChangedRegistered)
                return;
            this.SourceNameResolver.ResolvedElementChanged -= new EventHandler<NameResolvedEventArgs>(this.OnSourceNameResolverElementChanged);
            this.IsSourceChangedRegistered = false;
        }

        private void OnSourceNameResolverElementChanged(object sender, NameResolvedEventArgs e)
        {
            if (this.SourceObject != null)
                return;
            this.OnSourceChanged(e.OldObject, e.NewObject);
        }

        private void RegisterLoaded(FrameworkElement associatedElement)
        {
            if (this.IsLoadedRegistered || associatedElement == null)
                return;
            associatedElement.Loaded += this.OnEventImpl;
            this.IsLoadedRegistered = true;
        }

        private void UnregisterLoaded(FrameworkElement associatedElement)
        {
            if (!this.IsLoadedRegistered || associatedElement == null)
                return;
            associatedElement.Loaded -= this.OnEventImpl;
            this.IsLoadedRegistered = false;
        }

        /// <exception cref="T:System.ArgumentException">Could not find eventName on the Target.</exception>
        private void RegisterEvent(object obj, string eventName)
        {
            EventInfo @event = obj.GetType().GetRuntimeEvent(eventName);
            if (@event == null)
            {
                if (this.SourceObject == null)
                {
                    return;
                }
                throw new ArgumentException("EventTrigger cannot find EventName");
            }
            else if (!EventTriggerBase.IsValidEvent(@event))
            {
                if (this.SourceObject == null)
                {
                    return;
                }
                throw new ArgumentException("EventTriggerBase invalid event");
            }
            else
            {
                this.eventHandlerMethodInfo = typeof(EventTriggerBase).GetTypeInfo().GetDeclaredMethod("OnEventImpl");
                Delegate handler = this.eventHandlerMethodInfo.CreateDelegate(@event.EventHandlerType, this);

                WindowsRuntimeMarshal.AddEventHandler<Delegate>(
                        dlg => (EventRegistrationToken)@event.AddMethod.Invoke(obj, new object[] { dlg }),
                        etr => @event.RemoveMethod.Invoke(obj, new object[] { etr }), handler);
            }
        }

        private static bool IsValidEvent(EventInfo eventInfo)
        {
            Type eventHandlerType = eventInfo.EventHandlerType;
            if (!typeof(Delegate).GetTypeInfo().IsAssignableFrom(eventInfo.EventHandlerType.GetTypeInfo()))
            {
                return false;
            }
            ParameterInfo[] parameters = eventHandlerType.GetTypeInfo().GetDeclaredMethod("Invoke").GetParameters();
            if (parameters.Length == 2 && typeof(object).GetTypeInfo().IsAssignableFrom(parameters[0].ParameterType.GetTypeInfo()))
            {
                return typeof(RoutedEventArgs).GetTypeInfo().IsAssignableFrom(parameters[1].ParameterType.GetTypeInfo());
            }
            else
            {
                return false;
            }
        }

        private void UnregisterEvent(object obj, string eventName)
        {
            if (string.Compare(eventName, "Loaded", StringComparison.Ordinal) == 0)
            {
                FrameworkElement associatedElement = obj as FrameworkElement;
                if (associatedElement == null)
                {
                    return;
                }
                this.UnregisterLoaded(associatedElement);
            }
            else
            {
                this.UnregisterEventImpl(obj, eventName);
            }
        }

        private void UnregisterEventImpl(object obj, string eventName)
        {
            Type type = obj.GetType();
            if (this.eventHandlerMethodInfo == null)
            {
                return;
            }
            EventInfo @event = type.GetRuntimeEvent(eventName);
            Delegate handler = this.eventHandlerMethodInfo.CreateDelegate(@event.EventHandlerType, this);
            WindowsRuntimeMarshal.RemoveEventHandler<Delegate>(
                etr => @event.RemoveMethod.Invoke(this.AssociatedObject, new object[] { etr }), handler);
            this.eventHandlerMethodInfo = (MethodInfo)null;
        }

        private void OnEventImpl(object sender, RoutedEventArgs eventArgs)
        {
            this.OnEvent(eventArgs);
        }

        internal void OnEventNameChanged(string oldEventName, string newEventName)
        {
            if (this.AssociatedObject == null)
            {
                return;
            }
            FrameworkElement associatedElement = this.Source as FrameworkElement;
            if (associatedElement != null && string.Compare(oldEventName, "Loaded", StringComparison.Ordinal) == 0)
            {
                this.UnregisterLoaded(associatedElement);
            }
            else if (!string.IsNullOrEmpty(oldEventName))
            {
                this.UnregisterEvent(this.Source, oldEventName);
            }
            if (associatedElement != null && string.Compare(newEventName, "Loaded", StringComparison.Ordinal) == 0)
            {
                this.RegisterLoaded(associatedElement);
            }
            else
            {
                if (string.IsNullOrEmpty(newEventName))
                {
                    return;
                }
                this.RegisterEvent(this.Source, newEventName);
            }
        }
    }
}
