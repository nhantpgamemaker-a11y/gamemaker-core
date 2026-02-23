using GameMaker.Core.Runtime;
using UnityEngine;
using UnityEngine.Audio;

namespace GameMaker.Sound.Runtime
{
    [System.Serializable]
    public class SoundDefinition : BaseDefinition
    {
        [UnityEngine.SerializeField]
        AudioClip _clip;
        [UnityEngine.SerializeField]
        private string _mixerGroup;
        [UnityEngine.SerializeField]
        private float _volumeScale;
        public AudioClip Clip => _clip;
        public string MixerGroup => _mixerGroup;
        public float VolumeScale => _volumeScale;
        public SoundDefinition(): base()
        {
            
        }
        public SoundDefinition(string id,
        string name,
        string title,
        string description,
        Sprite icon,
        BaseMetaData metaData,
        AudioClip clip,
        string mixerGroup,
        float volumeScale):base(id, name, title, description, icon, metaData)
        {
            _clip = clip;
            _mixerGroup = mixerGroup;
            _volumeScale = volumeScale;
        }
        public override object Clone()
        {
            return new SoundDefinition(GetID(), GetName(), GetTitle(), GetDescription(), GetIcon(), GetMetaData(), _clip, _mixerGroup, _volumeScale);
        }
    }
}
