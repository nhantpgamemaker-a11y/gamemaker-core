using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class CurrencyActionData : BaseActionData
    {
        private string _currencyId;
        private float _value;
        public string CurrencyId { get => _currencyId; }
        public float Value { get => _value; }
        public  CurrencyActionData(string currencyId, float value, object data = null): base(data)
        {
            _currencyId = currencyId;
            _value = value;
        }

        

        public override IDefinition GetDefinition()
        {
            return CurrencyManager.Instance.GetDefinition(_currencyId);
        }
    }
}