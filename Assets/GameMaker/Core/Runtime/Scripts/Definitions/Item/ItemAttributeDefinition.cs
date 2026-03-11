using System.Collections.Generic;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class ItemAttributeDefinition : ItemPropertyDefinition
    {
        [UnityEngine.SerializeField]
        private string _defaultValue;
        public string DefaultValue { get => _defaultValue; set => _defaultValue = value; }
        public ItemAttributeDefinition() : base()
        {
            
        }
        public ItemAttributeDefinition(string id, string name,string defaultValue): base(id, name)
        {
            _defaultValue = defaultValue;
        }

        public override object Clone()
        {
            return new ItemAttributeDefinition(GetID(), GetName(), _defaultValue);
        }

        public override ItemPropertyDefinitionRef GetPropertyDefinitionRef()
        {
            return new ItemAttributeDefinitionRef(GetID(), GetName(), _defaultValue);
        }
    }
}