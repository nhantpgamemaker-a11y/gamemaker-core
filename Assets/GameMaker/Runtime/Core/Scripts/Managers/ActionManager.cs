using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [ScriptableObjectSingletonPathAttribute("Assets/Resources")]
    [CreateAssetMenu(fileName ="ActionManager",menuName = "GameMaker/Core/ActionManager")]
    public class ActionManager : BaseScriptableObjectDataManager<ActionManager, BaseActionDefinition>
    {
        protected override void OnLoad()
        {
            var actionDefinitionTypes = TypeUtils.GetAllDerivedNonAbstractTypes(typeof(BaseActionDefinition));
            List<BaseActionDefinition> defaultActionDefinitions = new();
            foreach (var actionDefinitionType in actionDefinitionTypes)
            {
                var baseDefinition = Activator.CreateInstance(actionDefinitionType) as BaseActionDefinition;
                defaultActionDefinitions.AddRange(baseDefinition.GetCoreActionDefinition());
            }
            
            foreach(var defaultActionDefinition in defaultActionDefinitions)
            {
                if(GetDefinition(defaultActionDefinition.GetID()) == null)
                {
                    AddDefinition(defaultActionDefinition);
                }
            }
        }
    }
}