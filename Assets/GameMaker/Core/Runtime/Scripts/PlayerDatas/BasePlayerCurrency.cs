using System;
using System.Numerics;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    [TypeCache]
    public abstract class BasePlayerCurrency : BasePlayerData
    {
        public BasePlayerCurrency(string id, IDefinition definition) : base(id, definition)
        {
        }

        public abstract void AddValue(object value);
    }
}