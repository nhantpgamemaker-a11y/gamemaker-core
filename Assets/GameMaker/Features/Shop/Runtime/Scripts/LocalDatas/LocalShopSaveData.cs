using UnityEngine;
using GameMaker.Core.Runtime;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Reflection;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

namespace GameMaker.Feature.Shop.Runtime
{
    [System.Serializable]
    public class LocalShopSaveData : BaseLocalData
    {
        [JsonProperty("PlayerShops")]
        [UnityEngine.SerializeReference]
        private List<PlayerShopModel> _playerShops = new();
        public LocalShopSaveData()
        {

        }
        
        protected override void OnCreate()
        {
            base.OnCreate();
            foreach (var shopDefinition in ShopManager.Instance.GetDefinitions())
            {
                var config = shopDefinition.TimeResetConfig;
                long nextRefreshTime = string.IsNullOrWhiteSpace(config.CronExpression)
                    ? 0
                    : config.GetNextResetUtcTicks(TimeManager.Instance.UnixTimestamp);

                var playerShopModel = new PlayerShopModel(shopDefinition.GetID(),
                shopDefinition.GetName(),
                shopDefinition,
                nextRefreshTime);
                _playerShops.Add(playerShopModel);
            }
        }
        protected override void OnLoad()
        {
            base.OnLoad();
            var definitions = ShopManager.Instance.GetDefinitions();

            foreach (var shopDefinition in definitions)
            {
                var playerShopModel = _playerShops.FirstOrDefault(x => x.GetID() == shopDefinition.GetID());
                if (playerShopModel != null)
                {
                    playerShopModel.SetShopDefinition(shopDefinition);
                    playerShopModel.OnLoad();
                    continue;
                }
                else
                {
                    var config = shopDefinition.TimeResetConfig;
                    long nextRefreshTime = string.IsNullOrWhiteSpace(config.CronExpression)
                        ? 0
                        : config.GetNextResetUtcTicks(TimeManager.Instance.UnixTimestamp);

                    playerShopModel = new PlayerShopModel(shopDefinition.GetID(),
                    shopDefinition.GetName(),
                    shopDefinition,
                    nextRefreshTime);
                    playerShopModel.SetShopDefinition(shopDefinition);
                    playerShopModel.OnLoad();
                    _playerShops.Add(playerShopModel);
                }
            }
        }
        public async UniTask<PlayerShop> RefreshShopAsync(string shopDefinitionId, long lastRefreshTime, bool isSave = true)
        {
            var playerShop = _playerShops.FirstOrDefault(x => x.GetID() == shopDefinitionId);
            if (playerShop == null) return null;
            var shopDefinition = playerShop.GetShopDefinition();
            var config = shopDefinition.TimeResetConfig;
            if (config.CronExpression == string.Empty) return null;
            playerShop.Refresh(lastRefreshTime);

            if (isSave)
                await SaveAsync();
            return playerShop.ToPlayerShop();
        }
        public List<PlayerShop> GetPlayerShops()
        {
            return _playerShops.Where(x=>x.GetShopDefinition() !=null).Select(x => x.ToPlayerShop()).ToList();
        }

        public async UniTask PurchaseAsync(string shopDefinitionId, string shopItemId, bool v, bool isSave = true)
        {
            var playerShop = _playerShops.FirstOrDefault(x => x.GetID() == shopDefinitionId);
            if (playerShop == null) return;
            playerShop.Purchase(shopItemId, v);
            if (isSave)
                await SaveAsync();
        }
    }
    [System.Serializable]
    public class PlayerShopModel : PlayerDataModel
    {
        [JsonProperty("ShopDefinitionReferenceID")]
        [UnityEngine.SerializeField]        
        private string _shopDefinitionReferenceID;

        [JsonProperty("ShopItem")]
        [UnityEngine.SerializeReference]
        private List<BasePlayerShopItemModel> _shopItems = new();

