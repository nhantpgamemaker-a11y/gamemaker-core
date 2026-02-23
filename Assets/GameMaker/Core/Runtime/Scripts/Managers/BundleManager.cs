using System.Collections.Generic;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    [ScriptableObjectSingletonPathAttribute("Assets/Resources")]
    [CreateAssetMenu(fileName ="BundleManager",menuName = "GameMaker/Bundle/BundleManager")]
    public class BundleManager : BaseScriptableObjectDataManager<BundleManager, BundleDefinition>
    {
    }
}
