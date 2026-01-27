using System;
using System.Collections.Generic;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public abstract class PlayerProperty : BasePlayerData
    {
        protected PlayerProperty(IDefinition definition) : base(definition)
        {
        }
    }
}
