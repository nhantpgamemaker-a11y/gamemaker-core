using System;
using System.Numerics;
using Newtonsoft.Json;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class BigIntPlayerCurrency : BasePlayerCurrency
    {
        [JsonProperty("Value")]
        private BigInteger _value;
        public BigIntPlayerCurrency(string id, IDefinition definition, BigInteger value) : base(id, definition)
        {
            _value = value;
        }

        public override void AddValue(object value)
        {
            _value += (BigInteger)value;
            NotifyObserver(this);
        }

        public override object Clone()
        {
            return new BigIntPlayerCurrency(GetID(), definition, _value);
        }

        public override object GetValue()
        {
            return _value;
        }
    }
}