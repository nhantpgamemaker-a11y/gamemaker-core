
using System;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
     [System.Serializable]
    public sealed class TimedRewardDefinition : BaseRewardDefinition
    {
        [UnityEngine.SerializeField]
        private long _amount;
        public long Amount => _amount;
        public TimedRewardDefinition() : base()
        {
            
        }
        public TimedRewardDefinition(string id,
        string name,
        string title,
        string definition,
        Sprite icon,
        BaseMetaData metaData,
        string referenceId,
        long amount) : base(id, name, title, definition, icon,metaData,referenceId )
        {
            _amount = amount;
        }


        public override object Clone()
        {
            return new TimedRewardDefinition(GetID(),GetName(),GetTitle(), GetDescription(), GetIcon(), GetMetaData(),GetReferenceID(), _amount);
        }

        public override IDefinition GetDefinition()
        {
            return TimedManager.Instance.GetDefinition(GetReferenceID());
        }
    }
}