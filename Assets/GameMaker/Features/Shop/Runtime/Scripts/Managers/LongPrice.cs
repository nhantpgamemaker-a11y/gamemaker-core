using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Feature.Shop.Runtime
{
    [System.Serializable]
    public class LongPrice : BasePrice
    {
        [UnityEngine.SerializeField]
        private long _amount;
        public LongPrice():base(){}
        public LongPrice(string currencyId, object amount): base(currencyId, amount)
        {
            _amount = System.Convert.ToInt64(amount);
        }

        public override object Clone()
        {
            return new LongPrice(CurrencyReferenceId, _amount);
        }

        public override object GetAmount()
        {
           return _amount;
        }

        public override BasePrice GetNegative()
        {
            var price = Clone() as LongPrice;
            price._amount = -price._amount;
            return price;
        }
    }
}