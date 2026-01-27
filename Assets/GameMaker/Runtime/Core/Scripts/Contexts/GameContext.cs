using System;
using System.Collections.Generic;
using System.Reflection;

namespace GameMaker.Core.Runtime
{
    public sealed class GameContext
    {
        private readonly Dictionary<Type, object> _services = new();
        
        public void Register<T>(T instance)
        {
            var type = typeof(T);
            _services[type] = instance;
            GameMaker.Core.Runtime.Logger.Log($"[Context] REGISTER {type.Name} => {instance.GetType().Name}");
        }

        public bool TryGet<T>(out T service)
        {
            if (_services.TryGetValue(typeof(T), out var obj))
            {
                service = (T)obj;
                return true;
            }

            service = default;
            return false;
        }

        public T Get<T>()
        {
            if (TryGet<T>(out var service))
                return service;

            throw new Exception($"Service not registered: {typeof(T)}");
        }

        public bool Contains<T>()
        {
            return _services.ContainsKey(typeof(T));
        }

        public void Dump()
        {
           
        }
    }
}