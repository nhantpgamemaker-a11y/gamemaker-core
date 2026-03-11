using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;
using Newtonsoft.Json;

namespace GameMaker.Sound.Runtime
{
    [System.Serializable]
    public class LocalSoundSaveData : BaseLocalData
    {
        [JsonProperty("PlayerSoundVolumes")]
        private List<PlayerSoundVolumeModel> _playerSoundVolumes;

        protected override void OnCreate()
        {
            base.OnCreate();
            _playerSoundVolumes = new();
            foreach (var groupName in SoundManager.Instance.GetMixerGroupNames())
            {
                _playerSoundVolumes.Add(new PlayerSoundVolumeModel(groupName, groupName, 1f));
            }
        }
        protected override void OnLoad()
        {
            base.OnLoad();
            foreach (var groupName in SoundManager.Instance.GetMixerGroupNames())
            {
                var playerSoundVolume = _playerSoundVolumes.FirstOrDefault(x => x.GetID() == groupName);
                if (playerSoundVolume != null) continue;

                _playerSoundVolumes.Add(new PlayerSoundVolumeModel(groupName, groupName, 1f));
            }

        }
        public List<PlayerSoundVolume> GetPlayerSoundVolumes()
        {
            return _playerSoundVolumes.Select(x => x.ToPlayerSoundVolume()).ToList();
        }

        public void SetVolume(string id, float volume)
        {
            var playerSoundVolume = _playerSoundVolumes.FirstOrDefault(x => x.GetID() == id);
            playerSoundVolume.Volume = volume;
            Save();
        }
    }
    [System.Serializable]
    public class PlayerSoundVolumeModel : PlayerDataModel
    {
        [JsonProperty("Volume")]
        private float _volume = 1;
        [JsonIgnore]
        public float Volume { get => _volume; set => _volume = value; }
        public PlayerSoundVolumeModel(string id, string name, float volume) : base(id, name)
        {
            _volume = volume;
        }
        public PlayerSoundVolume ToPlayerSoundVolume()
        {
            return new PlayerSoundVolume(GetID(), _volume);
        }
    }
}
