using GameMaker.Core.Runtime;
using UnityEngine;
using UnityEngine.Audio;

namespace GameMaker.Sound.Runtime
{
    public class SoundDefinition : BaseDefinition
    {
        [UnityEngine.SerializeField]
        private AudioClip _clip;
        [UnityEngine.SerializeField] [Range(0f, 1f)]
        private float _volume;
        [UnityEngine.SerializeField] [Range(0.5f, 2f)]
        private float _pitch = 1f;
        [UnityEngine.SerializeField]
        private AudioMixerGroup _mixerGroup;
        [UnityEngine.SerializeField]
        [Range(0, 256)]
        private int _priority = 128;
        public AudioClip Clip { get => _clip;  }
        public float Volume { get => _volume; }
        public float Pitch { get => _pitch; }
        public AudioMixerGroup MixerGroup { get => _mixerGroup; }
        public int Priority { get => _priority; }
        public SoundDefinition(): base()
        {
            
        }
        public SoundDefinition(string id,string name, string title, string description, Sprite icon,BaseMetaData metaData, AudioClip clip, float volume, float pitch, AudioMixerGroup audioMixerGroup, int priority)
        :base(id, name, title, description, icon,metaData)
        {
            _clip = clip;
            _volume = volume;
            _pitch = pitch;
            _mixerGroup = audioMixerGroup;
            _priority = priority;
        }

        
        public override object Clone()
        {
            return new SoundDefinition(GetID(), GetName(), GetTitle(), GetDescription(), GetIcon(), GetMetaData(), _clip, _volume, _pitch, _mixerGroup, _priority);
        }
    }
}
