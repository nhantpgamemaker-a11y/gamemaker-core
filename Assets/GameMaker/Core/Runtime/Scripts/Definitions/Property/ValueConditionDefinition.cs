using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class ValueConditionDefinition : BaseConditionDefinition
    {
        [SerializeField]
        private float _compareValue;
        public float CompareValue { get => _compareValue; }

        public ValueConditionDefinition() : base() { }
        public ValueConditionDefinition(string id, string name, string title, string description, string statCompareDefinitionId, float compareValue) : base(id, name, title, description, statCompareDefinitionId)
        {
            _compareValue = compareValue;   
        }

        public override object Clone()
        {
            return new ValueConditionDefinition(GetID(), GetName(), GetTitle(), GetDescription(), GetStatCompareDefinitionId().GetID(), _compareValue);
        }
    }
}