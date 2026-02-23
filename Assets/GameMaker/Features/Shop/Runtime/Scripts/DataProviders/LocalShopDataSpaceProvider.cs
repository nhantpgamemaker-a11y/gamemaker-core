using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Feature.Shop.Runtime
{
    [DataSpace(nameof(LocalDataSpaceSetting.LOCAL_SPACE))]
    public class LocalShopDataSpaceProvider : BaseShopDataSpaceProvider
    {
        public async override UniTask<bool> InitAsync(BaseDataSpaceSetting baseDataSpaceSetting)
        {
            return true;
        }
        public override UniTask<(bool, List<BasePlayerShop>)> GetShops()
        {
            throw new System.NotImplementedException();
        }

        public async override UniTask<(bool status, List<BaseReceiverProduct> products, Price price)> PurchaseAsync(string shopDefinitionId, string shopItemId, float amount)
        {
            var shopDefinition = ShopManager.Instance.GetDefinition(shopDefinitionId);
            var shopItemDefinition = shopDefinition.GetShopItemDefinition(shopItemId);
            return (true, new(), shopItemDefinition.Price);
        }
    }
}