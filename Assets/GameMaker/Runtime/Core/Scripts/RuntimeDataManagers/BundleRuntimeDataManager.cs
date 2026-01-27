using System;
using System.Linq;
using Cysharp.Threading.Tasks;

namespace GameMaker.Core.Runtime
{
    [RuntimeDataManager(new Type[] { typeof(BaseBundleDataSpaceProvider) }, new Type[] { typeof(PlayerCurrencyManager), typeof(PlayerPropertyManager), typeof(PlayerItemDetailManager) })]
    [System.Serializable]
    public class BundleRuntimeDataManager : BaseRuntimeDataManager
    {
        private string _id = "BundleRuntimeDataManager";
        private BaseBundleDataSpaceProvider _bundleDataSpaceProvider;
        private PlayerCurrencyManager _playerCurrencyManager;
        private PlayerPropertyManager _playerPropertyManager;
        private PlayerItemDetailManager _playerItemDetailManager;
        public async override UniTask<bool> InitializeAsync(IDataSpaceProvider[] dataSpaceProviders, PlayerDataManager[] playerDataManagers)
        {
            _playerCurrencyManager = playerDataManagers.FirstOrDefault(x => x.GetType() == typeof(PlayerCurrencyManager)) as PlayerCurrencyManager;
            _playerPropertyManager = playerDataManagers.FirstOrDefault(x => x.GetType() == typeof(PlayerPropertyManager)) as PlayerPropertyManager;
            _playerItemDetailManager = playerDataManagers.FirstOrDefault(x => x.GetType() == typeof(PlayerItemDetailManager)) as PlayerItemDetailManager;
            _bundleDataSpaceProvider = dataSpaceProviders.OfType<BaseBundleDataSpaceProvider>().FirstOrDefault();
            return true;
        }

        public async UniTask<bool> ConsumeAsync(BundleDefinition bundleDefinition, IExtendData extendData)
        {
            var (status, baseReceiverProducts) = await _bundleDataSpaceProvider.ConsumeAsync(bundleDefinition);
            if (!status) return status;
            var statReceiverProducts = baseReceiverProducts.OfType<StatReceiverProduct>();
            foreach (var stat in statReceiverProducts)
            {
                switch (stat.ConsumeType)
                {
                    case ConsumeType.Add:
                        _playerPropertyManager.AddStat(stat.ID, stat.Value, extendData);
                        break;
                    case ConsumeType.Set:
                        _playerPropertyManager.SetStat(stat.ID, stat.Value, extendData);
                        break;
                }
            }
            var currencyReceiverProducts = baseReceiverProducts.OfType<CurrencyReceiverProduct>();
            foreach (var currency in currencyReceiverProducts)
            {
                _playerCurrencyManager.AddPlayerCurrency(currency.ID, currency.Value, extendData);
            }
            
            var itemReceiverProducts = baseReceiverProducts.OfType<ItemReceiverProduct>();
            foreach(var item in itemReceiverProducts)
            {
                var itemDefinition = ItemManager.Instance.GetDefinition(item.ItemDefinitionId);
                _playerItemDetailManager.AddPlayerItem(new PlayerDetailItem(item.ID, item.Name, item.ItemStatDefinitionRefs,itemDefinition),extendData);
            }
            return true;
        }
    }
}