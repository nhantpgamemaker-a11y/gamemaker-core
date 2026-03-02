using System;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Feature.Shop.Runtime
{
    [System.Serializable]
    public class LongCurrencyShopItemDefinition : BaseCurrencyShopItemDefinition
    {
        [UnityEngine.SerializeField]
        private long _amount;
        public LongCurrencyShopItemDefinition() : base()
        {

        }
        public LongCurrencyShopItemDefinition(string id, string name, string title, string description,Sprite icon, BaseMetaData metaData, string referenceId, BasePrice price, long amount)
        :base(id, name, title, description, icon, metaData, referenceId, price)
        {
           _amount = amount;
        }

        public override object Clone()
        {
            return new LongCurrencyShopItemDefinition(GetID(), GetName(), GetTitle(), GetDescription(), GetIcon(), GetMetaData(),GetReferenceID(), Price.Clone() as BasePrice, _amount);
        }

        public override IDefinition GetDefinition()
        {
            return CurrencyManager.Instance.GetDefinition(GetReferenceID());
        }

        public override string GetStringAmount()
        {
            return _amount.ToString();
        }

        public object GetAmount()
        {
            return _amount;
        }
    }
}