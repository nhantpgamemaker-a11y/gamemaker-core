using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [ScriptableObjectSingletonPathAttribute("Assets/Resources")]
    [CreateAssetMenu(fileName = "ActionManager", menuName = "GameMaker/Core/ActionManager")]
    public class ActionManager : BaseScriptableObjectDataManager<ActionManager, ActionDefinition>
    {
        protected override void OnLoad()
        {
#if UNITY_EDITOR 
            LoadGenerateActionDefinition();
#endif
        }
#if UNITY_EDITOR 
        [ContextMenu("Reload")]
        private void LoadGenerateActionDefinition()
        {
            var actionDefinitions = TypeUtils.GetAllConcreteDerivedTypes(typeof(BaseActionData))
            .SelectMany(x => (Activator.CreateInstance(x) as BaseActionData).GetGenerateActionDefinitions());
            foreach (var actionDefinition in actionDefinitions)
            {
                var containItem = dataManager.GetDefinition(actionDefinition.GetID());
                if (containItem != null)
                {
                    dataManager.RemoveDefinition(containItem);
                }
                
                dataManager.Definitions.Insert(0, actionDefinition);
            }
            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif
    }
}