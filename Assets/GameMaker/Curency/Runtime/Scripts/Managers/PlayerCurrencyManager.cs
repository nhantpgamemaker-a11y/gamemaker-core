using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Currency.Runtime
{
    [System.Serializable]
    public class PlayerCurrencyManager : PlayerDataManager
    {
        private BaseDataSpaceProviderCurrency _baseDataSpaceProviderCurrency;
        public PlayerCurrencyManager(BaseDataSpaceProviderCurrency baseDataSpaceProviderCurrency,List<PlayerCurrency> basePlayerDatas) : base(basePlayerDatas.Cast<BasePlayerData>().ToList())
        {
            _baseDataSpaceProviderCurrency = baseDataSpaceProviderCurrency;
        }
        public void AddObserver(IObserverWithScope<PlayerCurrency, string> observer, string[] scopes)
        {
            AddObserver((IObserverWithScope<BasePlayerData, string>)observer, scopes);
        }
        public void RemoveObserver(IObserverWithScope<PlayerCurrency, string> observer, string[] scopes)
        {
            RemoveObserver((IObserverWithScope<BasePlayerData, string>)observer, scopes);
        }
        public PlayerCurrency GetPlayerCurrency(string referenceId)
        {
            return GetPlayerData(referenceId) as PlayerCurrency;
        }
        public async UniTask<bool> AddCurrencyAsync(string referenceId, float value, IExtendData extendData)
        {
            bool status = await _baseDataSpaceProviderCurrency.AddCurrencyAsync(referenceId, value);
            if (status)
            {
                AddRuntimeCurrency(referenceId, value, extendData);
            }
            return status;
        }
        public void AddRuntimeCurrency(string referenceId, float value, IExtendData extendData)
        {
            var playerCurrency = GetPlayerCurrency(referenceId);
            playerCurrency.AddValue(value);
            RuntimeActionManager.Instance.NotifyAction(CurrencyActionDefinition.ADD_CURRENCY_ACTION_DEFINITION_ID, new CurrencyActionData(referenceId, value, extendData));
        }
    }
}