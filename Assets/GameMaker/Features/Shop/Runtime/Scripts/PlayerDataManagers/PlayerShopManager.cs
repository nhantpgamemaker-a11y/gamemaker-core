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
        public BasePlayerShop GetPlayerShop(string referenceId)
        {
            return GetPlayerData(referenceId) as BasePlayerShop;
        }

        public List<BasePlayerShop> GetPlayerShops()
        {
            return basePlayerDatas.Cast<BasePlayerShop>().ToList();
        }
    }
}