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
        public ConditionDefinition GetConditionDefinition()
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
            return playerStat.Value >= GetConditionDefinition().CompareValue;
        }
        
        public PlayerStat GetPlayerStat()
        {
            var conditionDefinition = GetConditionDefinition();
            var playerStat = PropertyGateway.Manager.GetPlayerProperty(conditionDefinition.StatCompareDefinitionId) as PlayerStat;
            return playerStat;
        }
    }
}