using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public abstract class BaseShopDefinition : BaseDefinition
    {
        [UnityEngine.SerializeField]
        private List<BaseShopItemDefinition> _shopItems = new();
        public List<BaseShopItemDefinition> ShopItems => _shopItems;
        public BaseShopDefinition(): base()
        {
            _shopItems = new();
        }
        public BaseShopDefinition(string id, string name, string title, string description, Sprite icon, BaseMetaData metaData,List<BaseShopItemDefinition> shopItems)
        :base(id, name, title, description, icon, metaData)
        {
            _shopItems = shopItems;
        }
    }
}
