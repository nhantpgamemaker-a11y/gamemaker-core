using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;
using System.Linq;
using System;
using System.Reflection;

namespace GameMaker.IAP.Runtime
{
    [DataSpace(nameof(LocalDataSpaceSetting.LOCAL_SPACE))]
    public class LocalIAPDataSpaceProvider : BaseIAPDataSpaceProvider
    {
        private LocalIAPSaveData _localIAPSaveData = null;
        private LocalDataManager _localDataManager;

        private LocalConsumeRewardFactory _localConsumeRewardFactory;
        private LocalRecallRewardFactory _localRecallRewardFactory;

        public async override UniTask<bool> InitAsync(BaseDataSpaceSetting baseDataSpaceSetting)
        {
            _localDataManager = (baseDataSpaceSetting as LocalDataSpaceSetting).LocalDataManager;
            _localIAPSaveData = (baseDataSpaceSetting as LocalDataSpaceSetting).LocalDataManager.Get<LocalIAPSaveData>();
            _localConsumeRewardFactory = new();
            _localRecallRewardFactory = new();
            return true;
        }

        public async override UniTask<(bool status, List<PlayerIAP>)> GetPlayerIAPs()
        {
            return (true, new());
        }

        public async override UniTask<bool> MarkActiveAsync(List<(string productIds, string transactionIds)> confirmedOrders)
        {
            return true;
        }

        public async override UniTask<(bool, List<BaseReceiverProduct>)> PurchaseAsync(PlayerIAP playerIAP)
        {
            var receiverProducts = new List<BaseReceiverProduct>();
            var bundleDefinition = (playerIAP.GetDefinition() as IAPDefinition).Reward;
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

        public async override UniTask<(bool, List<BaseReceiverProduct>)> RecallPlayerIAPsAsync(List<string> transactionIds)
        {
            var receiverProducts = new List<BaseReceiverProduct>();
            foreach (var transactionId in transactionIds)
            {
                var playerIAP = _localIAPSaveData.GetPlayerIAPByTransactionId(transactionId);

                var bundleDefinition = (playerIAP.GetDefinition() as IAPDefinition).Reward;
                var rewards = bundleDefinition.Rewards;
                var consumers = rewards
                                .Select(x => x.GetType())
                                .Select(x => _localRecallRewardFactory.GetLocalConsumerReward(x))
                                .Distinct();

                foreach (var reward in rewards)
                {
                    var consumer = _localRecallRewardFactory.GetLocalConsumerReward(reward.GetType());
                    consumer.InitLocalDataManager(_localDataManager);
                    var data = consumer.Recall(reward);
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
            }
            return (true, receiverProducts);
            
        }
    }
    public class LocalRecallRewardFactory
    {
        /// <summary>
        /// type_1 is LocalConsumeRewardType
        /// type_2 is RewardDefinition
        /// </summary>
        private Dictionary<Type, Type> _cache;
        private Dictionary<Type, BaseLocalRecallReward> _localRecallRewardDict;
        public LocalRecallRewardFactory()
        {
            _cache = new();
            var baseLocalRecallRewardTypes =
            TypeUtils.GetAllConcreteDerivedTypes(typeof(BaseLocalRecallReward))
            .Where(x =>
            {
                return x.GetCustomAttribute<TypeContainAttribute>() != null;
            });

            foreach (var baseLocalRecallRewardType in baseLocalRecallRewardTypes)
            {
                var type = baseLocalRecallRewardType
                            .GetCustomAttribute<TypeContainAttribute>().Type;
                _cache[type] = baseLocalRecallRewardType;
            }
            _localRecallRewardDict = new();
        }

        public Type GetLocalConsumeRewardType(Type type)
        {
            return _cache[type];
        }
        public BaseLocalRecallReward GetLocalConsumerReward(Type type)
        {
            var localRecallRewardType = GetLocalConsumeRewardType(type);
            if (!_localRecallRewardDict.TryGetValue(localRecallRewardType, out BaseLocalRecallReward baseLocalConsumeReward))
            {
                _localRecallRewardDict[type] = Activator.CreateInstance(localRecallRewardType) as BaseLocalRecallReward;
            }
            return baseLocalConsumeReward;
        }
    }
    [TypeCache]
    public abstract class BaseLocalRecallReward
    {
        protected LocalDataManager localDataManager;
        public void InitLocalDataManager(LocalDataManager localDataManager)
        {
            this.localDataManager = localDataManager;
        }
        public abstract BaseReceiverProduct Recall(BaseRewardDefinition rewardDefinition);
        public abstract UniTask SaveAsync();
    }
    [TypeContain(typeof(CurrencyRewardDefinition))]
    public class CurrencyLocalRecallReward : BaseLocalRecallReward
    {
        public override BaseReceiverProduct Recall(BaseRewardDefinition rewardDefinition)
        {
            var currency = rewardDefinition as CurrencyRewardDefinition;
            var localCurrencySaveData = localDataManager.Get<LocalCurrencySaveData>();
            _ = localCurrencySaveData.AddPlayerCurrency(currency.GetReferenceID(), $"-{currency.Amount}", false);
            return new CurrencyReceiverProduct(currency.GetReferenceID(), $"-{currency.Amount}");
        }

        public async override UniTask SaveAsync()
        {
            var localCurrencySaveData = localDataManager.Get<LocalCurrencySaveData>();
            await localCurrencySaveData.SaveAsync();
        }
    }
    [TypeContain(typeof(StatRewardDefinition))]
    public class StatLocalRecallReward : BaseLocalRecallReward
    {
        public override BaseReceiverProduct Recall(BaseRewardDefinition rewardDefinition)
        {
            var localPropertySaveData = localDataManager.Get<LocalPropertySaveData>();
            var stat = rewardDefinition as StatRewardDefinition;
            _ = localPropertySaveData.SetPlayerPropertyAsync(stat.GetID(), (stat.GetDefinition() as StatDefinition).DefaultValue.ToString(), false);
            return new StatReceiverProduct(stat.GetID(),(stat.GetDefinition() as StatDefinition).DefaultValue);
        }

        public override async UniTask SaveAsync()
        {
            var localPropertySaveData = localDataManager.Get<LocalPropertySaveData>();
            await localPropertySaveData.SaveAsync();
        }
    }
    [TypeContain(typeof(ItemRewardDefinition))]
    public class ItemLocalRecallReward : BaseLocalRecallReward
    {
        public override BaseReceiverProduct Recall(BaseRewardDefinition rewardDefinition)
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