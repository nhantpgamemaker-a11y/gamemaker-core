using UnityEngine;
using GameMaker.Core.Runtime;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Reflection;

namespace GameMaker.Feature.Shop.Runtime
{
    [System.Serializable]
    public class LocalShopSaveData : BaseLocalData
    {
        [JsonProperty("PlayerShops")]
        [UnityEngine.SerializeReference]
        private List<BasePlayerShopModel> _playerShops = new();
        [JsonIgnore]
        private PlayerShopTypeFactory _playerShopFactory = new();
        public LocalShopSaveData()
        {

        }
        
        protected override void OnCreate()
        {
            base.OnCreate();
            foreach (var shopDefinition in ShopManager.Instance.GetDefinitions())
            {
                var playerShopModelType = _playerShopFactory.GetType();
                var playerShopModel = Activator.CreateInstance( playerShopModelType,
                                                                shopDefinition.GetID(),
                                                                shopDefinition.GetName(),
                                                                shopDefinition) as BasePlayerShopModel;
                _playerShops.Add(playerShopModel);
            }
        }
        protected override void OnLoad()
        {
            base.OnLoad();
            var definitions = ShopManager.Instance.GetDefinitions();

            var existedShopIds = _playerShops
                .Select(x => x.GetID())
                .ToHashSet();

            foreach (var shopDefinition in definitions)
            {
                var shopId = shopDefinition.GetID();
                if (existedShopIds.Contains(shopId))
                    continue;
                var playerShopModelType = _playerShopFactory.GetType();
                var playerShopModel = Activator.CreateInstance(
                    playerShopModelType,
                    shopDefinition.GetID(),
                    shopDefinition.GetName(),
                    shopDefinition
                ) as BasePlayerShopModel;

                _playerShops.Add(playerShopModel);
            }

             foreach(var shop in _playerShops)
            {
                var shopDefinition = ShopManager.Instance.GetDefinition(shop.ShopDefinitionReferenceID);
                shop.SetShopDefinition(shopDefinition);
                shop.OnLoad();
            }
        }
    }
    public class PlayerShopTypeFactory
    {
        private Dictionary<Type, Type> _caches;
        public PlayerShopTypeFactory()
        {
            _caches = TypeUtils.GetAllDerivedNonAbstractTypes(typeof(BasePlayerShopModel))
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
    public abstract class BasePlayerShopModel : PlayerDataModel
    {
        [JsonProperty("ShopDefinitionReferenceID")]
        [UnityEngine.SerializeField]        
        private string _shopDefinitionReferenceID;

        [JsonProperty("ShopItem")]
        [UnityEngine.SerializeReference]
        private List<BasePlayerShopItemModel> _shopItems = new();
        [JsonIgnore]
        private PlayerShopItemTypeFactory _playerShopItemTypeFactory = new();
        [JsonIgnore]
        protected List<BasePlayerShopItemModel> ShopItems => _shopItems;
        [JsonIgnore]
        protected BaseShopDefinition _shopDefinition;
        [JsonIgnore]
        public string ShopDefinitionReferenceID => _shopDefinitionReferenceID;
        public BasePlayerShopModel(string id, string name, BaseShopDefinition baseShopDefinition) : base(id, name)
        {
            _shopDefinitionReferenceID = baseShopDefinition.GetID();
            _shopDefinition = baseShopDefinition;
            foreach (var shopItem in baseShopDefinition.ShopItems)
            {
                var shopItemModelType = _playerShopItemTypeFactory.GetType(shopItem.GetType());
                var shopItemModel = Activator.CreateInstance(shopItemModelType, shopItem.GetID(), shopItem.GetName(), shopItem) as BasePlayerShopItemModel;
                _shopItems.Add(shopItemModel);
            }
        }
        public void SetShopDefinition(BaseShopDefinition shopDefinition)
        {
            _shopDefinition = shopDefinition;
        }
        public abstract BasePlayerShop ToPlayerShop();
        public BaseShopDefinition GetShopDefinition()
        {
            return ShopManager.Instance.GetDefinition(GetID());
        }
        public virtual void OnLoad()
        {
            var shopDefinition = GetShopDefinition();
            var definitionItems = shopDefinition.ShopItems;
            var existedItemIds = _shopItems
                .Select(x => x.GetID())
                .ToHashSet();

            foreach (var shopItem in definitionItems)
            {
                var itemId = shopItem.GetID();

                if (existedItemIds.Contains(itemId))
                    continue;

                var shopItemModelType = _playerShopItemTypeFactory.GetType(shopItem.GetType());
                var shopItemModel = Activator.CreateInstance(
                    shopItemModelType,
                    shopItem.GetID(),
                    shopItem.GetName(),
                    shopItem
                ) as BasePlayerShopItemModel;

                _shopItems.Add(shopItemModel);
            }

            foreach(var shopItem in _shopItems)
            {
                var shopItemDefinition = shopDefinition.GetShopItemDefinition(shopItem.ShopItemDefinitionReferenceID);
                shopItem.SetShopItemDefinition(shopItemDefinition);
            }
        }
    }

    [System.Serializable]
    [TypeContain(typeof(UnRefreshShopDefinition))]
    public class UnRefreshPlayerShopModel : BasePlayerShopModel
    {
        public UnRefreshPlayerShopModel(string id, string name, BaseShopDefinition baseShopDefinition) : base(id, name, baseShopDefinition)
        {
        }

        public override BasePlayerShop ToPlayerShop()
        {
            var shopDefinition = GetShopDefinition();
            return new UnRefreshPlayerShop(shopDefinition.GetID(), shopDefinition,ShopItems.Select(x=>x.ToPlayerShopItem()).ToList());
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
        [JsonProperty("Remain")]
        private float _remain;
        [JsonProperty("ShopItemDefinitionReferenceID")]
        private string _shopItemDefinitionReferenceID;
        [JsonIgnore]
        private BaseShopItemDefinition _shopItemDefinition;
        [JsonIgnore]
        public float Remain => _remain;
        [JsonIgnore]
        public string ShopItemDefinitionReferenceID => _shopItemDefinitionReferenceID;
        public BasePlayerShopItemModel(string id, string name, BaseShopItemDefinition baseShopItemDefinition) : base(id, name)
        {
            _remain = baseShopItemDefinition.Amount;
            _shopItemDefinition = baseShopItemDefinition;
            _shopItemDefinitionReferenceID = baseShopItemDefinition.GetID();
        }
        public void SetShopItemDefinition(BaseShopItemDefinition shopItemDefinition)
        {
            _shopItemDefinition = shopItemDefinition;
        }
        public void AddRemain(float amount)
        {
            _remain += amount;
        }
        public BaseShopItemDefinition GetShopItemDefinition()
        {
            return _shopItemDefinition;
        }

        public abstract BasePlayerShopItem ToPlayerShopItem();
    }
    [System.Serializable]
    [TypeContain(typeof(CurrencyShopItemDefinition))]
    public class CurrencyPlayerShopItemModel : BasePlayerShopItemModel
    {
        public CurrencyPlayerShopItemModel(string id, string name,BaseShopItemDefinition baseShopItemDefinition) : base(id, name,baseShopItemDefinition)
        {
        }

        public override BasePlayerShopItem ToPlayerShopItem()
        {
            return new CurrencyPlayerShopItem(id, name, GetShopItemDefinition() as IDefinition ,Remain);
        }
    }
    [System.Serializable]
    [TypeContain(typeof(ItemShopItemDefinition))]
    public class ItemPlayerShopItemModel : BasePlayerShopItemModel
    {
        public ItemPlayerShopItemModel(string id, string name, BaseShopItemDefinition baseShopItemDefinition) : base(id, name, baseShopItemDefinition)
        {
        }

        public override BasePlayerShopItem ToPlayerShopItem()
        {
            return new ItemPlayerShopItem(id, name, GetShopItemDefinition() as IDefinition ,Remain);
        }
    }
}
