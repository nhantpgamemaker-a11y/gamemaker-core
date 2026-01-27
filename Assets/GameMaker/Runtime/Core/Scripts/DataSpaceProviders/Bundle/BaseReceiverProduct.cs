namespace GameMaker.Core.Runtime
{
    public abstract class BaseReceiverProduct
    {
        private string _id;
        public string ID => _id;
        public BaseReceiverProduct(string id)
        {
            _id = id;
        }
    }
}