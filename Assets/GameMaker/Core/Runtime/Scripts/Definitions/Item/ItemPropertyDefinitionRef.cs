using System;
using System.Collections.Generic;
using System.Linq;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public abstract class ItemPropertyDefinitionRef: Definition
    {
        public ItemPropertyDefinitionRef(): base()
        {
            
        }
        public ItemPropertyDefinitionRef(string refId,string name):base(refId,name)
        {
        }

        public abstract ItemPropertyDefinitionRefModel ToItemPropertyDefinitionRefModel();
    }
}