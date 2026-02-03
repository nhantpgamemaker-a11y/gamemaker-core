using System.Linq;

namespace GameMaker.Core.Runtime
{
    public class CurrencyReceiverProduct : BaseReceiverProduct
    {
        private float _value;
        public float Value => _value;
        public CurrencyReceiverProduct(string id, float value) : base(id)
        {
            _value = value;
        }
        public override void Consume(PlayerDataManager[] playerDataManager, IExtendData extendData)
        {
            var playerCurrencyManager = playerDataManager.FirstOrDefault(x => x.GetType() == typeof(PlayerCurrencyManager)) as PlayerCurrencyManager;
            playerCurrencyManager.AddPlayerCurrency(ID, Value);
            RuntimeActionManager.Instance.NotifyAction(StatActionData.SET_STAT_ACTION_DEFINITION, new StatActionData(ID, Value, extendData));
        }
    }
}