using System;
using System.Collections.Generic;
using System.Linq;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class ItemStatDefinitionRef: Definition
    {
        [SerializeField]
        private float _value;

        public float Value { get => _value; }
        public ItemStatDefinitionRef(string refId,string name, float value):base(refId,name)
        {
            _value = value;
        }

        public override object Clone()
        {
            return new ItemStatDefinitionRef(GetID(),GetName(), _value);
        }
    }
}