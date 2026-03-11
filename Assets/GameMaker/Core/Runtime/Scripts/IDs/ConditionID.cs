using System.Collections.Generic;
using System.Linq;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class ConditionID : CurrencyID
    {
        public override List<IDefinition> GetDefinitions()
        {
            return ConditionManager.Instance.GetDefinitions().Cast<IDefinition>().ToList();
        }
        public BaseConditionDefinition GetConditionDefinition()
        {
            return ConditionManager.Instance.GetDefinition(ID);
        }
        public bool CheckCondition()
        {
            var conditionDefinition = GetConditionDefinition();
            var playerStat = PropertyGateway.Manager.GetPlayerProperty(conditionDefinition.StatCompareDefinitionId) as PlayerStat;
            if (playerStat == null)
            {
                return false;
            }
            if (conditionDefinition is ConfigConditionDefinition configConditionDefinition)
            {
                var playerConfig = ConfigGateway.Manager.GetPlayerConfig(configConditionDefinition.ConfigCompareDefinitionId) as PlayerConfig;
                if (playerConfig == null)
                {
                    return false;
                }
                return playerStat.Value >= playerConfig.GetFloatValue();
            }
            if(conditionDefinition is ValueConditionDefinition valueConditionDefinition)
            {
                return playerStat.Value >= valueConditionDefinition.CompareValue;
            }
            return false;
        }
        
        public PlayerStat GetPlayerStat()
        {
            var conditionDefinition = GetConditionDefinition();
            var playerStat = PropertyGateway.Manager.GetPlayerProperty(conditionDefinition.StatCompareDefinitionId) as PlayerStat;
            return playerStat;
        }
    }
}