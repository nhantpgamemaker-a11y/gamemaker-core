using System;
using System.Collections.Generic;
using System.Linq;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Feature.Shop.Runtime
{
    [System.Serializable]
    [TypeCache]
    public abstract class BasePrice:ICloneable
    {
        [UnityEngine.SerializeField]
        private string _currencyReferenceId;
        public string CurrencyReferenceId { get => _currencyReferenceId;}
        public abstract object GetAmount();
        public BasePrice()
        {
           
        }
        public BasePrice(string currencyId, object amount)
        {
            _currencyReferenceId = currencyId;
        }

        public virtual BaseCurrencyDefinition GetCurrencyDefinition()
        {
            return CurrencyManager.Instance.GetDefinition(_currencyReferenceId);
        }

        public abstract object Clone();

        public abstract BasePrice GetNegative();
    }
}