using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;

namespace GameMaker.Core.Runtime
{
    [DataSpace(nameof(LocalDataSpaceSetting.LOCAL_SPACE))]
    public class LocalCurrencyDataSpaceProvider : BaseCurrencyDataSpaceProvider
    {
        private LocalCurrencySaveData _localCurrencySaveData = null;
        public async override UniTask<bool> InitAsync(BaseDataSpaceSetting baseDataSpaceSetting)
        {
            _localCurrencySaveData = (baseDataSpaceSetting as LocalDataSpaceSetting).LocalDataManager.Get<LocalCurrencySaveData>();
            return true;
        }
        public override async UniTask<bool> AddCurrencyAsync(string currencyDefinitionId, object value)
        {
            await _localCurrencySaveData.AddPlayerCurrency(currencyDefinitionId, value);
            return true;
        }

        public override async UniTask<(bool status,List<BasePlayerCurrency> playerCurrencies)> GetPlayerCurrenciesAsync()
        {
            var playerCurrencies = _localCurrencySaveData.GetPlayerCurrencies();
            return (true, playerCurrencies);
        }

        public override async UniTask<(bool status, BasePlayerCurrency playerCurrency)> GetPlayerCurrencyAsync(string currencyDefinitionId)
        {
            var playerCurrency = _localCurrencySaveData.GetPlayerCurrency(currencyDefinitionId);
            return (true, playerCurrency);
        }
    }
}