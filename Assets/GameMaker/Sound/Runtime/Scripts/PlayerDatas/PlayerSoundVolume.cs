using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;

namespace GameMaker.Sound.Runtime
{
    [System.Serializable]
    public class PlayerSoundVolume : BasePlayerData
    {
        [UnityEngine.SerializeField]
        private float _volume;
        public float Volume => _volume;
        public PlayerSoundVolume(string id, float volume) : base(id, null)
        {
            _volume = volume;
        }

        public override object Clone()
        {
            return new PlayerSoundVolume(GetID(), _volume);
        }

        public override void CopyFrom(BasePlayerData basePlayerData)
        {
            var reference = basePlayerData as PlayerSoundVolume;
            _volume = reference._volume;
        }
        public void SetVolume(float volume)
        {
            _volume = volume;
            NotifyObserver(this);
        }
        public override string GetReferenceID()
        {
            return GetID();
        }
    }
}