using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Feature.Shop.Runtime
{
    public abstract class BaseShopDataSpaceProvider : IDataSpaceProvider
    {
        public abstract UniTask<(bool, List<PlayerShop>)> GetShopsAsync();
        public abstract UniTask<(bool status, List<BaseReceiverProduct> products, BasePrice price)> PurchaseAsync(string shopDefinitionId, string shopItemId);
        public abstract UniTask<bool> InitAsync(BaseDataSpaceSetting baseDataSpaceSetting);
        public abstract UniTask<(bool,PlayerShop)> RefreshShopAsync(string shopDefinitionId, long lastRefreshTime);
        public virtual void Dispose(){}
    }
}