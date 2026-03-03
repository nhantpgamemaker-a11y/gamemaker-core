using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [ScriptableObjectSingletonPathAttribute("Assets/Resources")]
    [CreateAssetMenu(fileName = "ConditionManager", menuName = "GameMaker/Core/ConditionManager")]
    public class ConditionManager : BaseScriptableObjectDataManager<ConditionManager, BaseConditionDefinition>
    {
#if UNITY_EDITOR
        [ContextMenu("Add Value Condition")]
        public void AddValueCondition()
        {
            AddDefinition(new ValueConditionDefinition());
        }
        [ContextMenu("Add Config Condition")]
        public void AddConfigCondition()
        {
            AddDefinition(new ConfigConditionDefinition());
        }
#endif
    }
}