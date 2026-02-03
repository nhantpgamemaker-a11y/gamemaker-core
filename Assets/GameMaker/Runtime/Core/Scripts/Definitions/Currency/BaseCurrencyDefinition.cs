using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class BaseCurrencyDefinition : BaseDefinition
    {
        [SerializeField] private float _defaultValue;
        [SerializeField] private float _maxValue;
        public float DefaultValue { get => _defaultValue; set => _defaultValue = value; }
        public float MaxValue { get => _maxValue; set => _maxValue = value; }

        public BaseCurrencyDefinition() : base()
        {
            
        }
        public BaseCurrencyDefinition(string id, string name, string title,string description, Sprite icon,BaseMetaData metaData, float defaultValue) : base(id, name, title,description, icon,metaData)
        {
            DefaultValue = defaultValue;
        }
        public override object Clone()
        {
            return new BaseCurrencyDefinition(GetID(), GetName(), GetTitle(), GetDescription(), GetIcon(), GetMetaData(), DefaultValue);
        }

    }
}
