using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;

namespace GameMaker.Feature.Shop.Runtime
{
    [System.Serializable]
    public class PlayerShopManager: PlayerDataManager
    {
        public PlayerShop GetPlayerShop(string referenceId)
        {
            return GetPlayerData(referenceId) as PlayerShop;
        }

        public List<PlayerShop> GetPlayerShops()
        {
            return basePlayerDatas.Cast<PlayerShop>().ToList();
        }
    }
}