
using System;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
     [System.Serializable]
    public sealed class CurrencyRewardDefinition : BaseRewardDefinition
    {
        [UnityEngine.SerializeField]
        private string _amount;
        public string Amount => _amount;
        public CurrencyRewardDefinition() : base()
        {
            
        }
        public CurrencyRewardDefinition(string id,
        string name,
        string title,
        string definition,
        Sprite icon,
        BaseMetaData metaData,
        string referenceId,
        string amount) : base(id, name, title, definition, icon,metaData,referenceId )
        {
            _amount = amount;
        }


        public override object Clone()
        {
            return new CurrencyRewardDefinition(GetID(),GetName(),GetTitle(), GetDescription(), GetIcon(), GetMetaData(),GetReferenceID(), _amount);
        }

        public override IDefinition GetDefinition()
        {
            return CurrencyManager.Instance.GetDefinition(GetReferenceID());
        }
    }
}