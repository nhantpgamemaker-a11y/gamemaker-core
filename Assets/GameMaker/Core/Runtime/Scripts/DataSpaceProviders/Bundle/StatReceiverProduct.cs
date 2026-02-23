using System.Linq;
using Newtonsoft.Json;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class StatReceiverProduct : BaseReceiverProduct
    {
        [JsonProperty("Value")]
        private float _value;
        [JsonIgnore]
        public float Value => _value;
        public StatReceiverProduct(string id, float value) : base(id)
        {
            _value = value;
        }
        public override void Consume(PlayerDataManager[] playerDataManagers, IExtendData extendData)
        {
            var playerPropertyDataManager = playerDataManagers.FirstOrDefault(x => x.GetType() == typeof(PlayerPropertyManager)) as PlayerPropertyManager;
            playerPropertyDataManager.Set(ID,Value.ToString());
                        RuntimeActionManager.Instance.NotifyAction(PropertyActionData.SET_PROPERTY_ACTION_DEFINITION, new PropertyActionData(ID,Value.ToString(), extendData));
                        
        }

        public override object Clone()
        {
            return new StatReceiverProduct(ID, Value);
        }
    }
}