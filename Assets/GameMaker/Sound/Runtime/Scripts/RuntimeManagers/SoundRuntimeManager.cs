using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;

namespace GameMaker.Sound.Runtime
{
    public class SoundRuntimeManager : AutomaticMonoSingleton<SoundRuntimeManager>
    {
        private Dictionary<string, AudioMixerGroup> _audioMixerGroupDict;
        private ObjectPool<AudioSource> _audioSourcePool;
        private readonly Dictionary<string, AudioSource> _loopSources = new Dictionary<string, AudioSource>();
        public override void OnLoad()
        {
            AudioMixerGroup[] groups = SoundManager.Instance.AudioMixer.FindMatchingGroups("");
            _audioMixerGroupDict = groups.ToDictionary(x => x.name, x => x);

            _audioSourcePool = new(
                createFunc: () =>
                {
                    var gameObject = new GameObject("AudioSource", typeof(AudioSource));
                    gameObject.transform.parent = this.transform;
                    var audioSource = gameObject.GetComponent<AudioSource>();
                    audioSource.playOnAwake = false;
                    return audioSource;
                },
                actionOnGet: (audioSource) => { },
                actionOnRelease: (AudioSource) => { },
                actionOnDestroy: (audioSource)=>{});
        }
        public void PlayOneShot(SoundDefinition soundDefinition)
        {
            _ = PlayOneShotInternalAsync(soundDefinition);
        }
        private async UniTask PlayOneShotInternalAsync(SoundDefinition soundDefinition)
        {
            var group = soundDefinition.MixerGroup;
            var mixerGroup = _audioMixerGroupDict[group];
            var audioSource = _audioSourcePool.Get();
            audioSource.outputAudioMixerGroup = mixerGroup;
            audioSource.clip = soundDefinition.Clip;
            audioSource.volume = soundDefinition.VolumeScale;
            audioSource.loop = false;
            audioSource.Play();
            await UniTask.WaitUntil(
                    () => !audioSource.isPlaying,
                    cancellationToken: this.GetCancellationTokenOnDestroy()
                );
            _audioSourcePool.Release(audioSource);
        }
        public void PlayLoop(SoundDefinition soundDefinition)
        {
            var id = soundDefinition.GetID();
            if (_loopSources.ContainsKey(id))
                return;

            var group = soundDefinition.MixerGroup;
            var mixerGroup = _audioMixerGroupDict[group];

            var audioSource = _audioSourcePool.Get();

            audioSource.clip = soundDefinition.Clip;
            audioSource.loop = true;
            audioSource.volume = soundDefinition.VolumeScale;
            audioSource.outputAudioMixerGroup = mixerGroup;
            audioSource.Play();

            _loopSources.Add(id, audioSource);
        }
        public void PlayLoopFade(SoundDefinition soundDefinition,float fadeInTime = 0.3f)
        {
            var id = soundDefinition.GetID();
            if (_loopSources.ContainsKey(id))
                return;

            var mixerGroup = _audioMixerGroupDict[soundDefinition.MixerGroup];
            var audioSource = _audioSourcePool.Get();

            audioSource.clip = soundDefinition.Clip;
            audioSource.loop = true;
            audioSource.volume = 0f;
            audioSource.outputAudioMixerGroup = mixerGroup;
            audioSource.Play();

            _loopSources.Add(id, audioSource);

            FadeIn(audioSource, soundDefinition.VolumeScale, fadeInTime).Forget();
        }
        public void StopLoop(SoundDefinition soundDefinition)
        {
            var id = soundDefinition.GetID();

            if (!_loopSources.TryGetValue(id, out var audioSource))
                return;

            audioSource.Stop();
            _audioSourcePool.Release(audioSource);
            _loopSources.Remove(id);
        }
        public void StopLoopFade(SoundDefinition soundDefinition, float fadeTime = 0.3f)
        {
            StopLoopFadeAsync(soundDefinition, fadeTime).Forget();
        }
        private async UniTask StopLoopFadeAsync(SoundDefinition soundDefinition, float fadeTime = 0.3f)
        {
            var id = soundDefinition.GetID();
            if (!_loopSources.TryGetValue(id, out var audioSource))
                return;

            await FadeOut(audioSource, fadeTime);

            audioSource.Stop();
            _audioSourcePool.Release(audioSource);
            _loopSources.Remove(id);
        }
        private async UniTask FadeOut(AudioSource source, float fadeTime)
        {
            if (source == null || !source.isPlaying)
                return;

            float startVolume = source.volume;
            float t = 0f;

            while (t < fadeTime)
            {
                if (source == null)
                    return;

                t += Time.unscaledDeltaTime;
                source.volume = Mathf.Lerp(startVolume, 0f, t / fadeTime);
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
            source.volume = 0f;
        }
        private async UniTask FadeIn(AudioSource source, float targetVolume, float fadeTime)
        {
            if (source == null)
                return;

            float t = 0f;
            source.volume = 0f;

            while (t < fadeTime)
            {
                t += Time.unscaledDeltaTime;
                source.volume = Mathf.Lerp(0f, targetVolume, t / fadeTime);
                await UniTask.Yield();
            }

            source.volume = targetVolume;
        }
    }
}
