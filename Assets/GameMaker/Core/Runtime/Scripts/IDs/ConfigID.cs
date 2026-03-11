using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class ConfigID: BaseID
    {
        public ConfigDefinition GetConfigDefinition()
        {
            return ConfigManager.Instance.GetDefinition(ID);
        }
        public override List<IDefinition> GetDefinitions()
        {
            return ConfigManager.Instance.GetDefinitions().Cast<IDefinition>().ToList();
        }
    }
}