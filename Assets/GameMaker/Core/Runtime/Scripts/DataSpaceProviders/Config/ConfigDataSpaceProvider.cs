using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace GameMaker.Core.Runtime
{
    [DataSpace(nameof(DataSpaceAttribute.INIT_ANY))]
    public class ConfigDataSpaceProvider : IDataSpaceProvider
    {
        private LocalConfigSaveData _localConfigSaveData;
        public async UniTask<bool> InitAsync(BaseDataSpaceSetting baseDataSpaceSetting)
        {
            _localConfigSaveData = baseDataSpaceSetting.LocalDataManager.Get<LocalConfigSaveData>();
            return true;
        }
        public List<PlayerConfig> GetPlayerConfigs()
        {
            var playerConfigs = _localConfigSaveData.GetPlayerConfigs();
            return playerConfigs;
        }
        public void SetPlayerConfig(string id, string value)
        {
            _localConfigSaveData.SetPlayerConfig(id, value);
        }
        public void Dispose()
        {
            _localConfigSaveData = null;
        }
    }
}