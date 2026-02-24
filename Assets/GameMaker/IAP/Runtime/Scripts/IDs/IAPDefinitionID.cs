using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;

namespace GameMaker.IAP.Runtime
{
    [System.Serializable]
    public class IAPDefinitionID : BaseID
    {
        public override List<IDefinition> GetDefinitions()
        {
            return IAPManager.Instance.GetDefinitions().Cast<IDefinition>().ToList();
        }
        public IAPDefinition GetIAPDefinition()
        {
            return IAPManager.Instance.GetDefinition(ID);
        }
    }
}