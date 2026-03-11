using System.Collections.Generic;
using System.Linq;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Feature.Shop.Runtime
{
    [System.Serializable]
    public  class ShopDefinition : BaseDefinition
    {
        [UnityEngine.SerializeField]
        private TimeResetConfig _timeResetConfig;
        public TimeResetConfig TimeResetConfig => _timeResetConfig;

        [UnityEngine.SerializeField]
        private BaseDefinitionManager<BaseShopItemDefinition> _shopItemManager;
        public List<BaseShopItemDefinition> ShopItems => _shopItemManager.GetDefinitions();
        protected BaseDefinitionManager<BaseShopItemDefinition> ShopItemManager => _shopItemManager;
        public ShopDefinition(): base()
        {
            _timeResetConfig = new();
            _shopItemManager = new();
        }
        public ShopDefinition(string id, string name, string title,
         string description, Sprite icon, BaseMetaData metaData,
          BaseDefinitionManager<BaseShopItemDefinition> shopItemManager,
          TimeResetConfig timeResetConfig)
        : base(id, name, title, description, icon, metaData)
        {
            _shopItemManager = shopItemManager;
            _timeResetConfig = timeResetConfig;
        }
        public BaseShopItemDefinition GetShopItemDefinition(string id)
        {
            return _shopItemManager.GetDefinition(id);
        }

        public override object Clone()
        {
            return new ShopDefinition(
            GetID(),
            GetName(),
            GetTitle(),
            GetDescription(),
            GetIcon(),
            GetMetaData(),
            _shopItemManager.Clone() as BaseDefinitionManager<BaseShopItemDefinition>,
            _timeResetConfig.Clone() as TimeResetConfig);
        }
    }
}
