using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public abstract class BaseConditionDefinition : Definition, ITitle, IDescription
    {
        [SerializeField]
        private string _description;
        [SerializeField]
        private string _title;
        [SerializeField]
        private string _statCompareDefinitionId;
        public string StatCompareDefinitionId { get => _statCompareDefinitionId; }

        public BaseConditionDefinition() : base() { }
        public BaseConditionDefinition(string id, string name, string title, string description, string statCompareDefinitionId) : base(id, name)
        {
            _description = description;
            _title = title;
            _statCompareDefinitionId = statCompareDefinitionId;
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
