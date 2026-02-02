using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public abstract class BaseActionData : IObserverData
    {
        public BaseActionData(IExtendData extendData)
        {
        }

        public virtual List<ActionDefinition> GetGenerateActionDefinitions()
        {
            return new();
        }
    }
}