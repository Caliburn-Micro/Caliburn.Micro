namespace Caliburn.Micro
{
    using System;
    using System.Collections.Generic;

    public static class EventAggregator
    {
        private static readonly List<WeakReference> Subscribers = new List<WeakReference>();
        private static readonly object LockObject = new object();

        public static void Subscribe(object instance)
        {
            lock (LockObject)
            {
                Subscribers.Add(new WeakReference(instance));
            }
        }

        public static void Publish<T>(T message)
        {
            Execute.OnUIThread(() =>{
                lock(LockObject)
                {
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