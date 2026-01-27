
using System;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    public enum UpdateType
    {
        Add = 0,
        Override = 1,
        OverrideIfGreater = 2,
        AddSecondToTimeNow = 3
    }
    [System.Serializable]
    public sealed class StatRewardDefinition : BaseRewardDefinition
    {
        [UnityEngine.SerializeField]
        private long _amount;
        [UnityEngine.SerializeField]
        private UpdateType _updateType;
        public long Amount => _amount;
        public UpdateType UpdateType => _updateType;
        public StatRewardDefinition(string id,string name, string title, string definition, Sprite icon,BaseMetaData metaData, long amount, UpdateType updateType): base(id, name, title, definition, icon,metaData )
        {
            _amount = amount;
            _updateType = updateType;
        }

        public override object Clone()
        {
            return new StatRewardDefinition(GetID(),GetName(),GetTitle(), GetDescription(), GetIcon(), GetMetaData(), _amount,_updateType);
        }

        public override IDefinition GetDefinition()
        {
            return PropertyManager.Instance.GetDefinition(GetID());
        }
    }
}