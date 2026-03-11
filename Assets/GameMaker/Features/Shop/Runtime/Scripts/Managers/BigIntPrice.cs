using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Feature.Shop.Runtime
{
    [System.Serializable]
    public class BigIntPrice : BasePrice
    {
        [UnityEngine.SerializeField]
        private string _amount;
        public BigIntPrice():base(){}
        public BigIntPrice(string currencyId, object amount): base(currencyId, amount)
        {
            _amount = amount.ToString();
        }

        public override object Clone()
        {
            return new BigIntPrice(CurrencyReferenceId, _amount);
        }

        public override object GetAmount()
        {
           return BigInteger.Parse(_amount);
        }

        public override BasePrice GetNegative()
        {
            var price = Clone() as BigIntPrice;
            BigInteger amount = BigInteger.Parse(price._amount);
            price._amount = (-amount).ToString();
            return price;
        }
    }
}