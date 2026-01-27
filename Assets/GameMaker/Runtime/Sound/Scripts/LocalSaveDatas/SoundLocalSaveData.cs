using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;

namespace GameMaker.Sound.Runtime
{
    [System.Serializable]
    public class SoundLocalSaveData: BaseLocalData
    {
        private float _musicVolume = 1.0f;
        private float _sfxVolume = 1.0f;

        public float MusicVolume { get => _musicVolume; }
        public float SfxVolume { get => _sfxVolume; }

        public async UniTask SetMusicVolumeAsync(float volume, bool isSave = true)
        {
            _musicVolume = volume;
            if (isSave)
            {
                await SaveAsync();
            }
        }
        public async UniTask SetSFXVolumeAsync(float volume, bool isSave = true)
        {
            _sfxVolume = volume;
            if (isSave)
            {
                await SaveAsync();
            }
        }
        public float GetMusicVolume()
        {
            return _musicVolume;
        }
        public float GetSFXVolume()
        {
            return _sfxVolume;
        }
    }
}