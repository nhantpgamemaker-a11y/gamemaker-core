using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [ScriptableObjectSingletonPathAttribute("Assets/Resources")]
    [CreateAssetMenu(fileName = "CoreConfig", menuName = "GameMaker/Core/CoreConfig")]
    public class CoreConfig : InheritanceScriptableObjectSingleton<CoreConfig>
    {
        [UnityEngine.SerializeField]
        private TimePolicyConfig _timePolicyConfig;
        public TimePolicyConfig TimePolicyConfig => _timePolicyConfig;
    }
}