using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [DataSpace(nameof(LocalDataSpaceSetting.LOCAL_SPACE))]
    public class LocalTimedDataSpaceProvider : BaseTimedDataSpaceProvider
    {
        private LocalTimedSaveData _localTimedSaveData;

        public async override UniTask<bool> InitAsync(BaseDataSpaceSetting baseDataSpaceSetting)
        {
            _localTimedSaveData = (baseDataSpaceSetting as LocalDataSpaceSetting).LocalDataManager.Get<LocalTimedSaveData>();
            return true;
        }
        public async override UniTask<(bool, List<PlayerTimed>)> GetPlayerTimedAsync()
        {
            var playerTimeds = _localTimedSaveData.GetPlayerTimeds();
            return (true, playerTimeds);
        }

        public async override UniTask<(bool, PlayerTimed)> AddTimedAsync(string refId, long amount)
        {
            var playerTimed = await _localTimedSaveData.AddTimedAsync(refId, amount);
            return (true, playerTimed);
        }
    }
}