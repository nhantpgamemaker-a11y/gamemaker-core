using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class BaseDefinitionManager<M>:ICloneable where M: IDefinition, ICloneable
    {
        [UnityEngine.SerializeReference]
        private List<M> _definitions = new List<M>();
        [NonSerialized]
        private Dictionary<string, M> _definitionCache;
        public List<M> Definitions => _definitions;
        public BaseDefinitionManager()
        {
            
        }
        public BaseDefinitionManager(List<M> definitions)
        {
            this._definitions = definitions;
        }
        private void BuildCache()
        {
            _definitionCache = new Dictionary<string, M>();

            foreach (var def in _definitions)
            {
                if (def == null) continue;

                string id = def.GetID();

                if (string.IsNullOrEmpty(id))
                {
                    Debug.LogWarning($"{typeof(M).Name}: Definition has empty ID, skipping.");
                    continue;
                }

                if (_definitionCache.ContainsKey(id))
                {
                    Debug.LogWarning($"{typeof(M).Name}: Duplicate ID '{id}' found.");
                    continue;
                }

                _definitionCache.Add(id, def);
            }
        }
        private void EnsureCache()
        {
            if (_definitionCache == null)
                BuildCache();
        }
        public virtual void AddDefinition(M definition)
        {
            _definitions.Add(definition);
            BuildCache();
        }
        public virtual void AddDefinitions(List<M> definitions)
        {
            definitions.AddRange(definitions);
            BuildCache();
        }
        public virtual void RemoveDefinition(M definition)
        {
            _definitions.Remove(definition);
            BuildCache();
        }
        public List<M> GetDefinitions()
        {
            return _definitions;
        }
        public M GetDefinition(string id)
        {
            EnsureCache();

            if (id == null) return default;
            _definitionCache.TryGetValue(id, out var def);
            return def;
        }
        public object Clone()
        {
            return new BaseDefinitionManager<M>(_definitions.Select(d=> (M)d.Clone()).ToList());
        }
    }
}