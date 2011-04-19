namespace Caliburn.Micro {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    ///   Enables loosely-coupled publication of and subscription to events.
    /// </summary>
    public interface IEventAggregator {
        /// <summary>
        ///   Subscribes an instance to all events declared through implementations of <see cref = "IHandle{T}" />
        /// </summary>
        /// <param name = "instance">The instance to subscribe for event publication.</param>
        void Subscribe(object instance);

        /// <summary>
        ///   Unsubscribes the instance from all events.
        /// </summary>
        /// <param name = "instance">The instance to unsubscribe.</param>
        void Unsubscribe(object instance);

        /// <summary>
        ///   Publishes a message.
        /// </summary>
        /// <param name = "message">The message instance.</param>
        void Publish(object message);
    }

    /// <summary>
    /// Enables loosely-coupled publication of and subscription to events.
    /// </summary>
    public class EventAggregator : IEventAggregator {
        static readonly ILog Log = LogManager.GetLog(typeof(EventAggregator));
        readonly List<Handler> handlers = new List<Handler>();

        /// <summary>
        ///   Subscribes an instance to all events declared through implementations of <see cref = "IHandle{T}" />
        /// </summary>
        /// <param name = "instance">The instance to subscribe for event publication.</param>
        public void Subscribe(object instance) {
            lock(handlers) {
                if(handlers.Any(x => x.Matches(instance)))
                    return;

                Log.Info("Subscribing {0}.", instance);
                handlers.Add(new Handler(instance));
            }
        }

        /// <summary>
        ///  Unsubscribes the instance from all events.
        /// </summary>
        /// <param name = "instance">The instance to unsubscribe.</param>
        public void Unsubscribe(object instance) {
            lock(handlers) {
                var found = handlers.FirstOrDefault(x => x.Matches(instance));

                if(found != null)
                    handlers.Remove(found);
            }
        }

        /// <summary>
        ///   Publishes a message.
        /// </summary>
        /// <param name = "message">The message instance.</param>
        public void Publish(object message) {
            Handler[] toNotify;
            lock(handlers)
                toNotify = handlers.ToArray();

            Execute.OnUIThread(() => {
                Log.Info("Publishing {0}.", message);
                var messageType = message.GetType();

                var dead = toNotify.Where(handler => !handler.Handle(messageType, message));

                if(dead.Any()) {
                    lock(handlers)
                        dead.Apply(x => handlers.Remove(x));
                }
            });
        }

        class Handler {
            readonly WeakReference reference;
            readonly Dictionary<Type, MethodInfo> supportedHandlers = new Dictionary<Type, MethodInfo>();

            public Handler(object handler) {
                reference = new WeakReference(handler);

                var interfaces = handler.GetType().GetInterfaces()
                    .Where(x => typeof(IHandle).IsAssignableFrom(x) && x.IsGenericType);

                foreach(var @interface in interfaces) {
                    var type = @interface.GetGenericArguments()[0];
                    var method = @interface.GetMethod("Handle");
                    supportedHandlers[type] = method;
                }
            }

            public bool Matches(object instance) {
                return reference.Target == instance;
            }

            public bool Handle(Type messageType, object message) {
                var target = reference.Target;
                if(target == null)
                    return false;

                foreach(var pair in supportedHandlers) {
                    if(pair.Key.IsAssignableFrom(messageType)) {
                        pair.Value.Invoke(target, new[] { message });
                        return true;
                    }
                }

                return true;
            }
        }
    }
}