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
        private Dictionary<string, M> _definitionCache = new();
        public List<M> Definitions => _definitions;
        public BaseDefinitionManager()
        {
        }
        public BaseDefinitionManager(List<M> definitions)
        {
            this._definitions = definitions;
        }
        public virtual void AddDefinition(M definition)
        {
            _definitions.Add(definition);
        }
        public virtual void AddDefinitions(List<M> definitions)
        {
            definitions.AddRange(definitions);
        }
        public virtual void RemoveDefinition(M definition)
        {
            _definitions.Remove(definition);
            _definitionCache.Remove(definition.GetID());
        }
        public List<M> GetDefinitions()
        {
            return _definitions;
        }
        public M GetDefinition(string id)
        {
            if (id == null) return default;
            _definitionCache.TryGetValue(id, out var def);
            if(def == null)
            {
                def = _definitions.FirstOrDefault(x => x.GetID() == id);
                _definitionCache[id] = def;
            }
            return def;
        }
        public object Clone()
        {
            return new BaseDefinitionManager<M>(_definitions.Select(d=> (M)d.Clone()).ToList());
        }
    }
}