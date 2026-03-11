using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class ActionID: BaseID
    {
        public ActionDefinition GetActionDefinition()
        {
            return ActionManager.Instance.GetDefinition(ID);
        }
        
        public override List<IDefinition> GetDefinitions()
        {
            return ActionManager.Instance.GetDefinitions().Cast<IDefinition>().ToList();
        }
    }
}