
using System;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
     [System.Serializable]
    public sealed class ItemRewardDefinition : BaseRewardDefinition
    {
        [UnityEngine.SerializeField]
        private float _amount;
        [UnityEngine.SerializeField]
        private BaseCreateItemTemplate _createItemTemplate;

        public float Amount => _amount;
        public BaseCreateItemTemplate CreateItemTemplate => _createItemTemplate;
        public ItemRewardDefinition():base(){}
        public ItemRewardDefinition(string id,string name, string title, string definition, Sprite icon,BaseMetaData metaData,string referenceId, float amount, BaseCreateItemTemplate createItemTemplate): base(id, name, title, definition, icon,metaData ,referenceId)
        {
            _amount = amount;
            _createItemTemplate = createItemTemplate;
        }

        public override object Clone()
        {
            return new ItemRewardDefinition(GetID(),GetName(),GetTitle(), GetDescription(), GetIcon(), GetMetaData(), GetReferenceID(),_amount,_createItemTemplate);
        }

        public override IDefinition GetDefinition()
        {
            return ItemDetailManager.Instance.GetDefinition(GetReferenceID());
        }
        public ItemDetailDefinition GetItemDetailDefinition()
        {
            return GetDefinition() as ItemDetailDefinition;
        }

        public override object GetAmount()
        {
            return _amount;
        }
    }
}