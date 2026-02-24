using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class ConditionDefinition : Definition, ITitle, IDescription
    {
        [SerializeField]
        private string _description;
        [SerializeField]
        private string _title;
        [SerializeField]
        private string _statCompareDefinitionId;
        [SerializeField]
        private float _compareValue;

        public float CompareValue { get => _compareValue;}
        public string StatCompareDefinitionId { get => _statCompareDefinitionId; }

        public ConditionDefinition() : base() { }
        public ConditionDefinition(string id, string name, string title, string description, string statCompareDefinitionId, float compareValue) : base(id, name)
        {
            _description = description;
            _title = title;
            _statCompareDefinitionId = statCompareDefinitionId;
            _compareValue = compareValue;
        }
        public override object Clone()
        {
            return new ConditionDefinition(GetID(), GetName(), _title, _description, _statCompareDefinitionId, _compareValue);
        }

        public string GetDescription()
        {
            return _description;
        }

        public string GetTitle()
        {
            return _title;
        }
        public StatDefinition GetStatCompareDefinitionId()
        {
            return PropertyManager.Instance.GetDefinition(_statCompareDefinitionId) as StatDefinition;
        }
    }
}
