using GameMaker.Core.Runtime;

namespace GameMaker.Feature.Shop.Runtime
{
    [System.Serializable]
    public abstract class BasePlayerShopItem : BasePlayerData,IDefinition
    {
        [UnityEngine.SerializeField]
        private string _name;
        [UnityEngine.SerializeField]
        private bool _canPurchase = true;
        public bool CanPurchase => _canPurchase;
        protected BasePlayerShopItem(string id, string name ,IDefinition definition, bool canPurchase) : base(id,definition)
        {
            _name = name;
            _canPurchase = canPurchase;
        }
        public BaseShopItemDefinition GetBaseShopItemDefinition()
        {
            return definition as BaseShopItemDefinition;
        }
        public void SetCanPurchase(bool status)
        {
            _canPurchase = status;
            NotifyObserver(this);
        }
        
        public string GetName()
        {
            return _name;
        }

        public bool Equals(IDefinition other)
        {
            return other.GetID() == GetID();
        }
    }
}