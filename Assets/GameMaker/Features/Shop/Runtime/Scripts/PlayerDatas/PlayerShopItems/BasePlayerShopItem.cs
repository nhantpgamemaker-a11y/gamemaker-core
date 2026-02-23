using GameMaker.Core.Runtime;

namespace GameMaker.Feature.Shop.Runtime
{
    [System.Serializable]
    public abstract class BasePlayerShopItem : BasePlayerData,IDefinition
    {
        [UnityEngine.SerializeField]
        private string _name;
        [UnityEngine.SerializeField]
        private float _remain;
        public float Remain => _remain;
        protected BasePlayerShopItem(string id, string name ,IDefinition definition, float remain) : base(id,definition)
        {
            _name = name;
            _remain = remain;
        }
        public BaseShopItemDefinition GetBaseShopItemDefinition()
        {
            return definition as BaseShopItemDefinition;
        }
        public void AddRemain(float amount)
        {
            _remain += amount;
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