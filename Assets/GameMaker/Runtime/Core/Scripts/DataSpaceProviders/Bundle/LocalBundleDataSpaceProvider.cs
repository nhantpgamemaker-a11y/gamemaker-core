using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cysharp.Threading.Tasks;

namespace GameMaker.Core.Runtime
{
    [DataSpace(nameof(LocalDataSpaceSetting.LOCAL_SPACE))]
    public class LocalBundleDataSpaceProvider : BaseBundleDataSpaceProvider
    {
        private LocalDataManager _localDataManager;
        private LocalConsumeRewardFactory _localConsumeRewardFactory;

        public override async UniTask<(bool, List<BaseReceiverProduct>)> ConsumeAsync(BundleDefinition bundleDefinition)
        {
            var receiverProducts = new List<BaseReceiverProduct>();
            var rewards = bundleDefinition.Rewards;
            var consumers = rewards
                            .Select(x => x.GetType())
                            .Select(x => _localConsumeRewardFactory.GetLocalConsumerReward(x))
                            .Distinct();
                            
            foreach (var reward in rewards)
            {
                var consumer = _localConsumeRewardFactory.GetLocalConsumerReward(reward.GetType());
                consumer.InitLocalDataManager(_localDataManager);
                var data = consumer.Consume(reward);
                if (data != null)
                {
                    receiverProducts.Add(data);
                }
            }
            var tasks = new List<UniTask>();
            foreach (var consumer in consumers)
            {
                tasks.Add(consumer.SaveAsync());
            }
            await UniTask.WhenAll(tasks);
            return (true, receiverProducts);
        }

        public async override UniTask<bool> InitAsync(BaseDataSpaceSetting baseDataSpaceSetting)
        {
            _localDataManager = baseDataSpaceSetting.LocalDataManager;
            _localConsumeRewardFactory = new();
            return true;
        }
    }
    public class LocalConsumeRewardFactory
    {
        /// <summary>
        /// type_1 is LocalConsumeRewardType
        /// type_2 is RewardDefinition
        /// </summary>
        private Dictionary<Type, Type> _cache;
        private Dictionary<Type, BaseLocalConsumeReward> _localConsumeRewardDict;
        public LocalConsumeRewardFactory()
        {
            _cache = new();
            var baseLocalConsumeRewardTypes =
            TypeUtils.GetAllConcreteDerivedTypes(typeof(BaseLocalConsumeReward))
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
            _localConsumeRewardDict = new();
        }

        public Type GetLocalConsumeRewardType(Type type)
        {
            return _cache[type];
        }
        public BaseLocalConsumeReward GetLocalConsumerReward(Type type)
        {
            var localConsumeRewardType = GetLocalConsumeRewardType(type);
            if (!_localConsumeRewardDict.TryGetValue(localConsumeRewardType, out BaseLocalConsumeReward baseLocalConsumeReward))
            {
                _localConsumeRewardDict[type] = Activator.CreateInstance(localConsumeRewardType) as BaseLocalConsumeReward;
            }
            return baseLocalConsumeReward;

        }
    }

    public abstract class BaseLocalConsumeReward
    {
        protected LocalDataManager localDataManager;
        public void InitLocalDataManager(LocalDataManager localDataManager)
        {
            this.localDataManager = localDataManager;
        }
        public abstract BaseReceiverProduct Consume(BaseRewardDefinition rewardDefinition);
        public abstract UniTask SaveAsync();
    }

    [TypeContain(typeof(CurrencyRewardDefinition))]
    public class CurrencyLocalConsumeReward : BaseLocalConsumeReward
    {
        public override BaseReceiverProduct Consume(BaseRewardDefinition rewardDefinition)
        {
            var currency = rewardDefinition as CurrencyRewardDefinition;
            var localCurrencySaveData = localDataManager.Get<LocalCurrencySaveData>();
            _ = localCurrencySaveData.AddPlayerCurrency(currency.GetReferenceID(), currency.Amount, false);
            return new CurrencyReceiverProduct(currency.GetReferenceID(), currency.Amount);
        }

