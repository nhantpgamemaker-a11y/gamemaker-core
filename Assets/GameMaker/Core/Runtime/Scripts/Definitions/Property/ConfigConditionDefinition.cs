using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class ConfigConditionDefinition : BaseConditionDefinition
    {
        [SerializeField]
        private string _configCompareDefinitionId;
        public string ConfigCompareDefinitionId { get => _configCompareDefinitionId; }
        public ConfigConditionDefinition() : base() { }
        public ConfigConditionDefinition(string id, string name, string title, string description, string statCompareDefinitionId, string configCompareDefinitionId) : base(id, name, title, description, statCompareDefinitionId)
        {
            _configCompareDefinitionId = configCompareDefinitionId;
        }

        public override object Clone()
        {
            return new ConfigConditionDefinition(GetID(), GetName(), GetTitle(), GetDescription(), GetStatCompareDefinitionId().GetID(), _configCompareDefinitionId);
        }
    }
}