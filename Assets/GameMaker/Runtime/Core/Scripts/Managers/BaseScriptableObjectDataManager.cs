using System;
using System.Collections.Generic;
using System.Linq;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public abstract class BaseScriptableObjectDataManager<T, M> : 
    ScriptableObjectSingleton<T>, IDefinitionManager
    
    where T : ScriptableObjectSingleton<T> where M : IDefinition, ICloneable
    {
        [SerializeField]
        protected BaseDefinitionManager<M> dataManager = new();

        public List<M> GetDefinitions()
        {
            return dataManager.GetDefinitions();
        }
        public M GetDefinition(string referenceId)
        {
            return dataManager.GetDefinition(referenceId);
        }
        protected void AddDefinition(M definition)
        {
            dataManager.AddDefinition(definition);
        }
        protected void RemoveDefinition(M definition)
        {
            dataManager.RemoveDefinition(definition);
        }

        List<IDefinition> IDefinitionManager.GetDefinitions()
        {
            return GetDefinitions().Cast<IDefinition>().ToList();
        }
    }
}
