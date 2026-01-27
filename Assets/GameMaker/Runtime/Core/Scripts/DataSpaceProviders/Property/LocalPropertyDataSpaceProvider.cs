using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [DataSpace(nameof(LocalDataSpaceSetting.LOCAL_SPACE))]
    public class LocalPropertyDataSpaceProvider : BasePropertyDataSpaceProvider
    {
        private LocalPropertySaveData _localPropertySaveData;
        public async override UniTask<bool> InitAsync(BaseDataSpaceSetting baseDataSpaceSetting)
        {
            _localPropertySaveData = (baseDataSpaceSetting as LocalDataSpaceSetting).LocalDataManager.Get<LocalPropertySaveData>();
            return true;
        }
        
        public async override UniTask<bool> AddStatAsync(string id, long value)
        {
            await _localPropertySaveData.AddPlayerStatAsync(id, value);
            return true;
        }
        public override async UniTask<bool> SetAttributeAsync(string id, string value)
        {
            await _localPropertySaveData.SetPlayerAttributeAsync(id, value);
            return true;
        }

        public async override UniTask<(bool, List<PlayerProperty>)> GetPlayerPropertiesAsync()
        {
            var playerProperties = _localPropertySaveData.GetPlayerProperties();
            return (true, playerProperties);
        }

        public override async UniTask<bool> SetStatAsync(string id, long value)
        {
            await _localPropertySaveData.SetPlayerStatAsync(id, value);
            return true;
        }
    }
}