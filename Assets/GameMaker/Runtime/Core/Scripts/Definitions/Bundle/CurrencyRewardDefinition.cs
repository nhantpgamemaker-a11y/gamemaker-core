
using System;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
     [System.Serializable]
    public sealed class CurrencyRewardDefinition : BaseRewardDefinition
    {
        [UnityEngine.SerializeField]
        private float _amount;
        public float Amount => _amount;
        public CurrencyRewardDefinition(string id,string name, string title, string definition, Sprite icon,BaseMetaData metaData, float amount): base(id, name, title, definition, icon,metaData )
        {
            _amount = amount;
        }


        public override object Clone()
        {
            return new CurrencyRewardDefinition(GetID(),GetName(),GetTitle(), GetDescription(), GetIcon(), GetMetaData(), _amount);
        }

        public override IDefinition GetDefinition()
        {
            return CurrencyManager.Instance.GetDefinition(GetID());
        }
    }
}