using System;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Feature.Shop.Runtime
{
    [System.Serializable]
    [TypeCache]
    public abstract class BaseCurrencyShopItemDefinition : BaseShopItemDefinition
    {
        public BaseCurrencyShopItemDefinition() : base()
        {

        }
        public BaseCurrencyShopItemDefinition(string id, string name, string title, string description,Sprite icon, BaseMetaData metaData, string referenceId, BasePrice price)
        :base(id, name, title, description, icon, metaData, referenceId, price)
        {
           
        }

        public override IDefinition GetDefinition()
        {
            return CurrencyManager.Instance.GetDefinition(GetReferenceID());
        }
    }
}