using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class CurrencyDefinition : BaseDefinition
    {
        [SerializeField] private float _defaultValue;
        [SerializeField] private float _maxValue;
        [SerializeField] private bool _isGain = false;
        [SerializeField] private float _perGainAmount;
        [SerializeField] private float _gainTime;
        [SerializeField] private float _maxGainValue;

        public float DefaultValue { get => _defaultValue; set => _defaultValue = value; }
        public float MaxValue { get => _maxValue; set => _maxValue = value; }
        public bool IsGain { get => _isGain; set => _isGain = value; }
        public float PerGainAmount { get => _perGainAmount; set => _perGainAmount = value; }
        public float GainTime { get => _gainTime; set => _gainTime = value; }
        public float MaxGainValue { get => _maxGainValue; set => _maxGainValue = value; }

        public CurrencyDefinition() : base()
        {
            
        }
        public CurrencyDefinition(string id, string name, string title,string description, Sprite icon,BaseMetaData metaData, float defaultValue) : base(id, name, title,description, icon,metaData)
        {
            DefaultValue = defaultValue;
        }
        public override object Clone()
        {
            return new CurrencyDefinition(GetID(), GetName(), GetTitle(),GetDescription(), GetIcon(), GetMetaData(),DefaultValue);
        }
    }
}
