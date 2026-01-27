using System;
using GameMaker.Core.Runtime;
using JetBrains.Annotations;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class PlayerStat : PlayerProperty
    {
        private long _value;
        
        public long Value { get => _value; set => _value = value; }

        public PlayerStat(IDefinition definition, long value):base(definition)
        {
            _value = value;
        }

        public override object Clone()
        {
            return new PlayerStat(definition, _value);
        }
        public void AddValue(long value)
        {
            _value += value;
            NotifyObserver(this);
        }
        public void SetValue(long value)
        {
            _value = value;
            NotifyObserver(this);
        }

        public override void CopyFrom(BasePlayerData basePlayerData)
        {
            _value = (basePlayerData as PlayerStat).Value;
            NotifyObserver(this);
        }
    }
}
