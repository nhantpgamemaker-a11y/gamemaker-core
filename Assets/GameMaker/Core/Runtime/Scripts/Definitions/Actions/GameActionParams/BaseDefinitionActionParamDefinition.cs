using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public abstract class BaseDefinitionActionParamDefinition<TDef> : BaseActionParamDefinition where TDef : IDefinition
    {
        public abstract List<TDef> GetDefinitions();
        public BaseDefinitionActionParamDefinition(){}
        public BaseDefinitionActionParamDefinition(string id, string name, string bindingName) : base(id, name, bindingName)
        {
        }
    }
}