using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class BundleID: BaseID
    {
        public BundleDefinition GetBundleDefinition()
        {
            return BundleManager.Instance.GetDefinition(ID);
        }
        public override List<IDefinition> GetDefinitions()
        {
            return BundleManager.Instance.GetDefinitions().Cast<IDefinition>().ToList();
        }
    }
}