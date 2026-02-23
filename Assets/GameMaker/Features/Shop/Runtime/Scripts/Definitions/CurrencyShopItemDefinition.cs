using System;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Feature.Shop.Runtime
{
    [System.Serializable]
    public class CurrencyShopItemDefinition : BaseShopItemDefinition
    {
        
        public CurrencyShopItemDefinition() : base()
        {

        }
        public CurrencyShopItemDefinition(string id, string name, string title, string description,Sprite icon, BaseMetaData metaData, string referenceId, Price price,float amount)
        :base(id, name, title, description, icon, metaData, referenceId, price,amount)
        {
           
        }
        public override object Clone()
        {
            return new CurrencyShopItemDefinition(GetID(), GetName(), GetTitle(), GetReferenceID(), GetIcon(), GetMetaData(), GetReferenceID(), Price, Amount);
        }

        public override IDefinition GetDefinition()
        {
            return CurrencyManager.Instance.GetDefinition(GetReferenceID());
        }
    }
}