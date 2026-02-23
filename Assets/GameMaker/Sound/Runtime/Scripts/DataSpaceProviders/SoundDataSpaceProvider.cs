using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;

namespace GameMaker.Sound.Runtime
{
    [DataSpace(nameof(DataSpaceAttribute.INIT_ANY))]
    public class SoundDataSpaceProvider : IDataSpaceProvider
    {
        private LocalSoundSaveData _localSoundData;
        public async UniTask<bool> InitAsync(BaseDataSpaceSetting baseDataSpaceSetting)
        {
            _localSoundData = baseDataSpaceSetting.LocalDataManager.Get<LocalSoundSaveData>();
            return true;
        }
        public void Dispose()
        {
            _localSoundData = null;
        }

        public List<PlayerSoundVolume> GetPlayerSoundVolumes()
        {
            return _localSoundData.GetPlayerSoundVolumes();
        }
        public void SetVolume(string id, float volume)
        {
            _localSoundData.SetVolume(id, volume);
        }
    }
}