using System;
using System.Collections.Generic;
using System.Linq;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class ItemAttributeDefinitionRef: ItemPropertyDefinitionRef
    {
        [SerializeField]
        private string _value;

        public string Value { get => _value; }
        public ItemAttributeDefinitionRef(string refId,string name, string value):base(refId,name)
        {
            _value = value;
        }

        public override object Clone()
        {
            return new ItemAttributeDefinitionRef(GetID(),GetName(), _value);
        }

        public override ItemPropertyDefinitionRefModel ToItemPropertyDefinitionRefModel()
        {
            return new ItemAttributeDefinitionRefModel(GetID(), GetName(), _value);
        }
    }
}