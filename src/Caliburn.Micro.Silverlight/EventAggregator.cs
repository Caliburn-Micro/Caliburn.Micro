namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;

    public static class EventAggregator
    {
        static readonly ILog Log = LogManager.GetLog(typeof(EventAggregator));
        static readonly List<WeakReference> Subscribers = new List<WeakReference>();
        static readonly object LockObject = new object();

        public static void Subscribe(object instance)
        {
            lock (LockObject)
            {
                Log.Info("Subscribing {0}.", instance);
                Subscribers.Add(new WeakReference(instance));
            }
        }

        public static void Publish<T>(T message)
        {
            Execute.OnUIThread(() =>{
                lock(LockObject)
                {
                    Log.Info("Publishing {0}.", message);
                    var dead = new List<WeakReference>();

                    foreach(var reference in Subscribers)
                    {
                        var target = reference.Target as IHandler<T>;

                        if(target != null)
                            target.Handle(message);
                        else if(!reference.IsAlive)
                            dead.Add(reference);
                    }

                    dead.Apply(x => Subscribers.Remove(x));
                }
            });
        }
    }
}