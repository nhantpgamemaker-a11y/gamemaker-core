using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;

namespace GameMaker.Core.Runtime
{
    [DataSpace(nameof(LocalDataSpaceSetting.LOCAL_SPACE))]
    public class LocalBundleDataSpaceProvider : BaseBundleDataSpaceProvider
    {
        private LocalCurrencySaveData _localCurrencySaveData;
        private LocalPropertySaveData _localPropertySaveData;
        private LocalItemSaveData _localItemSaveData;
        public override async UniTask<(bool, List<BaseReceiverProduct>)> ConsumeAsync(BundleDefinition bundleDefinition)
        {
            var receiverProducts = new List<BaseReceiverProduct>();
            var rewards = bundleDefinition.Rewards;

            var currencyRewards = rewards.OfType<CurrencyRewardDefinition>().GroupBy(x => x.GetReferenceID()).Select(x => new { key = x.Key, value = x.Sum(t => t.Amount)});
            foreach (var currency in currencyRewards)
            {
                _ = _localCurrencySaveData.AddPlayerCurrency(currency.key, currency.value, false);
                receiverProducts.Add(new CurrencyReceiverProduct(currency.key, currency.value));
            }
            var statRewards = rewards.OfType<StatRewardDefinition>();

            foreach(var stat in statRewards)
            {
                switch (stat.UpdateType)
                {
                    case UpdateType.Add:
                        _ = _localPropertySaveData.AddPlayerStatAsync(stat.GetID(), stat.Amount, false);
                        receiverProducts.Add(new StatReceiverProduct(stat.GetID(), stat.Amount, ConsumeType.Add));
                        break;
                    case UpdateType.Override:
                        _ = _localPropertySaveData.SetPlayerStatAsync(stat.GetID(), stat.Amount, false);
                        receiverProducts.Add(new StatReceiverProduct(stat.GetID(), stat.Amount, ConsumeType.Set));
                        break;
                    case UpdateType.OverrideIfGreater:
                        var playerStat = _localPropertySaveData.GetPlayerProperty(stat.GetID()) as PlayerStat;
                        if (playerStat.Value < stat.Amount)
                            _ = _localPropertySaveData.SetPlayerStatAsync(stat.GetID(), stat.Amount, false);
                            receiverProducts.Add(new StatReceiverProduct(stat.GetID(), stat.Amount, ConsumeType.Set));
                        break;
                    case UpdateType.AddSecondToTimeNow:
                        var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                        playerStat = _localPropertySaveData.GetPlayerProperty(stat.GetID()) as PlayerStat;
                        if (playerStat.Value > now)
                        {
                            _ = _localPropertySaveData.AddPlayerStatAsync(stat.GetID(), stat.Amount);
                            receiverProducts.Add(new StatReceiverProduct(stat.GetID(), playerStat.Value + stat.Amount, ConsumeType.Set));
                        }
                        else
                        {
                            _ = _localPropertySaveData.SetPlayerStatAsync(stat.GetID(), now + stat.Amount);
                            receiverProducts.Add(new StatReceiverProduct(stat.GetID(),now + stat.Amount, ConsumeType.Set));
                        }
                        break;
                }

            }

            var itemRewards = rewards.OfType<ItemRewardDefinition>();
            foreach(var item in itemRewards)
            {
                var itemDetailDefinition = item.GetItemDetailDefinition();
                var statRefs = itemDetailDefinition.ItemStatDefinitionRefs.ToList();
                var prefix = itemDetailDefinition.GetPrefixID();
                for(int i=0;i< item.Amount; i++)
                {
                    var newId = $"{prefix}_{Guid.NewGuid()}";
                    var statDefinitionRef = item.CreateItemTemplate.GetItemStatDefinitionRefs(statRefs);
                    var playerItemDetail = new PlayerDetailItem(newId, newId, statDefinitionRef, itemDetailDefinition);
                    _ = _localItemSaveData.AddPlayerItemDetailAsync(playerItemDetail);

                    receiverProducts.Add(new ItemReceiverProduct(newId, newId,statDefinitionRef.Select(x=>x.Clone() as ItemStatDefinitionRef).ToList(),itemDetailDefinition.ItemDefinitionId));
                }
            }

            var t1 = _localCurrencySaveData.SaveAsync();
            var t2 = _localPropertySaveData.SaveAsync();
            var t3 = _localItemSaveData.SaveAsync();

            await UniTask.WhenAll(t1, t2, t3);
            return (true, receiverProducts);
        }

        public async override UniTask<bool> InitAsync(BaseDataSpaceSetting baseDataSpaceSetting)
        {
            _localCurrencySaveData = baseDataSpaceSetting.LocalDataManager.Get<LocalCurrencySaveData>();
            _localPropertySaveData = baseDataSpaceSetting.LocalDataManager.Get<LocalPropertySaveData>();
            _localItemSaveData = baseDataSpaceSetting.LocalDataManager.Get<LocalItemSaveData>();
            return true;
        }
    }
}