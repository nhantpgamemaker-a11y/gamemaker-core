using System.Collections.Generic;
using System.Linq;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class PlayerCurrencyManager : PlayerDataManager
    {   
        public void AddObserver(IObserverWithScope<BasePlayerCurrency, string> observer, string[] scopes)
        {
            AddObserver((IObserverWithScope<BasePlayerData, string>)observer, scopes);
        }
        public void RemoveObserver(IObserverWithScope<BasePlayerCurrency, string> observer, string[] scopes)
        {
            RemoveObserver((IObserverWithScope<BasePlayerData, string>)observer, scopes);
        }
        public BasePlayerCurrency GetPlayerCurrency(string referenceId)
        {
            return GetPlayerData(referenceId) as BasePlayerCurrency;
        }
        public List<BasePlayerCurrency> GetPlayerCurrencies()
        {
            return basePlayerDatas.Cast<BasePlayerCurrency>().ToList();
        }
        public void AddPlayerCurrency(string id, float value)
        {
            var playerCurrency = GetPlayerCurrency(id);
            playerCurrency.AddValue(value);
        }
    }
}