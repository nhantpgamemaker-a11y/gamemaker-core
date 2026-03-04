
using System;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public abstract class BaseCurrencyRewardDefinition : BaseRewardDefinition
    {
        public BaseCurrencyRewardDefinition() : base()
        {
            
        }
        public BaseCurrencyRewardDefinition(string id,
        string name,
        string title,
        string description,
        Sprite icon,
        BaseMetaData metaData,
        string referenceId) : base(id, name, title, description, icon,metaData,referenceId )
        {
        }

        public override IDefinition GetDefinition()
        {
            return CurrencyManager.Instance.GetDefinition(GetReferenceID());
        }
        public abstract object GetNegativeAmount();
    }
}