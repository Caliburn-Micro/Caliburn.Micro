namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Enables loosely-coupled publication of and subscription to events.
    /// </summary>
    public interface IEventAggregator
    {
        /// <summary>
        /// Subscribes an instance to all events declared through implementations of <see cref="IHandle{T}"/>
        /// </summary>
        /// <param name="instance">The instance to subscribe for event publication.</param>
        void Subscribe(object instance);

        /// <summary>
        /// Publishes a message.
        /// </summary>
        /// <typeparam name="T">The type of message being published.</typeparam>
        /// <param name="message">The message instance.</param>
        void Publish<T>(T message);
    }

    /// <summary>
    /// Enables loosely-coupled publication of and subscription to events.
    /// </summary>
    public class EventAggregator : IEventAggregator
    {
        static readonly ILog Log = LogManager.GetLog(typeof(EventAggregator));
        readonly List<WeakReference> subscribers = new List<WeakReference>();

        /// <summary>
        /// Subscribes an instance to all events declared through implementations of <see cref="IHandle{T}"/>
        /// </summary>
        /// <param name="instance">The instance to subscribe for event publication.</param>
        public void Subscribe(object instance)
        {
            lock (subscribers)
            {
                Log.Info("Subscribing {0}.", instance);
                subscribers.Add(new WeakReference(instance));
            }
        }

        /// <summary>
        /// Publishes a message.
        /// </summary>
        /// <typeparam name="T">The type of message being published.</typeparam>
        /// <param name="message">The message instance.</param>
        public void Publish<T>(T message)
        {
            Execute.OnUIThread(() =>{
                lock(subscribers)
                {
                    Log.Info("Publishing {0}.", message);
                    var dead = new List<WeakReference>();

                    foreach(var reference in subscribers)
                    {
                        var target = reference.Target as IHandle<T>;

                        if(target != null)
                            target.Handle(message);
                        else if(!reference.IsAlive)
                            dead.Add(reference);
                    }

                    dead.Apply(x => subscribers.Remove(x));
                }
            });
        }
    }
}