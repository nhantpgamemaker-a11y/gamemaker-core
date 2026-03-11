using System.Collections.Generic;
using System.Linq;
using GameMaker.Core.Runtime;
using UnityEngine;
using UnityEngine.Audio;

namespace GameMaker.Sound.Runtime
{
    [System.Serializable]
    [ScriptableObjectSingletonPathAttribute("Assets/Resources")]
    [CreateAssetMenu(fileName ="SoundManager",menuName = "GameMaker/Sound/SoundManager")]
    public class SoundManager : BaseScriptableObjectDataManager<SoundManager, SoundDefinition>
    {
        [UnityEngine.SerializeField]
        private AudioMixer _audioMixed;
        public AudioMixer AudioMixer => _audioMixed;

        public List<string> GetMixerGroupNames()
        {
            return _audioMixed.FindMatchingGroups("").Select(x => x.name).ToList();
        }
#if UNITY_EDITOR
        [ContextMenu("Create Sound")]
        private void CreateSound()
        {
            AddDefinition(new SoundDefinition());
            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif
    }
}