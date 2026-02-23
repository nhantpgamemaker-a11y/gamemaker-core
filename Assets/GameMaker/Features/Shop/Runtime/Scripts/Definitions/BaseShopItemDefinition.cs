using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Feature.Shop.Runtime
{
    [System.Serializable]
    public abstract class BaseShopItemDefinition : BaseDefinition, IReferenceDefinition
    {
        [UnityEngine.SerializeField]
        private string _referenceId;
        [UnityEngine.SerializeField]
        private Price _price;
        [UnityEngine.SerializeField]
        private float _amount;
        public Price Price => _price;
        
        public float Amount => _amount;
        public BaseShopItemDefinition() : base() { }

        public BaseShopItemDefinition(string id, string name, string title, string description, Sprite icon, BaseMetaData metaData, string referenceId, Price price,float amount) :
        base(id, name, title, description, icon, metaData)
        {
            _referenceId = referenceId;
            _price = price;
            _amount = amount;
        }
        
        public override Sprite GetIcon()
        {
            var icon = base.GetIcon();
            if (icon == null)
            {
                icon = (GetDefinition() as BaseDefinition).GetIcon();
            }
            return icon;
        }
    
        public abstract IDefinition GetDefinition();

        public string GetReferenceID()
        {
            return _referenceId;
        }
    }
}