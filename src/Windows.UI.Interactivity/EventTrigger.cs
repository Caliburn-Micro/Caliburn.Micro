using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Windows.UI.Interactivity
{
    /// <summary>
    /// A trigger that listens for a specified event on its source and fires when that event is fired.
    /// 
    /// </summary>
    public class EventTrigger : EventTriggerBase<object>
    {
        public static readonly DependencyProperty EventNameProperty = DependencyProperty.Register("EventName", typeof(string), typeof(EventTrigger), new PropertyMetadata((object)"Loaded", new PropertyChangedCallback(EventTrigger.OnEventNameChanged)));

        /// <summary>
        /// Gets or sets the name of the event to listen for. This is a dependency property.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The name of the event.
        /// </value>
        public string EventName
        {
            get
            {
                return (string)this.GetValue(EventTrigger.EventNameProperty);
            }
            set
            {
                this.SetValue(EventTrigger.EventNameProperty, (object)value);
            }
        }

        static EventTrigger()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Windows.Interactivity.EventTrigger"/> class.
        /// 
        /// </summary>
        public EventTrigger()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Windows.Interactivity.EventTrigger"/> class.
        /// 
        /// </summary>
        /// <param name="eventName">Name of the event.</param>
        public EventTrigger(string eventName)
        {
            this.EventName = eventName;
        }

        protected override string GetEventName()
        {
            return this.EventName;
        }

        private static void OnEventNameChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            ((EventTriggerBase)sender).OnEventNameChanged((string)args.OldValue, (string)args.NewValue);
        }
    }
}
