using System.Collections.Generic;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class ItemStatDefinition : Definition
    {
        [UnityEngine.SerializeField]
        private float _defaultValue;
        public float DefaultValue { get => _defaultValue; set => _defaultValue = value; }
        public ItemStatDefinition() : base()
        {
            
        }
        public ItemStatDefinition(string id, string name,float defaultValue): base(id, name)
        {
            _defaultValue = defaultValue;
        }

        public override object Clone()
        {
            return new ItemStatDefinition(GetID(), GetName(), _defaultValue);
        }
    }
}