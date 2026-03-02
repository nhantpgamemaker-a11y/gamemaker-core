using System;
using System.Numerics;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Feature.Shop.Runtime
{
    [System.Serializable]
    public class BigIntCurrencyShopItemDefinition : BaseCurrencyShopItemDefinition
    {
        [UnityEngine.SerializeField]
        private string _amount;
        public BigIntCurrencyShopItemDefinition() : base()
        {

        }
        public BigIntCurrencyShopItemDefinition(string id, string name, string title, string description,Sprite icon, BaseMetaData metaData, string referenceId, BasePrice price, string amount)
        :base(id, name, title, description, icon, metaData, referenceId, price)
        {
           _amount = amount;
        }

        public override object Clone()
        {
            return new BigIntCurrencyShopItemDefinition(GetID(), GetName(), GetTitle(), GetDescription(), GetIcon(), GetMetaData(),GetReferenceID(), Price.Clone() as BasePrice, _amount);
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
            return BigInteger.Parse(_amount);
        }
    }
}