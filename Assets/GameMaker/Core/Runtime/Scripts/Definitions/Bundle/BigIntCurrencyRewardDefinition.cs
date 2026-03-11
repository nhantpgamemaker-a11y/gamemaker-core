using System;
using System.Numerics;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class BigIntCurrencyRewardDefinition : BaseCurrencyRewardDefinition
    {
        [UnityEngine.SerializeField]
        private string _amount;
        public BigIntCurrencyRewardDefinition() : base()
        {
            
        }
        public BigIntCurrencyRewardDefinition(string id, string name, string title, string description, Sprite icon, BaseMetaData metaData, string referenceId, string amount):
        base(id, name, title, description, icon, metaData, referenceId)
        {
            _amount = amount;
        }
        public override object Clone()
        {
            return new BigIntCurrencyRewardDefinition(GetID(), GetName(), GetTitle(), GetDescription(), GetIcon(), GetMetaData(), GetReferenceID(), _amount);
        }

        public override object GetAmount()
        {
            return BigInteger.Parse(_amount);
        }

        public override object GetNegativeAmount()
        {
            return BigInteger.Parse(_amount) * -1;
        }
    }
}