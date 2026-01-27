using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Cysharp.Threading.Tasks;

namespace GameMaker.Core.Runtime
{
    [RuntimeDataManager(new Type[] { typeof(BaseCurrencyDataSpaceProvider) }, new Type[] { typeof(PlayerCurrencyManager) })]
    [System.Serializable]
    public class PlayerCurrencyRuntimeDataManager : BaseRuntimeDataManager
    {
        private string _id = "PlayerCurrencyRuntimeDataManager";
        [UnityEngine.SerializeField]
        private PlayerCurrencyManager _playerCurrencyManager;
        public PlayerCurrencyManager PlayerCurrencyManager => _playerCurrencyManager;

        private BaseCurrencyDataSpaceProvider _currencyDataSpaceProvider;

        public async override UniTask<bool> InitializeAsync(IDataSpaceProvider[] dataSpaceProviders, PlayerDataManager[] playerDataManagers)
        {
            _playerCurrencyManager = playerDataManagers.FirstOrDefault(x => x.GetType() == typeof(PlayerCurrencyManager)) as PlayerCurrencyManager;
            _currencyDataSpaceProvider = dataSpaceProviders.OfType<BaseCurrencyDataSpaceProvider>().FirstOrDefault();
            var (status, playerCurrencies) = await _currencyDataSpaceProvider.GetPlayerCurrenciesAsync();
            if (!status) return status;
            _playerCurrencyManager.Initialize(playerCurrencies.Cast<BasePlayerData>().ToList());
            CurrencyGateway.Initialize(this);
            return true;
        }
        public async UniTask<bool> AddPlayerCurrencyAsync(string id, float amount, IExtendData extendData)
        {
            bool status = await _currencyDataSpaceProvider.AddCurrencyAsync(id, amount);
            if (status)
            {
                _playerCurrencyManager.AddPlayerCurrency(id, amount, extendData);
            }
            return status;
        }
        public List<PlayerCurrency> GetPlayerCurrencies()
        {
            return _playerCurrencyManager.GetPlayerCurrencies();
        }
        public PlayerCurrency GetPlayerCurrency(string id)
        {
            return _playerCurrencyManager.GetPlayerCurrency(id);
        }
    }
}