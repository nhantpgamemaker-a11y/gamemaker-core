
using System;
using System.Collections.Generic;
using System.Data.Common;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;

namespace GameMaker.Core.Runtime
{
    [DataSpace(nameof(LocalDataSpaceSetting.LOCAL_SPACE))]
    public class LocalItemDataSpaceProvider : BaseItemDataSpaceProvider
    {
        public async override UniTask<bool> InitAsync(BaseDataSpaceSetting baseDataSpaceSetting)
        {
            return true;
        }
        public async override UniTask<bool> UpdatePlayerDetailItemAsync(string id, PlayerDetailItem playerDetailItem)
        {
            throw new NotImplementedException();
        }
        public async override UniTask<bool> AddPlayerItemDetailAsync(PlayerDetailItem playerDetailItem)
        {
            throw new NotImplementedException();
        }
        public async override UniTask<bool> RemovePlayerItemDetailAsync(PlayerDetailItem playerDetailItem)
        {
            throw new NotImplementedException();
        }
        public async override UniTask<(bool, List<PlayerDetailItem>)> GetPlayerItemDetails()
        {
            return (true, new());
        }
    }
}