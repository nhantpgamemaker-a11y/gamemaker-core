using System;
using GameMaker.Core.Runtime;
using JetBrains.Annotations;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class PlayerAttribute : PlayerProperty
    {
        private string _value;
        public string Value { get => _value; set => _value = value; }

        public PlayerAttribute(string id, IDefinition definition, string value):base(id, definition)
        {
            _value = value;
        }

        public override object Clone()
        {
            return new PlayerAttribute(GetID(),definition, _value);
        }

        public override void CopyFrom(BasePlayerData basePlayerData)
        {
             _value = (basePlayerData as PlayerAttribute).Value;
            NotifyObserver(this);
        }

        public override string GetStringValue()
        {
            return _value;
        }

        public override void Set(string value)
        {
            _value = value;
            NotifyObserver(this);
        }
    }
}
