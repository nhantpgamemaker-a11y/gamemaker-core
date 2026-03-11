using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Feature.Shop.Runtime
{
    [DataSpace(nameof(LocalDataSpaceSetting.LOCAL_SPACE))]
    public class LocalShopDataSpaceProvider : BaseShopDataSpaceProvider
    {
        private LocalShopSaveData _localShopSaveData;
        private LocalCurrencySaveData _localCurrencySaveData;
        private LocalDataManager _localDataManager;
        private LocalConsumeShopItemFactory _localConsumeShopItemFactory;
        public async override UniTask<bool> InitAsync(BaseDataSpaceSetting baseDataSpaceSetting)
        {
            _localDataManager = baseDataSpaceSetting.LocalDataManager;
            _localShopSaveData = (baseDataSpaceSetting as LocalDataSpaceSetting).LocalDataManager.Get<LocalShopSaveData>();
            _localCurrencySaveData = (baseDataSpaceSetting as LocalDataSpaceSetting).LocalDataManager.Get<LocalCurrencySaveData>();
            _localConsumeShopItemFactory = new();
            return true;
        }

        public async override UniTask<(bool status, List<BaseReceiverProduct> products, BasePrice price)> PurchaseAsync(string shopDefinitionId, string shopItemId)
        {
            var shopDefinition = ShopManager.Instance.GetDefinition(shopDefinitionId);
            var shopItemDefinition = shopDefinition.GetShopItemDefinition(shopItemId);
            var price = shopItemDefinition.Price;
            var currentCurrency = _localCurrencySaveData.GetPlayerCurrency(price.CurrencyReferenceId);
            if (currentCurrency == null || !currentCurrency.IsEnough(price.GetAmount()))
            {
                return (false, null, null);
            }

            await _localShopSaveData.PurchaseAsync(shopDefinitionId, shopItemId, shopDefinition.TimeResetConfig.CronExpression == string.Empty);
            
            var consumer = _localConsumeShopItemFactory.GetLocalConsumeShopItem(shopItemDefinition.GetType());
            consumer.InitLocalDataManager(_localDataManager);
            await _localCurrencySaveData.AddPlayerCurrency(shopItemDefinition.Price.CurrencyReferenceId,
                                                    shopItemDefinition.Price.GetNegative().GetAmount());
            await consumer.SaveAsync();
            return (true, new() { consumer.Consume(shopItemDefinition) }, shopItemDefinition.Price.GetNegative());
        }
        
        public async override UniTask<(bool, List<PlayerShop>)> GetShopsAsync()
        {
            return (true, _localShopSaveData.GetPlayerShops());
        }

        public async override UniTask<(bool, PlayerShop)> RefreshShopAsync(string shopDefinitionId, long lastRefreshTime)
        {
            var newPlayerShop = await _localShopSaveData.RefreshShopAsync(shopDefinitionId, lastRefreshTime);
            return (true, newPlayerShop);
        }
    }

    public class LocalConsumeShopItemFactory
    {
        private Dictionary<Type, Type> _cache;
        private Dictionary<Type, BaseLocalConsumeShopItem> _localConsumeShopItemDict;
        public LocalConsumeShopItemFactory()
        {
            _cache = new();
            var baseLocalConsumeRewardTypes =
            TypeUtils.GetAllConcreteDerivedTypes(typeof(BaseLocalConsumeShopItem))
            .Where(x =>
            {
                return x.GetCustomAttribute<TypeContainAttribute>() != null;
            });

            foreach (var baseLocalConsumeRewardType in baseLocalConsumeRewardTypes)
            {
                var type = baseLocalConsumeRewardType
                            .GetCustomAttribute<TypeContainAttribute>().Type;
                _cache[type] = baseLocalConsumeRewardType;
            }
            _localConsumeShopItemDict = new();
        }
        public Type GetLocalConsumeRewardType(Type type)
        {
            return _cache[type];
        }
        public BaseLocalConsumeShopItem GetLocalConsumeShopItem(Type type)
        {
            var localConsumeRewardType = GetLocalConsumeRewardType(type);
            if (!_localConsumeShopItemDict.TryGetValue(localConsumeRewardType, out BaseLocalConsumeShopItem baseLocalConsumeShopItem))
            {
                baseLocalConsumeShopItem = Activator.CreateInstance(localConsumeRewardType) as BaseLocalConsumeShopItem;
                _localConsumeShopItemDict[type] = baseLocalConsumeShopItem;
            }
            return baseLocalConsumeShopItem;

        }
    }
    [TypeCache]
    public abstract class BaseLocalConsumeShopItem
    {
        protected LocalDataManager _localDataManager;
        public void InitLocalDataManager(LocalDataManager localDataManager)
        {
            _localDataManager = localDataManager;
        }
        public abstract BaseReceiverProduct Consume(BaseShopItemDefinition shopItemDefinition);
        public abstract UniTask SaveAsync();
    }
    [TypeCache]
    public abstract class BaseCurrencyLocalConsumeShopItem : BaseLocalConsumeShopItem
    {
        public abstract override BaseReceiverProduct Consume(BaseShopItemDefinition shopItemDefinition);

        public override async UniTask SaveAsync()
        {
            var localCurrencySaveData = _localDataManager.Get<LocalCurrencySaveData>();
            await localCurrencySaveData.SaveAsync();
        }
    }
    [TypeContain(typeof(BigIntCurrencyShopItemDefinition))]
    public class BigIntCurrencyLocalConsumeShopItem : BaseCurrencyLocalConsumeShopItem
    {
        public override BaseReceiverProduct Consume(BaseShopItemDefinition shopItemDefinition)
        {
            var bigIntCurrencyShopItemDefinition = shopItemDefinition as BigIntCurrencyShopItemDefinition;
            var localCurrencySaveData = _localDataManager.Get<LocalCurrencySaveData>();
            localCurrencySaveData.AddPlayerCurrency(shopItemDefinition.GetReferenceID(), bigIntCurrencyShopItemDefinition.GetAmount(), false).Forget();
            return new BigIntCurrencyReceiverProduct(bigIntCurrencyShopItemDefinition.GetReferenceID(), bigIntCurrencyShopItemDefinition.GetAmount().ToString());
        }
    }
    [TypeContain(typeof(LongCurrencyShopItemDefinition))]
    public class LongCurrencyLocalConsumeShopItem : BaseCurrencyLocalConsumeShopItem
    {
        public override BaseReceiverProduct Consume(BaseShopItemDefinition shopItemDefinition)
        {
            var longCurrencyShopItemDefinition = shopItemDefinition as LongCurrencyShopItemDefinition;
            var localCurrencySaveData = _localDataManager.Get<LocalCurrencySaveData>();
            localCurrencySaveData.AddPlayerCurrency(shopItemDefinition.GetReferenceID(), longCurrencyShopItemDefinition.GetAmount(), false).Forget();
            return new LongCurrencyReceiverProduct(longCurrencyShopItemDefinition.GetReferenceID(), longCurrencyShopItemDefinition.GetAmount());
        }
    }
    [TypeContain(typeof(ItemShopItemDefinition))]
    public class ItemLocalConsumeShopItem : BaseLocalConsumeShopItem
    {
        public override BaseReceiverProduct Consume(BaseShopItemDefinition shopItemDefinition)
        {
            var itemShopItemDefinition = shopItemDefinition as ItemShopItemDefinition;
            var localInventorySaveData = _localDataManager.Get<LocalItemSaveData>();
            var itemDetailDefinition = itemShopItemDefinition.GetDefinition() as ItemDetailDefinition;
            var prefix = itemDetailDefinition.GetPrefixID();
            var newId = $"{prefix}_{Guid.NewGuid()}";
            var statRefs = itemDetailDefinition.ItemPropertyDefinitionRefs.ToList();
            var propertyDefinitionRef = itemShopItemDefinition.CreateItemTemplate.GetItemPropertyDefinitionRefs(statRefs);
            var playerItemDetail = new PlayerDetailItem(newId, newId, propertyDefinitionRef, itemDetailDefinition);
            localInventorySaveData.AddPlayerItemDetailAsync(playerItemDetail).Forget();
            return new ItemReceiverProduct(newId, newId, propertyDefinitionRef.Select(x => x.Clone() as ItemPropertyDefinitionRef).ToList(), itemDetailDefinition.GetID());
        }

        public override UniTask SaveAsync()
        {
            var localInventorySaveData = _localDataManager.Get<LocalItemSaveData>();
            return localInventorySaveData.SaveAsync();
        }
    }
}