        [JsonProperty("LastRefreshUTCTime")]
        private long _lastRefreshUTCTime;
        [JsonIgnore]
        private PlayerShopItemTypeFactory _playerShopItemTypeFactory = new();
        [JsonIgnore]
        protected List<BasePlayerShopItemModel> ShopItems => _shopItems;
        [JsonIgnore]
        protected ShopDefinition _shopDefinition;
        [JsonIgnore]
        public string ShopDefinitionReferenceID => _shopDefinitionReferenceID;
        [JsonIgnore]
        public long LastRefreshUTCTime => _lastRefreshUTCTime;
        
        public PlayerShopModel() : base()
        {

        }
        public  PlayerShopModel(string id, string name, ShopDefinition baseShopDefinition, long lastRefreshUTCTime) : base(id, name)
        {
            _lastRefreshUTCTime = lastRefreshUTCTime;
            _shopDefinitionReferenceID = baseShopDefinition.GetID();
            _shopDefinition = baseShopDefinition;
            foreach (var shopItem in baseShopDefinition.ShopItems)
            {
                var shopItemModelType = _playerShopItemTypeFactory.GetType(shopItem.GetType());
                var shopItemModel = Activator.CreateInstance(
                    shopItemModelType,
                    shopItem.GetID(),
                    shopItem.GetName(),
                    shopItem, true) as BasePlayerShopItemModel;
                _shopItems.Add(shopItemModel);
            }
        }
        public void SetShopDefinition(ShopDefinition shopDefinition)
        {
            _shopDefinition = shopDefinition;
        }
        public PlayerShop ToPlayerShop()
        {
            return new PlayerShop(id, _shopDefinition, _shopItems.Where(x=>x.GetShopItemDefinition() !=null).Select(x => x.ToPlayerShopItem()).ToList(), _lastRefreshUTCTime);
        }
        public ShopDefinition GetShopDefinition()
        {
            return ShopManager.Instance.GetDefinition(GetID());
        }

        public virtual void OnLoad()
        {
            var shopDefinition = GetShopDefinition();
            var definitionItems = shopDefinition.ShopItems;
            foreach (var shopItem in definitionItems)
            {
                var playerShopItemModel = _shopItems.FirstOrDefault(x => x.GetID() == shopItem.GetID());
                if (playerShopItemModel != null)
                {
                    playerShopItemModel.SetShopItemDefinition(shopItem);
                    continue;
                }
                else
                {
                    var shopItemModelType = _playerShopItemTypeFactory.GetType(shopItem.GetType());
                    var shopItemModel = Activator.CreateInstance(
                        shopItemModelType,
                        shopItem.GetID(),
                        shopItem.GetName(),
                        shopItem,
                        true) as BasePlayerShopItemModel;
                    shopItemModel.SetShopItemDefinition(shopItem);
                    _shopItems.Add(shopItemModel);
                }
            }
        }

        public void Refresh(long lastRefreshTime)
        {
            var shopDefinition = GetShopDefinition();
            var config = shopDefinition.TimeResetConfig;
            if (config.CronExpression == string.Empty) return;
            _lastRefreshUTCTime = lastRefreshTime;
            foreach (var item in _shopItems)
            {
                item.SetCanPurchase(true);
            }
        }

        public void Purchase(string shopItemId, bool v)
        {
            var playerShopItem = _shopItems.FirstOrDefault(x => x.GetID() == shopItemId);
            if (playerShopItem == null) return;
            playerShopItem.Purchase(v);
        }
    }

    public class PlayerShopItemTypeFactory
    {
        private Dictionary<Type, Type> _caches;
        public PlayerShopItemTypeFactory()
        {
            _caches = TypeUtils.GetAllDerivedNonAbstractTypes(typeof(BasePlayerShopItemModel))
            .Where(x => x.GetCustomAttribute<GameMaker.Core.Runtime.TypeContainAttribute>()!=null)
            .ToDictionary(x => x.GetCustomAttribute<GameMaker.Core.Runtime.TypeContainAttribute>().Type, x => x);
        
        }
        public Type GetType(Type playerShopType)
        {
            return _caches[playerShopType];
        }
    }

