using System;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public abstract class BaseActionData : IObserverData, IReferenceDefinition
    {
        private object _data;
        public object Data => _data;
        
        public BaseActionData(object data = null)
        {
            _data = data;
        }
        public abstract IDefinition GetDefinition();

        public string GetReferenceID()
        {
            return GetDefinition().GetID();
        }
    }
}