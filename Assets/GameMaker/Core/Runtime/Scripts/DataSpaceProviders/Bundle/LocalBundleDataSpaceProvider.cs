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
        public async override UniTask<bool> InitAsync(BaseDataSpaceSetting baseDataSpaceSetting)
        {
            _localDataManager = baseDataSpaceSetting.LocalDataManager;
            _localConsumeRewardFactory = new();
            return true;
        }
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
    [TypeCache]
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

    public abstract class BaseCurrencyLocalConsumeReward : BaseLocalConsumeReward
    {
        public abstract override BaseReceiverProduct Consume(BaseRewardDefinition rewardDefinition);

        public override async UniTask SaveAsync()
        {
            var localCurrencySaveData = localDataManager.Get<LocalCurrencySaveData>();
            await localCurrencySaveData.SaveAsync();
        }
    }
    [TypeContain(typeof(BigIntCurrencyDefinition))]

    public class BigIntCurrencyLocalConsumeReward : BaseCurrencyLocalConsumeReward
    {
        public override BaseReceiverProduct Consume(BaseRewardDefinition rewardDefinition)
        {
            var currency = rewardDefinition as BaseCurrencyRewardDefinition;
            var localCurrencySaveData = localDataManager.Get<LocalCurrencySaveData>();
            _ = localCurrencySaveData.AddPlayerCurrency(currency.GetReferenceID(), currency.GetAmount(), false);
            return new BigIntCurrencyReceiverProduct(currency.GetReferenceID(), currency.GetAmount());
        }
    }
    [TypeContain(typeof(LongCurrencyDefinition))]
    public class LongCurrencyLocalConsumeReward : BaseCurrencyLocalConsumeReward
    {
        public override BaseReceiverProduct Consume(BaseRewardDefinition rewardDefinition)
        {
            var currency = rewardDefinition as BaseCurrencyRewardDefinition;
            var localCurrencySaveData = localDataManager.Get<LocalCurrencySaveData>();
            _ = localCurrencySaveData.AddPlayerCurrency(currency.GetReferenceID(), currency.GetAmount(), false);
            return new LongCurrencyReceiverProduct(currency.GetReferenceID(), currency.GetAmount());
        }
    }

    [TypeContain(typeof(StatRewardDefinition))]
    public class StatLocalConsumeReward : BaseLocalConsumeReward
    {
        public override BaseReceiverProduct Consume(BaseRewardDefinition rewardDefinition)
        {
            var localPropertySaveData = localDataManager.Get<LocalPropertySaveData>();
            var stat = rewardDefinition as StatRewardDefinition;
            _ = localPropertySaveData.SetPlayerPropertyAsync(stat.GetID(), stat.Amount.ToString(), false);
            return new StatReceiverProduct(stat.GetID(), stat.Amount);
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

    [TypeContain(typeof(TimedRewardDefinition))]
    public class TimedLocalConsumeReward : BaseLocalConsumeReward
    {
        public override BaseReceiverProduct Consume(BaseRewardDefinition rewardDefinition)
        {
            var localTimedSaveData = localDataManager.Get<LocalTimedSaveData>();
            var timed = rewardDefinition as TimedRewardDefinition;
            _ = localTimedSaveData.AddTimedAsync(timed.GetReferenceID(), timed.Amount, false);
            var playerTimed = localTimedSaveData.GetPlayerTimed(timed.GetID());
            return new TimedReceiverProduct(playerTimed.GetID(),playerTimed.Remain, playerTimed.StartTime);
        }

        public async override UniTask SaveAsync()
        {
            var localTimedSaveData = localDataManager.Get<LocalTimedSaveData>();
            await localTimedSaveData.SaveAsync(); 
        }
    }
}