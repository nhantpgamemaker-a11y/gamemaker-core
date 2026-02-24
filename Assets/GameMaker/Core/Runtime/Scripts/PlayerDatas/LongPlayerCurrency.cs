using Newtonsoft.Json;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class LongPlayerCurrency : BasePlayerCurrency
    {
        [JsonProperty("Value")]
        private long _value;
        public LongPlayerCurrency(string id, IDefinition definition, long value) : base(id, definition)
        {
            _value = value;
        }

        public override void AddValue(object value)
        {
            _value += (long)value;
        }

        public override object Clone()
        {
            return new LongPlayerCurrency(GetID(), definition, _value);
        }

        public long GetValue()
        {
            return _value;
        }
    }
}