using System.Linq;

namespace GameMaker.Core.Runtime
{
    public enum ConsumeType
    {
        Add= 0 ,
        Set = 1
    }
    public class StatReceiverProduct : BaseReceiverProduct
    {
        private long _value;
        private ConsumeType _consumeType;
        public long Value => _value;
        public ConsumeType ConsumeType => _consumeType;
        public StatReceiverProduct(string id, long value, ConsumeType consumeType) : base(id)
        {
            _value = value;
            _consumeType = consumeType;
        }
        public override void Consume(PlayerDataManager[] playerDataManagers, IExtendData extendData)
        {
           var playerPropertyDataManager = playerDataManagers.FirstOrDefault(x => x.GetType() == typeof(PlayerPropertyManager)) as PlayerPropertyManager;
            switch (ConsumeType)
                {
                    case ConsumeType.Add:
                        playerPropertyDataManager.AddStat(ID,Value);
                        RuntimeActionManager.Instance.NotifyAction(StatActionData.ADD_STAT_ACTION_DEFINITION, new StatActionData(ID,Value, extendData));
                        break;
                    case ConsumeType.Set:
                        playerPropertyDataManager.SetStat(ID,Value);
                        RuntimeActionManager.Instance.NotifyAction(StatActionData.SET_STAT_ACTION_DEFINITION, new StatActionData(ID,Value, extendData));
                        break;
                }
        }
    }
}