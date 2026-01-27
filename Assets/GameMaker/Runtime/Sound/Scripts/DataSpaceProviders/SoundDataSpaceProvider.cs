using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;

namespace GameMaker.Sound.Runtime
{
    [DataSpace(nameof(DataSpaceAttribute.INIT_ANY))]
    public class SoundDataSpaceProvider : IDataSpaceProvider
    {
        private SoundLocalSaveData _soundLocalSaveData;
        public async UniTask<bool> InitAsync(BaseDataSpaceSetting baseDataSpaceSetting)
        {
            _soundLocalSaveData = baseDataSpaceSetting.LocalDataManager.Get<SoundLocalSaveData>();
            return true;
        }
        public async UniTask<bool> SetSFXVolumeAsync(float volume)
        {
            await _soundLocalSaveData.SetSFXVolumeAsync(volume);
            return true;
        }
        public async UniTask<bool> SetMusicVolumeAsync(float volume)
        {   
            await _soundLocalSaveData.SetMusicVolumeAsync(volume);
            return true;
        }

        public async UniTask<(bool status, float volume)> GetSFXVolumeAsync()
        {
            var volume = _soundLocalSaveData.GetSFXVolume();
            return (true, volume);
        }
        public UniTask<(bool status, float volume)> GetMusicVolumeAsync()
        {
            var volume = _soundLocalSaveData.GetMusicVolume();
            return UniTask.FromResult((true, volume));
        }
    }
}