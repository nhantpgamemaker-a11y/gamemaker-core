using System.Collections.Generic;
using System.Linq;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Feature.Shop.Runtime
{
    [System.Serializable]
    public abstract class BaseShopDefinition : BaseDefinition
    {
        [UnityEngine.SerializeField]
        private BaseDefinitionManager<BaseShopItemDefinition> _shopItemManager;
        public List<BaseShopItemDefinition> ShopItems => _shopItemManager.GetDefinitions();
        protected BaseDefinitionManager<BaseShopItemDefinition> ShopItemManager => _shopItemManager;
        public BaseShopDefinition(): base()
        {
            _shopItemManager = new();
        }
        public BaseShopDefinition(string id, string name, string title, string description, Sprite icon, BaseMetaData metaData, BaseDefinitionManager<BaseShopItemDefinition> shopItemManager)
        : base(id, name, title, description, icon, metaData)
        {
            _shopItemManager = shopItemManager;
        }
        public BaseShopItemDefinition GetShopItemDefinition(string id)
        {
            return _shopItemManager.GetDefinition(id);
        }
    }
}
