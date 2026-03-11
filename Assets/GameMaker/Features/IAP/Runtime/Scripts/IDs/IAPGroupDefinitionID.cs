using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;

namespace GameMaker.IAP.Runtime
{
    [System.Serializable]
    public class IAPGroupDefinitionID : BaseID
    {
        public override List<IDefinition> GetDefinitions()
        {
            return new();
        }
        public List<IAPDefinition> GetIAPDefinitionsInGroup()
        {
            return IAPManager.Instance.GetIAPDefinitionsByGroupId(ID);
        }
    }
}