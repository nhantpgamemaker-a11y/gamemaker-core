using System.Collections.Generic;
using System.Linq;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Feature.Shop.Runtime
{
    [System.Serializable]
    public class Price: IReferenceDefinition
    {
        [UnityEngine.SerializeField]
        private string _currencyReferenceId;
        [UnityEngine.SerializeField]
        private float _amount;

        public string CurrencyReferenceId { get => _currencyReferenceId;}
        public float Amount { get => _amount; }
        public Price()
        {
            _currencyReferenceId = CurrencyManager.Instance.GetDefinitions().First().GetID();
        }
        public Price(string currencyId, float amount)
        {
            _currencyReferenceId = currencyId;
            _amount = amount;
        }

        public IDefinition GetDefinition()
        {
            return CurrencyManager.Instance.GetDefinition(_currencyReferenceId);
        }

        public string GetReferenceID()
        {
            return _currencyReferenceId;
        }
    }
}