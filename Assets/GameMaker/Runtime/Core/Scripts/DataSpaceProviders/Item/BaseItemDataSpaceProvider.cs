using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;

namespace GameMaker.Core.Runtime
{
    public abstract class BaseItemDataSpaceProvider : IDataSpaceProvider
    {
        public abstract UniTask<bool> InitAsync(BaseDataSpaceSetting baseDataSpaceSetting);
        public abstract UniTask<bool> UpdatePlayerDetailItemAsync(string id, PlayerDetailItem playerDetailItem);
        public abstract UniTask<bool> AddPlayerItemDetailAsync(PlayerDetailItem playerDetailItem);
        public abstract UniTask<bool> RemovePlayerItemDetailAsync(PlayerDetailItem playerDetailItem);
        public abstract UniTask<(bool, List<PlayerDetailItem>)> GetPlayerItemDetails();
    }
}