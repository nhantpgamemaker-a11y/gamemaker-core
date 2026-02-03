using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [CreateAssetMenu(fileName = "CoreConfig", menuName = "GameMaker/Core/CoreConfig")]
    public class CoreConfig : ScriptableObjectSingleton<CoreConfig>
    {
        private TimePolicyConfig _timePolicyConfig;
        public TimePolicyConfig TimePolicyConfig => _timePolicyConfig;
    }
}