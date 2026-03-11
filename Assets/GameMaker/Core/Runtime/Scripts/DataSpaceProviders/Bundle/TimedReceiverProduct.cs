using System.Linq;
using Newtonsoft.Json;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class TimedReceiverProduct : BaseReceiverProduct
    {
        [JsonProperty("Remain")]
        private long _remain;

        [JsonProperty("StartTime")]
        private long _startTime;
        [JsonIgnore]
        public long Value => _remain;
        [JsonIgnore]
        public long StartTime => _startTime;
        public TimedReceiverProduct(string id, long value,long startTime) : base(id)
        {
            _remain = value;
        }
        public override void Consume(PlayerDataManager[] playerDataManager, IExtendData extendData)
        {
            var playerTimedManager = playerDataManager.FirstOrDefault(x => x.GetType() == typeof(PlayerTimedManager)) as PlayerTimedManager;
            playerTimedManager.CopyFrom(new PlayerTimed(ID,TimedManager.Instance.GetDefinition(ID) ,_remain, _startTime));
            RuntimeActionManager.Instance.NotifyAction(TimedActionData.ADD_TIMED_ACTION_DEFINITION, new TimedActionData(ID, Value, extendData));
        }

        public override object Clone()
        {
            return new  TimedReceiverProduct(ID, _remain,_startTime);
        }
    }
}