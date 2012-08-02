using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Windows.UI.Interactivity
{
    /// <summary>
    /// Static class that owns the Triggers and Behaviors attached properties. Handles propagation of AssociatedObject change notifications.
    /// 
    /// </summary>
    public static class Interaction
    {
        /// <summary>
        /// This property is used as the internal backing store for the public Triggers attached property.
        /// 
        /// </summary>
        public static readonly DependencyProperty TriggersProperty = DependencyProperty.RegisterAttached("Triggers", typeof(TriggerCollection), typeof(Interaction), new PropertyMetadata(null,new PropertyChangedCallback(Interaction.OnTriggersChanged)));
        /// <summary>
        /// This property is used as the internal backing store for the public Behaviors attached property.
        /// 
        /// </summary>
        public static readonly DependencyProperty BehaviorsProperty = DependencyProperty.RegisterAttached("Behaviors", typeof(BehaviorCollection), typeof(Interaction), new PropertyMetadata(null,new PropertyChangedCallback(Interaction.OnBehaviorsChanged)));
        
        /// <summary>
        /// Gets the TriggerCollection containing the triggers associated with the specified object.
        /// 
        /// </summary>
        /// <param name="obj">The object from which to retrieve the triggers.</param>
        /// <returns>
        /// A TriggerCollection containing the triggers associated with the specified object.
        /// </returns>
        public static TriggerCollection GetTriggers(DependencyObject obj)
        {
            TriggerCollection triggerCollection = (TriggerCollection)obj.GetValue(Interaction.TriggersProperty);
            if (triggerCollection == null)
            {
                triggerCollection = new TriggerCollection();
                obj.SetValue(Interaction.TriggersProperty, triggerCollection);
            }
            return triggerCollection;
        }

        /// <summary>
        /// Gets the <see cref="T:System.Windows.Interactivity.BehaviorCollection"/> associated with a specified object.
        /// 
        /// </summary>
        /// <param name="obj">The object from which to retrieve the <see cref="T:System.Windows.Interactivity.BehaviorCollection"/>.</param>
        /// <returns>
        /// A <see cref="T:System.Windows.Interactivity.BehaviorCollection"/> containing the behaviors associated with the specified object.
        /// </returns>
        public static BehaviorCollection GetBehaviors(FrameworkElement obj)
        {
            BehaviorCollection behaviorCollection = (BehaviorCollection)obj.GetValue(Interaction.BehaviorsProperty);
            if (behaviorCollection == null)
            {
                behaviorCollection = new BehaviorCollection();
                obj.SetValue(Interaction.BehaviorsProperty, behaviorCollection);
            }
            return behaviorCollection;
        }

        /// <exception cref="T:System.InvalidOperationException">Cannot host the same BehaviorCollection on more than one object at a time.</exception>
        private static void OnBehaviorsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            BehaviorCollection behaviorCollection1 = args.OldValue as BehaviorCollection;
            BehaviorCollection behaviorCollection2 = args.NewValue as BehaviorCollection;
            if (behaviorCollection1 == behaviorCollection2)
            {
                return;
            }
            if (behaviorCollection1 != null && behaviorCollection1.AssociatedObject != null)
            {
                behaviorCollection1.Detach();
            }
            if (behaviorCollection2 == null || obj == null)
            {
                return;
            }
            if (behaviorCollection2.AssociatedObject != null)
            {
                throw new InvalidOperationException("Cannot Host BehaviorCollection Multiple Times");
            }
            FrameworkElement fElement = obj as FrameworkElement;
            if (fElement == null)
            {
                throw new InvalidOperationException("Can only host BehaviorCollection on FrameworkElement");
            }
            behaviorCollection2.Attach(fElement);
        }

        /// <exception cref="T:System.InvalidOperationException">Cannot host the same TriggerCollection on more than one object at a time.</exception>
        private static void OnTriggersChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            TriggerCollection triggerCollection1 = args.OldValue as TriggerCollection;
            TriggerCollection triggerCollection2 = args.NewValue as TriggerCollection;
            if (triggerCollection1 == triggerCollection2)
            {
                return;
            }
            if (triggerCollection1 != null && triggerCollection1.AssociatedObject != null)
            {
                triggerCollection1.Detach();
            }
            if (triggerCollection2 == null || obj == null)
            {
                return;
            }
            if (triggerCollection2.AssociatedObject != null)
            {
                throw new InvalidOperationException("Cannot Host TriggerCollection Multiple Times");
            }
            FrameworkElement fElement = obj as FrameworkElement;
            if (fElement == null)
            {
                throw new InvalidOperationException("Can only host BehaviorCollection on FrameworkElement");
            }
            triggerCollection2.Attach(fElement);
        }

        /// <summary>
        /// A helper function to take the place of FrameworkElement.IsLoaded, as this property is not available in Silverlight.
        /// 
        /// </summary>
        /// <param name="element">The element of interest.</param>
        /// <returns>
        /// True if the element has been loaded; otherwise, False.
        /// </returns>
        internal static bool IsElementLoaded(FrameworkElement element)
        {
            UIElement rootVisual = Window.Current.Content;
            if ((element.Parent ?? VisualTreeHelper.GetParent(element)) != null)
            {
                return true;
            }
            if (rootVisual != null)
            {
                return element == rootVisual;
            }
            else
            {
                return false;
            }
        }
    }
}
