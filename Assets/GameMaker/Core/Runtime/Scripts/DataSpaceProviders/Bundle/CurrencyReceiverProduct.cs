using System.Linq;
using System.Numerics;
using Newtonsoft.Json;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public abstract class CurrencyReceiverProduct : BaseReceiverProduct
    {
        public CurrencyReceiverProduct(string id, object value) : base(id)
        {
        }
        public override void Consume(PlayerDataManager[] playerDataManager, IExtendData extendData)
        {
            var playerCurrencyManager = playerDataManager.FirstOrDefault(x => x.GetType() == typeof(PlayerCurrencyManager)) as PlayerCurrencyManager;
            playerCurrencyManager.AddPlayerCurrency(ID, GetValue());
            RuntimeActionManager.Instance.NotifyAction(CurrencyActionData.ADD_CURRENCY_ACTION_DEFINITION, new CurrencyActionData(ID, GetValue(), extendData));
        }
        public abstract object GetValue();
    }
    [System.Serializable]
    public class LongCurrencyReceiverProduct : CurrencyReceiverProduct
    {
        [JsonProperty("Value")]
        private long _value;
        public LongCurrencyReceiverProduct(string id, object value) : base(id, value)
        {
            _value = (long)value;
        }

        public override object Clone()
        {
            return new LongCurrencyReceiverProduct(ID, _value);
        }

        public override object GetValue()
        {
            return _value;
        }
    }
    [System.Serializable]
    public class BigIntCurrencyReceiverProduct : CurrencyReceiverProduct
    {
        [JsonProperty("Value")]
        private string _value;
        public BigIntCurrencyReceiverProduct(string id, object value) : base(id, value)
        {
            _value = (string)value;
        }

        public override object Clone()
        {
            return new BigIntCurrencyReceiverProduct(ID, _value);
        }

        public override object GetValue()
        {
            return BigInteger.Parse(_value);
        }
    }
}