    [System.Serializable]
    [TypeCache]
    public abstract class BasePlayerShopItemModel : PlayerDataModel
    {
        [JsonProperty("CanPurchase")]
        private bool _canPurchase;
        [JsonProperty("ShopItemDefinitionReferenceID")]
        private string _shopItemDefinitionReferenceID;
        [JsonIgnore]
        private BaseShopItemDefinition _shopItemDefinition;
        [JsonIgnore]
        public bool CanPurchase => _canPurchase;
        [JsonIgnore]
        public string ShopItemDefinitionReferenceID => _shopItemDefinitionReferenceID;
        public BasePlayerShopItemModel(): base()
        {}
        public BasePlayerShopItemModel(string id, string name, BaseShopItemDefinition baseShopItemDefinition, bool canPurchase) : base(id, name)
        {
            _canPurchase = canPurchase;
            _shopItemDefinition = baseShopItemDefinition;
            _shopItemDefinitionReferenceID = baseShopItemDefinition.GetID();
        }
        public void SetShopItemDefinition(BaseShopItemDefinition shopItemDefinition)
        {
            _shopItemDefinition = shopItemDefinition;
        }
        public void SetCanPurchase(bool canPurchase)
        {
            _canPurchase = canPurchase;
        }
        public BaseShopItemDefinition GetShopItemDefinition()
        {
            return _shopItemDefinition;
        }

        public abstract BasePlayerShopItem ToPlayerShopItem();

        public void Purchase(bool v)
        {
            _canPurchase = v;
        }
    }
    [System.Serializable]
    [TypeCache]
    public abstract class CurrencyPlayerShopItemModel : BasePlayerShopItemModel
    {
        public CurrencyPlayerShopItemModel() : base()
        {

        }
        public CurrencyPlayerShopItemModel(string id, string name, BaseShopItemDefinition baseShopItemDefinition, bool canPurchase) 
        : base(id, name, baseShopItemDefinition, canPurchase)
        {
        }
    }
    [TypeContain(typeof(BigIntCurrencyShopItemDefinition))]
    public class BigIntCurrencyPlayerShopItemModel : CurrencyPlayerShopItemModel
    {
        public BigIntCurrencyPlayerShopItemModel():base()
        {

        }
        public BigIntCurrencyPlayerShopItemModel(string id, string name, BaseShopItemDefinition baseShopItemDefinition, bool canPurchase) : base(id, name, baseShopItemDefinition, canPurchase)
        {
        }
        override public BasePlayerShopItem ToPlayerShopItem()
        {
            return new BigIntCurrencyPlayerShopItem(id, name, GetShopItemDefinition() as IDefinition ,CanPurchase);
        }
    }
    [TypeContain(typeof(LongCurrencyShopItemDefinition))]
    public class LongCurrencyPlayerShopItemModel : CurrencyPlayerShopItemModel
    {
        public LongCurrencyPlayerShopItemModel() : base()
        {

        }
        public LongCurrencyPlayerShopItemModel(string id, string name, BaseShopItemDefinition baseShopItemDefinition, bool canPurchase) : base(id, name, baseShopItemDefinition, canPurchase)
        {
        }

        public override BasePlayerShopItem ToPlayerShopItem()
        {
            return new LongCurrencyPlayerShopItem(id, name, GetShopItemDefinition() as IDefinition ,CanPurchase);
        }
    }
    [System.Serializable]
    [TypeContain(typeof(ItemShopItemDefinition))]
    public class ItemPlayerShopItemModel : BasePlayerShopItemModel
    {
        public ItemPlayerShopItemModel() : base()
        {

        }
        public ItemPlayerShopItemModel(string id, string name, BaseShopItemDefinition baseShopItemDefinition, bool canPurchase) : base(id, name, baseShopItemDefinition, canPurchase)
        {
        }

        public override BasePlayerShopItem ToPlayerShopItem()
        {
            return new ItemPlayerShopItem(id, name, GetShopItemDefinition() as IDefinition ,CanPurchase);
        }
    }
}
