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
    }
}