using System;
using System.Collections.Generic;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public abstract class BaseActionData : IObserverData, IEquatable<BaseActionData>
    {
        public BaseActionData()
        {
            
        }
        public BaseActionData(IExtendData extendData)
        {
        }

        public abstract bool Equals(BaseActionData other);

        public virtual List<ActionDefinition> GetGenerateActionDefinitions()
        {
            return new();
        }
    }
}