        public override async UniTask SaveAsync()
        {
            var localCurrencySaveData = localDataManager.Get<LocalCurrencySaveData>();
            await localCurrencySaveData.SaveAsync();
        }
    }

    [TypeContain(typeof(StatRewardDefinition))]
    public class StatLocalConsumeReward : BaseLocalConsumeReward
    {
        public override BaseReceiverProduct Consume(BaseRewardDefinition rewardDefinition)
        {
            var localPropertySaveData = localDataManager.Get<LocalPropertySaveData>();
            var stat = rewardDefinition as StatRewardDefinition;
            switch (stat.UpdateType)
            {
                case UpdateType.Add:
                    _ = localPropertySaveData.AddPlayerStatAsync(stat.GetID(), stat.Amount, false);
                    return new StatReceiverProduct(stat.GetID(), stat.Amount, ConsumeType.Add);
                case UpdateType.Override:
                    _ = localPropertySaveData.SetPlayerStatAsync(stat.GetID(), stat.Amount, false);
                    return new StatReceiverProduct(stat.GetID(), stat.Amount, ConsumeType.Set);
                case UpdateType.OverrideIfGreater:
                    var playerStat = localPropertySaveData.GetPlayerProperty(stat.GetID()) as PlayerStat;
                    if (playerStat.Value < stat.Amount)
                        _ = localPropertySaveData.SetPlayerStatAsync(stat.GetID(), stat.Amount, false);
                    return new StatReceiverProduct(stat.GetID(), stat.Amount, ConsumeType.Set);
                case UpdateType.AddSecondToTimeNow:
                    var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    playerStat = localPropertySaveData.GetPlayerProperty(stat.GetID()) as PlayerStat;
                    if (playerStat.Value > now)
                    {
                        _ = localPropertySaveData.AddPlayerStatAsync(stat.GetID(), stat.Amount);
                        return new StatReceiverProduct(stat.GetID(), playerStat.Value + stat.Amount, ConsumeType.Set);
                    }
                    else
                    {
                        _ = localPropertySaveData.SetPlayerStatAsync(stat.GetID(), now + stat.Amount);
                        return new StatReceiverProduct(stat.GetID(), now + stat.Amount, ConsumeType.Set);
                    }
            }
            return null;
        }

        public override async UniTask SaveAsync()
        {
            var localPropertySaveData = localDataManager.Get<LocalPropertySaveData>();
            await localPropertySaveData.SaveAsync();
        }
    }
    
    [TypeContain(typeof(ItemRewardDefinition))]
    public class ItemLocalConsumeReward : BaseLocalConsumeReward
    {
        public override BaseReceiverProduct Consume(BaseRewardDefinition rewardDefinition)
        {
            var localItemSaveData = localDataManager.Get<LocalItemSaveData>();
            var item = rewardDefinition as ItemRewardDefinition;
            var itemDetailDefinition = item.GetItemDetailDefinition();
            var statRefs = itemDetailDefinition.ItemPropertyDefinitionRefs.ToList();
            var prefix = itemDetailDefinition.GetPrefixID();
            for (int i = 0; i < item.Amount; i++)
            {
                var newId = $"{prefix}_{Guid.NewGuid()}";
                var propertyDefinitionRef = item.CreateItemTemplate.GetItemPropertyDefinitionRefs(statRefs);
                var playerItemDetail = new PlayerDetailItem(newId, newId, propertyDefinitionRef, itemDetailDefinition);
                _ = localItemSaveData.AddPlayerItemDetailAsync(playerItemDetail);

                return new ItemReceiverProduct(newId, newId, propertyDefinitionRef.Select(x => x.Clone() as ItemPropertyDefinitionRef).ToList(), itemDetailDefinition.GetID());
            }
            return null;
        }

        public override async UniTask SaveAsync()
        {
            var localItemSaveData = localDataManager.Get<LocalItemSaveData>();
            await localItemSaveData.SaveAsync();
        }
    }
}