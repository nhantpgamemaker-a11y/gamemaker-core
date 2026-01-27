using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Sound.Runtime
{
    [System.Serializable]
    [ScriptableObjectSingletonPathAttribute("Assets/Resources")]
    [CreateAssetMenu(fileName ="SoundManager",menuName = "GameMaker/Sound/SoundManager")]
    public class SoundManager : BaseScriptableObjectDataManager<SoundManager, SoundDefinition>
    {

    }
}