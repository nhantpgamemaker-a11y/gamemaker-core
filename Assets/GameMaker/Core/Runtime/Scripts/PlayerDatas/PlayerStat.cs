using System;
using GameMaker.Core.Runtime;
using JetBrains.Annotations;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class PlayerStat : PlayerProperty
    {
        private float _value;
        
        public float Value { get => _value; set => _value = value; }

        public PlayerStat(string id, IDefinition definition, float value):base(id, definition)
        {
            _value = value;
        }

        public override object Clone()
        {
            return new PlayerStat(GetID(), definition, _value);
        }

        public override void CopyFrom(BasePlayerData basePlayerData)
        {
            _value = (basePlayerData as PlayerStat).Value;
            NotifyObserver(this);
        }

        public override string GetStringValue()
        {
            return _value.ToString();
        }

        public override void Set(string value)
        {
            _value = float.Parse(value);
            NotifyObserver(this);
        }
    }
}
