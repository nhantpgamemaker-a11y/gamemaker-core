using System;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class LongCurrencyRewardDefinition : BaseCurrencyRewardDefinition
    {
        [UnityEngine.SerializeField]
        private long _amount;
        public LongCurrencyRewardDefinition() : base()
        {
            
        }
        public LongCurrencyRewardDefinition(string id, string name, string title, string description, Sprite icon, BaseMetaData metaData, string referenceId, long amount):
        base(id, name, title, description, icon, metaData, referenceId)
        {
            _amount = amount;
        }
        public override object Clone()
        {
            return new LongCurrencyRewardDefinition(GetID(), GetName(), GetTitle(), GetDescription(), GetIcon(), GetMetaData(), GetReferenceID(), _amount);
        }

        public override object GetAmount()
        {
            return _amount;
        }
    }
}