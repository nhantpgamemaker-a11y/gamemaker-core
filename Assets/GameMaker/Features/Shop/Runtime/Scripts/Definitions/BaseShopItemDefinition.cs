using System;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Feature.Shop.Runtime
{
    [System.Serializable]
    [TypeCache]
    public abstract class BaseShopItemDefinition : BaseDefinition, IReferenceDefinition
    {
        [UnityEngine.SerializeField]
        private string _referenceId;
        [UnityEngine.SerializeReference]
        private BasePrice _price;
        public BasePrice Price => _price;
        public BaseShopItemDefinition() : base() { }

        public BaseShopItemDefinition(string id, string name, string title, string description, Sprite icon, BaseMetaData metaData, string referenceId, BasePrice price) :
        base(id, name, title, description, icon, metaData)
        {
            _referenceId = referenceId;
            _price = price;
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
        
        public Type GetTypeOfPrice()
        {
            return _price.GetType();
        }

        public abstract string GetStringAmount();
        
    }
}