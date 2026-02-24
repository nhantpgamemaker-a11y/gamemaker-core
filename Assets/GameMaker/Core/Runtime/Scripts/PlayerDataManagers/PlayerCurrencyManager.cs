using System;
using System.Collections.Generic;
using System.Linq;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class PlayerCurrencyManager : PlayerDataManager
    {   
        public BasePlayerCurrency GetPlayerCurrency(string referenceId)
        {
            return GetPlayerData(referenceId) as BasePlayerCurrency;
        }
        public List<BasePlayerCurrency> GetPlayerCurrencies()
        {
            return basePlayerDatas.Cast<BasePlayerCurrency>().ToList();
        }
        public void AddPlayerCurrency(string id, object value)
        {
            var playerCurrency = GetPlayerCurrency(id);
            playerCurrency.AddValue(value);
        }
    }
}