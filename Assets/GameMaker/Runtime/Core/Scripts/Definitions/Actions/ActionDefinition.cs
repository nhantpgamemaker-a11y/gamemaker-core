using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class ActionDefinition : Definition
    {
        [UnityEngine.SerializeField]
        private BaseDefinitionManager<BaseActionParamDefinition> _actionParamManager;
        public ActionDefinition() : base()
        {
            _actionParamManager = new();
        }
        public ActionDefinition(string id, string name, BaseDefinitionManager<BaseActionParamDefinition> actionParamManager):base(id, name)
        {
            _actionParamManager = actionParamManager;
        }
        public override object Clone()
        {
            return new ActionDefinition(GetID(), GetName(), _actionParamManager.Clone() as BaseDefinitionManager<BaseActionParamDefinition>);
        }
    }
}