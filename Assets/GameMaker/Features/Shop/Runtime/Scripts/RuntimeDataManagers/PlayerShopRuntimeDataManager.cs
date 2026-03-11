using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;

namespace GameMaker.Feature.Shop.Runtime
{
    [System.Serializable]
    [RuntimeDataManagerAttribute(new Type[] { typeof(BaseShopDataSpaceProvider) })]
    public class PlayerShopRuntimeDataManager : BaseRuntimeDataManager
    {
        private string _id = "PlayerShopRuntimeDataManager";
        private BaseShopDataSpaceProvider _shopDataSpaceProvider;
        private PlayerDataManager[] _playerDataManagers;
        private PlayerCurrencyManager _playerCurrencyManager;
        private PlayerShopManager _playerShopManager;
        public async override UniTask<bool> InitializeAsync(IDataSpaceProvider[] dataSpaceProviders, PlayerDataManager[] playerDataManagers)
        {
            _playerDataManagers = playerDataManagers.ToArray();
            _playerShopManager = playerDataManagers.FirstOrDefault(x => x.GetType() == typeof(PlayerShopManager)) as PlayerShopManager;
            _playerCurrencyManager = playerDataManagers.FirstOrDefault(x => x.GetType() == typeof(PlayerCurrencyManager)) as PlayerCurrencyManager;
            _shopDataSpaceProvider = dataSpaceProviders.OfType<BaseShopDataSpaceProvider>().FirstOrDefault();
            var (status, playerShops) = await _shopDataSpaceProvider.GetShopsAsync();
            if (!status) return false;
            _playerShopManager.Initialize(playerShops.Cast<BasePlayerData>().ToList());
            playerShops.ForEach(x => x.OnInit((lastRefreshTime) =>
            {
                OnShopReset(x, lastRefreshTime);
            }));
            ShopGateway.Initialize(this);
            return true;
        }
        public async UniTask<bool> PurchaseAsync(string shopDefinitionId, string shopItemId, IExtendData extendData)
        {
            var (status, products, price) = await _shopDataSpaceProvider.PurchaseAsync(shopDefinitionId, shopItemId);
            if (!status) return status;
            _playerCurrencyManager.AddPlayerCurrency(price.CurrencyReferenceId, price.GetAmount());
            foreach (var product in products)
            {
                product.Consume(_playerDataManagers, extendData);
            }
            var shopDefinition = ShopManager.Instance.GetDefinition(shopDefinitionId);
            var playerShop = _playerShopManager.GetPlayerShop(shopDefinitionId);
            playerShop.PurchaseItem(shopItemId, shopDefinition.TimeResetConfig.CronExpression == string.Empty);
            RuntimeActionManager.Instance.NotifyAction(CurrencyActionData.ADD_CURRENCY_ACTION_DEFINITION, new CurrencyActionData(price.CurrencyReferenceId,  price.GetAmount(), extendData));
            return true;
        }
        public List<PlayerShop> GetPlayerShops()
        {
            return _playerShopManager.GetPlayerShops();
        }
        public PlayerShop GetPlayerShop(string shopDefinitionId)
        {
            return _playerShopManager.GetPlayerShop(shopDefinitionId);
        }
        private void OnShopReset(PlayerShop playerShop, long lastRefreshTime)
        {
            RefreshShopWithRetryAsync(playerShop, lastRefreshTime).Forget();
        }
        private async UniTask RefreshShopWithRetryAsync(PlayerShop playerShop, long lastRefreshTime)
        {
            var shopDefinition = playerShop.GetDefinition() as ShopDefinition;
            var config = shopDefinition.TimeResetConfig;
            if (config.CronExpression == string.Empty) return;
            if(playerShop.IsShopResetting) return;
            playerShop.SetIsShopResetting(true);
            bool success = false;
            while (!success)
            {
                var (status, refreshedPlayerShop) = await _shopDataSpaceProvider.RefreshShopAsync(shopDefinition.GetID(), lastRefreshTime);
                if (status)
                {
                    playerShop.CopyFrom(refreshedPlayerShop);
                    success = true;
                }
                else
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(5));
                }
            }
            playerShop.SetIsShopResetting(false);
        }
    }
}