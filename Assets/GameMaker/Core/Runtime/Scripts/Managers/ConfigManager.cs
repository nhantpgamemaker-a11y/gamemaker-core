using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [ScriptableObjectSingletonPathAttribute("Assets/Resources")]
    [CreateAssetMenu(fileName = "ConfigManager", menuName = "GameMaker/Core/ConfigManager")]
    public class ConfigManager : BaseScriptableObjectDataManager<ConfigManager, ConfigDefinition>
    {
#if UNITY_EDITOR
        [ContextMenu("Add Config")]
        public void AddConfig()
        {
            AddDefinition(new ConfigDefinition());
        }
#endif
    }
}