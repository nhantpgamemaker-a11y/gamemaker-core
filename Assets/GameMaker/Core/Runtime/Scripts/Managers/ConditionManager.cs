using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [ScriptableObjectSingletonPathAttribute("Assets/Resources")]
    [CreateAssetMenu(fileName = "ConditionManager", menuName = "GameMaker/Core/ConditionManager")]
    public class ConditionManager : BaseScriptableObjectDataManager<ConditionManager, ConditionDefinition>
    {
#if UNITY_EDITOR
        [ContextMenu("Add Condition")]
        public void AddCondition()
        {
            AddDefinition(new ConditionDefinition());
        }
#endif
    }
}