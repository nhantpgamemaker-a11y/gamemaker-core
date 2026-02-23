using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
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
            _playerCurrencyManager = playerDataManagers.FirstOrDefault(x => x.GetType() == typeof(PlayerCurrencyManager)) as PlayerCurrencyManager;
            _shopDataSpaceProvider = dataSpaceProviders.OfType<BaseShopDataSpaceProvider>().FirstOrDefault();
            ShopGateway.Initialize(this);
            return true;
        }
        public async UniTask<bool> PurchaseAsync(string shopDefinitionId, string shopItemId, float amount, IExtendData extendData)
        {
            var (status, products, price) = await _shopDataSpaceProvider.PurchaseAsync(shopDefinitionId, shopItemId, amount);
            if (!status) return status;
            foreach (var product in products)
            {
                product.Consume(_playerDataManagers, extendData);
            }
            _playerCurrencyManager.AddPlayerCurrency(price.CurrencyReferenceId, $"-{price.Amount}");
            RuntimeActionManager.Instance.NotifyAction(CurrencyActionData.ADD_CURRENCY_ACTION_DEFINITION, new CurrencyActionData(price.CurrencyReferenceId,  $"-{price.Amount}", extendData));
            return true;
        }
        public List<BasePlayerShop> GetBasePlayerShops()
        {
            return _playerShopManager.GetPlayerShops();
        }
        public BasePlayerShop GetBasePlayerShop(string shopDefinitionId)
        {
            return _playerShopManager.GetPlayerShop(shopDefinitionId);
        }
    }
}