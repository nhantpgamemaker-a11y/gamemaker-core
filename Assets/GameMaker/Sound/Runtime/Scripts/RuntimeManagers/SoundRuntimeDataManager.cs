using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;
using UnityEngine;
using UnityEngine.Audio;

namespace GameMaker.Sound.Runtime
{
    [System.Serializable]
    [RuntimeDataManagerAttribute(new Type[] { typeof(SoundDataSpaceProvider) })]
    public class SoundRuntimeDataManager: BaseRuntimeDataManager
    {
        private string _id = "SoundRuntimeDataManager";
        private SoundDataSpaceProvider _soundDataSpaceProvider;
        private SoundDataManager _soundDataManager;
        private AudioMixer _coreMixer;
        private List<string> _mixerGroupNames;
        public async override UniTask<bool> InitializeAsync(IDataSpaceProvider[] dataSpaceProviders, PlayerDataManager[] playerDataManagers)
        {
            _coreMixer = SoundManager.Instance.AudioMixer;
            _soundDataSpaceProvider = dataSpaceProviders.FirstOrDefault(x => x.GetType() == typeof(SoundDataSpaceProvider)) as SoundDataSpaceProvider;
            _soundDataManager = playerDataManagers.FirstOrDefault(x => x.GetType() == typeof(SoundDataManager)) as SoundDataManager;
            var soundVolumes = _soundDataSpaceProvider.GetPlayerSoundVolumes();
            _soundDataManager.Initialize(soundVolumes.Cast<BasePlayerData>().ToList());
            _mixerGroupNames = _coreMixer.FindMatchingGroups(string.Empty).Select(x => x.name).ToList();
            foreach(var name in _mixerGroupNames)
            {
                var playerVolume = _soundDataManager.GetPlayerSoundVolume(name);
                float volumeInDb = Mathf.Log10(Mathf.Clamp(playerVolume.Volume, 0.0001f, 1f)) * 20;
                _coreMixer.SetFloat($"{name}_Volume", volumeInDb);
            }
            return true;
        }

        public void SetVolume(string id, float volume)
        {
            _soundDataSpaceProvider.SetVolume(id, volume);
            _soundDataManager.SetVolume(id, volume);
        }
    }
}