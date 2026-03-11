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
        
        public async override UniTask<bool> AddAsync(string id, string value)
        {
            await _localPropertySaveData.AddPlayerPropertyAsync(id, value.ToString());
            return true;
        }
        public override async UniTask<bool> SetAsync(string id, string  value)
        {
            await _localPropertySaveData.SetPlayerPropertyAsync(id, value);
            return true;
        }

        public async override UniTask<(bool, List<PlayerProperty>)> GetPlayerPropertiesAsync()
        {
            var playerProperties = _localPropertySaveData.GetPlayerProperties();
            return (true, playerProperties);
        }
    }
}