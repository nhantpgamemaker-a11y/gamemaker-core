using System.Collections.Generic;
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
        private List<string> _iapGroups = new List<string>();
        public IReadOnlyList<string> IAPGroups => _iapGroups;
        protected override void OnLoad()
        {
            base.OnLoad();
            _iapGroups = GetDefinitions().Select(x => x.GroupID).Distinct().ToList();
        }
        public IAPDefinition GetIAPDefinitionByProductId(string id)
        {
            return GetDefinitions().FirstOrDefault(x => x.ProductID == id);
        }
        public List<IAPDefinition> GetIAPDefinitionsByGroupId(string groupId)
        {
            return GetDefinitions().Where(x => x.GroupID == groupId).ToList();
        }

#if UNITY_EDITOR
        [ContextMenu("Add IAP Definition")]
        private void AddIAPDefinition()
        {
            AddDefinition(new IAPDefinition());
        }
#endif

    }
}