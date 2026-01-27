namespace GameMaker.Core.Runtime
{
    public class StatReceiverProduct : BaseReceiverProduct
    {
        private long _value;
        private ConsumeType _consumeType;
        public long Value => _value;
        public ConsumeType ConsumeType => _consumeType;
        public StatReceiverProduct(string id, long value,ConsumeType consumeType) : base(id)
        {
            _value = value;
            _consumeType = consumeType;
        }
    }
}