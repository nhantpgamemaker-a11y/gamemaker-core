using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;
using UnityEngine;
using UnityEngine.Pool;

namespace GameMaker.Sound.Runtime
{
    [RuntimeDataManager(new Type[] { typeof(SoundDataSpaceProvider) }, new Type[] {})]
    [System.Serializable]    
    public class SoundRuntimeManager : BaseRuntimeDataManager
    {
        private string _id = "SoundRuntimeManager";
        private ObjectPool<AudioSource> _audioSourcePool;
        private SoundDataSpaceProvider _soundDataSpaceProvider;

        [UnityEngine.SerializeField]
        private float _musicVolume = 1.0f;
        [UnityEngine.SerializeField]
        private float _sfxVolume = 1.0f;
        private List<AudioSource> _audioSourcesInUse = new List<AudioSource>();

        private AudioSource _bgmAudioSource;
        private AudioClip _currentBGM;
        public override async UniTask<bool> InitializeAsync(IDataSpaceProvider[] dataSpaceProviders, PlayerDataManager[] playerDataManagers)
        {
            _soundDataSpaceProvider = dataSpaceProviders.OfType<SoundDataSpaceProvider>().FirstOrDefault();

            _audioSourcePool = new ObjectPool<AudioSource>(
                createFunc: () =>
                {
                    GameObject audioSourceObject = new GameObject("PooledAudioSource");
                    audioSourceObject.AddComponent<DontDestroyOnLoad>();
                    AudioSource audioSource = audioSourceObject.AddComponent<AudioSource>();
                    audioSource.playOnAwake = false;
                    return audioSource;
                },
                actionOnGet: (audioSource) =>
                {
                    audioSource.gameObject.SetActive(true);
                },
                actionOnRelease: (audioSource) =>
                {
                    audioSource.Stop();
                    audioSource.gameObject.SetActive(false);
                },
                actionOnDestroy: (audioSource) =>
                {
                    GameObject.Destroy(audioSource.gameObject);
                },
                collectionCheck: false,
                defaultCapacity: 10,
                maxSize: 100
            );
            _bgmAudioSource = _audioSourcePool.Get();
            _bgmAudioSource.gameObject.name = "BGM_AudioSource";
            var (mStatus, _musicVolume) = await _soundDataSpaceProvider.GetMusicVolumeAsync();
            var (_sStatus, _sfxVolume) = await _soundDataSpaceProvider.GetSFXVolumeAsync();

            return mStatus && _sStatus;
        }
        public void PlayOneShot(SoundDefinition soundDefinition, Vector3 position)
        {
            AudioSource audioSource = _audioSourcePool.Get();
            _audioSourcesInUse.Add(audioSource);
            audioSource.clip = soundDefinition.Clip;
            audioSource.volume = soundDefinition.Volume* _sfxVolume;
            audioSource.pitch = soundDefinition.Pitch;
            audioSource.outputAudioMixerGroup = soundDefinition.MixerGroup;
            audioSource.priority = soundDefinition.Priority;
            audioSource.transform.position = position;
            audioSource.loop = false;
            audioSource.PlayOneShot(audioSource.clip);
            _ = UniTask.Delay(TimeSpan.FromSeconds(soundDefinition.Clip.length)).ContinueWith(() =>
            {
                _audioSourcePool.Release(audioSource);
                _audioSourcesInUse.Remove(audioSource);
            });
        }
        public void PlayBGM(SoundDefinition soundDefinition)
        {
            if (_currentBGM == soundDefinition.Clip && _bgmAudioSource.isPlaying)
                return;

            _currentBGM = soundDefinition.Clip;
            _bgmAudioSource.clip = soundDefinition.Clip;
            _bgmAudioSource.volume = soundDefinition.Volume * _musicVolume;
            _bgmAudioSource.loop = true;
            _bgmAudioSource.Play();
        }
        public void StopBGM()
        {
            _bgmAudioSource.Stop();
            _currentBGM = null;
        }
        public void StopAllSounds()
        {
            foreach (var audioSource in _audioSourcesInUse.ToList())
            {
                audioSource.Stop();
                _audioSourcePool.Release(audioSource);
                _audioSourcesInUse.Remove(audioSource);
            }
        }
        public float GetGlobalSfxVolume()
        {
            return _sfxVolume;
        }
        public float GetGlobalMusicVolume()
        {
            return _musicVolume;
        }
        public async UniTask SetGlobalSfxVolume(float volume)
        {
            float lastVolume = _sfxVolume;
            bool status = await _soundDataSpaceProvider.SetSFXVolumeAsync(volume);
            if (status)
            {
                _sfxVolume = volume;
                foreach (var audioSource in _audioSourcesInUse)
                {
                    audioSource.volume = (audioSource.volume / lastVolume) * _sfxVolume;
                }
            }
        }
        public async UniTask SetGlobalMusicVolume(float volume)
        {
             float lastVolume = _musicVolume;
            bool status = await _soundDataSpaceProvider.SetMusicVolumeAsync(volume);
            if (status)
            {
                _musicVolume = volume;
                _bgmAudioSource.volume = (_bgmAudioSource.volume / lastVolume) * volume;
            }
        }
    }
}
