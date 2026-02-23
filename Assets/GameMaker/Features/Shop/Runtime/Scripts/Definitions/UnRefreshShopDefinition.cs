using System.Collections.Generic;
using System.Linq;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Feature.Shop.Runtime
{
    [System.Serializable]
    public class UnRefreshShopDefinition : BaseShopDefinition
    {
        public UnRefreshShopDefinition():base()
        {

        }
        public UnRefreshShopDefinition(string id, string name, string title, string description, Sprite icon, BaseMetaData metaData,  BaseDefinitionManager<BaseShopItemDefinition> shopItemManager)
        : base(id, name, title, description, icon, metaData, shopItemManager)
        {
            
        }
        
        public override object Clone()
        {
            return new UnRefreshShopDefinition(GetID(), GetName(), GetTitle(), GetDescription(), GetIcon(), GetMetaData(), ShopItemManager.Clone() as BaseDefinitionManager<BaseShopItemDefinition>);
        }
    }
}