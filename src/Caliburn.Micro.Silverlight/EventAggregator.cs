namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;

    public interface IEventAggregator
    {
        void Subscribe(object instance);
        void Publish<T>(T message);
    }

    public class EventAggregator : IEventAggregator
    {
        static readonly ILog Log = LogManager.GetLog(typeof(EventAggregator));
        readonly List<WeakReference> subscribers = new List<WeakReference>();

        public void Subscribe(object instance)
        {
            lock (subscribers)
            {
                Log.Info("Subscribing {0}.", instance);
                subscribers.Add(new WeakReference(instance));
            }
        }

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