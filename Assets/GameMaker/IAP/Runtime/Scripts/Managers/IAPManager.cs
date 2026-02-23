using System.Linq;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Purchasing;

namespace GameMaker.IAP.Runtime
{
     [System.Serializable]
    [ScriptableObjectSingletonPathAttribute("Assets/Resources")]
    [CreateAssetMenu(fileName = "IAPManager", menuName = "GameMaker/IAP/IAPManager")]    
    public class IAPManager: BaseScriptableObjectDataManager<IAPManager, IAPDefinition>
    {
        public IAPDefinition GetIAPDefinitionByProductId(string id)
        {
            return GetDefinitions().FirstOrDefault(x => x.ProductID == id);
        }
    }
}