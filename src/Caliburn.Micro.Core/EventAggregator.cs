using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Caliburn.Micro
{
    /// <inheritdoc />
    public class EventAggregator : IEventAggregator
    {
        /// <summary>
        /// Processing of handler results on publication thread.
        /// </summary>
        public static Action<object, object> HandlerResultProcessing = (target, result) => { };

        private readonly List<Handler> _handlers = new List<Handler>();

        /// <inheritdoc />
        public bool HandlerExistsFor(Type messageType)
        {
            lock (_handlers)
            {
                return _handlers.Any(handler => handler.Handles(messageType) & !handler.IsDead);
            }
        }

        /// <inheritdoc />
        public virtual void Subscribe(object subscriber)
        {
            if (subscriber == null)
                throw new ArgumentNullException(nameof(subscriber));

            lock (_handlers)
            {
                if (_handlers.Any(x => x.Matches(subscriber)))
                    return;

                _handlers.Add(new Handler(subscriber));
            }
        }

        /// <inheritdoc />
        public virtual void Unsubscribe(object subscriber)
        {
            if (subscriber == null)
                throw new ArgumentNullException(nameof(subscriber));

            lock (_handlers)
            {
                var found = _handlers.FirstOrDefault(x => x.Matches(subscriber));

                if (found != null)
                    _handlers.Remove(found);
            }
        }

        /// <inheritdoc />
        public virtual Task PublishAsync(object message, Func<Func<Task>, Task> marshal, CancellationToken cancellationToken)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            if (marshal == null)
                throw new ArgumentNullException(nameof(marshal));

            Handler[] toNotify;

            lock (_handlers)
            {
                toNotify = _handlers.ToArray();
            }

            return marshal(async () =>
            {
                var messageType = message.GetType();
                
                var tasks = toNotify.SelectMany(h => h.Handle(messageType, message, CancellationToken.None));

                await Task.WhenAll(tasks);

                var dead = toNotify.Where(h => h.IsDead).ToList();

                if (dead.Any())
                {
                    lock (_handlers)
                    {
                        dead.Apply(x => _handlers.Remove(x));
                    }
                }
            });
        }

        private class Handler
        {
            private readonly WeakReference _reference;
            private readonly Dictionary<Type, MethodInfo> _supportedHandlers = new Dictionary<Type, MethodInfo>();

            public Handler(object handler)
            {
                _reference = new WeakReference(handler);

                var interfaces = handler.GetType().GetTypeInfo().ImplementedInterfaces
                    .Where(x => typeof(IHandle).GetTypeInfo().IsAssignableFrom(x.GetTypeInfo()) && x.GetTypeInfo().IsGenericType);

                foreach (var @interface in interfaces)
                {
                    var type = @interface.GetTypeInfo().GenericTypeArguments[0];
                    var method = @interface.GetRuntimeMethod("HandleAsync", new[]  { type, typeof(CancellationToken) });

                    if (method != null)
                        _supportedHandlers[type] = method;
                }
            }

            public bool IsDead => _reference.Target == null;

            public bool Matches(object instance)
            {
                return _reference.Target == instance;
            }

            public IEnumerable<Task> Handle(Type messageType, object message, CancellationToken cancellationToken)
            {
                var target = _reference.Target;

                if (target == null)
                    return Enumerable.Empty<Task>();

                return _supportedHandlers
                    .Where(handler => handler.Key.GetTypeInfo().IsAssignableFrom(messageType.GetTypeInfo()))
                    .Select(pair => pair.Value.Invoke(target, new[] {message, cancellationToken}))
                    .Select(result => (Task) result)
                    .ToList();
            }

            public bool Handles(Type messageType)
            {
                return _supportedHandlers.Any(pair => pair.Key.GetTypeInfo().IsAssignableFrom(messageType.GetTypeInfo()));
            }
        }
    }
}
