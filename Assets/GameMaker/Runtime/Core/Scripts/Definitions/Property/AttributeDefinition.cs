using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class AttributeDefinition : PropertyDefinition
    {
        [SerializeField]
        private string _defaultValue;
        public string DefaultValue { get => _defaultValue; set => _defaultValue = value; }
        public AttributeDefinition() : base()
        {
            
        }
        public AttributeDefinition(string id, string name, string title,string description, Sprite icon, BaseMetaData metaData,string defaultValue) : base(id, name, title,description, icon,metaData)
        {
            DefaultValue = defaultValue;
        }
        public override object Clone()
        {
            return new AttributeDefinition(GetID(), GetName(), GetTitle(), GetDescription(), GetIcon(),GetMetaData(), DefaultValue);
        }
    }
}