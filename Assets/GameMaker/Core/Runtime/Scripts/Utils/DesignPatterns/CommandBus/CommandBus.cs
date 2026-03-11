using System;
using System.Collections.Generic;
using UnityEngine;

namespace TrippleQ.Casual.Runtime.Core
{
    [System.Serializable]
    public class CommandBus
    {
        private readonly Dictionary<Type, Delegate> _handlers;
        public CommandBus()
        {
            _handlers = new();
        }
        public void RegisterHandler<TCommand>(Action<TCommand> handler)where TCommand: ICommand
        {
            var type = typeof(TCommand);

            if (_handlers.TryGetValue(type, out var existing))
            {
                _handlers[type] = Delegate.Combine(existing, handler);
            }
            else
            {
                _handlers[type] = handler;
            }
        }
        public void UnRegisterHandler<TCommand>(Action<TCommand> handler)
        where TCommand : ICommand
        {
            var type = typeof(TCommand);

            if (_handlers.TryGetValue(type, out var existing))
            {
                var current = Delegate.Remove(existing, handler);

                if (current == null)
                    _handlers.Remove(type);
                else
                    _handlers[type] = current;
            }
        }
        public void Execute(ICommand command)
        {
            var type = command.GetType();
            if (_handlers.TryGetValue(type, out var handler))
            {
                handler.DynamicInvoke(command);
            }
        }
    }
}