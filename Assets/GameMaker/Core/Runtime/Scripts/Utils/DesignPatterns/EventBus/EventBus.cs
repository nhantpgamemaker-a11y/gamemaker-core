using System;
using System.Collections.Generic;

namespace GameMaker.Core.Runtime
{
    public class EventBus
    {
        private readonly Dictionary<Type, List<Delegate>> _subscribers;
        private readonly Queue<IEvent> _eventQueue;
        public EventBus()
        {
            _subscribers = new Dictionary<Type, List<Delegate>>();
            _eventQueue = new Queue<IEvent>();
        }
        public void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : IEvent
        {
            var type = typeof(TEvent);
            if (!_subscribers.TryGetValue(type, out var handlers))
            {
                handlers = new List<Delegate>();
                _subscribers[type] = handlers;
            }

            handlers.Add(handler);
            Logger.Log($"[EventBus] Subscribe {type.Name} <- {handler.Target}");
        }
        public void UnSubscribe<TEvent>(Action<TEvent> handler)
            where TEvent : IEvent
        {
            var type = typeof(TEvent);

            if (_subscribers.TryGetValue(type, out var handlers))
            {
                handlers.Remove(handler);
                Logger.Log($"[EventBus] UnSubscribe {type.Name} <- {handler.Target}");
                if (handlers.Count == 0)
                    _subscribers.Remove(type);
            }
        }
        public void Publish(IEvent evt)
        {
            Logger.Log($"[EventBus] Publish: {evt.GetType().Name}");
            _eventQueue.Enqueue(evt);
            Dispatch();
        }
        public void ClearAll()
        {
            _subscribers.Clear();
            _eventQueue.Clear();
        }
        private void Dispatch()
        {
            while (_eventQueue.Count > 0)
            {
                var evt = _eventQueue.Dequeue();
                var type = evt.GetType();
                Logger.Log($"[EventBus] Dispatch {type.Name}");

                if (_subscribers.TryGetValue(type, out var handlers))
                {
                    var snapshot = handlers.ToArray();
                    foreach (var handler in snapshot)
                    {
                        Logger.Log($"[EventBus] -> {type.Name} to {handler.Target}.{handler.Method.Name}");
                        handler.DynamicInvoke(evt);
                    }
                }
            }
        }
    }
}