using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    public abstract class BaseDataSpaceSetting : ScriptableObject
    {
        private LocalDataManager _localDataManager;
        public LocalDataManager LocalDataManager => _localDataManager;
        private readonly Dictionary<Type, object> _providers = new();
        public virtual async UniTask<bool> InitAsync()
        {
            _localDataManager = new();
            await _localDataManager.InitAsync();
            var providerTypes = TypeUtils.GetAllLeafDerivedTypes(typeof(IDataSpaceProvider))
            .Where(x => x.GetCustomAttribute<DataSpaceAttribute>().Name == nameof(DataSpaceAttribute.INIT_ANY)).ToList();
            foreach (var type in providerTypes)
            {
                var provider = Activator.CreateInstance(type) as IDataSpaceProvider;
                await provider.InitAsync(this);
                RegisterProvider(type, provider);
            }
            
            return true;
        }
        protected void RegisterProvider<T>(T provider)
        {
            _providers[typeof(T)] = provider;
        }
        protected void RegisterProvider<T>(Type type, T provider)
        {
            _providers[type] = provider;
        }
        
        public T GetProvider<T>()
        {
            return (T)_providers[typeof(T)];
        }

        public T GetProvider<T>(Type type)
        {
            return (T)_providers[type];
        }
        public T Resolve<T>(Type type) where T : class
        {
            foreach (var p in _providers.Values)
            {
                if (type.IsAssignableFrom(p.GetType()))
                {
                    return (T)p;
                }
            }

            return null;
        }
    }
}