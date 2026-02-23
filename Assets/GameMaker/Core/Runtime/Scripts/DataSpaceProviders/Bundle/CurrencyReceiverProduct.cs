using System.Linq;
using Newtonsoft.Json;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class CurrencyReceiverProduct : BaseReceiverProduct
    {
        [JsonProperty("Value")]
        private string _value;
        [JsonIgnore]
        public string Value => _value;
        public CurrencyReceiverProduct(string id, string value) : base(id)
        {
            _value = value;
        }
        public override void Consume(PlayerDataManager[] playerDataManager, IExtendData extendData)
        {
            var playerCurrencyManager = playerDataManager.FirstOrDefault(x => x.GetType() == typeof(PlayerCurrencyManager)) as PlayerCurrencyManager;
            playerCurrencyManager.AddPlayerCurrency(ID, Value);
            RuntimeActionManager.Instance.NotifyAction(CurrencyActionData.ADD_CURRENCY_ACTION_DEFINITION, new CurrencyActionData(ID, Value, extendData));
        }

        public override object Clone()
        {
            return new  CurrencyReceiverProduct(ID, Value);
        }
    }
}