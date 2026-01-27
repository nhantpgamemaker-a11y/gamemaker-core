using NUnit.Framework;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class StatDefinition : PropertyDefinition
    {
        [SerializeField] private float _defaultValue;
        public float DefaultValue { get => _defaultValue; set => _defaultValue = value; }
        public StatDefinition(): base()
        {
            
        }
        public StatDefinition(string id, string name, string title,string description, Sprite icon, BaseMetaData metaData,float defaultValue) : base(id, name, title,description, icon,metaData)
        {
            DefaultValue = defaultValue;
        }
        public override object Clone()
        {
            return new StatDefinition(GetID(), GetName(), GetTitle(), GetDescription(), GetIcon(),GetMetaData(), DefaultValue);
        }
    }
}