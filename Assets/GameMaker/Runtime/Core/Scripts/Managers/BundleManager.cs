using System.Collections.Generic;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    [ScriptableObjectSingletonPathAttribute("Assets/Resources")]
    [CreateAssetMenu(fileName ="BundleManager",menuName = "GameMaker/Bundle")]
    public class BundleManager : BaseScriptableObjectDataManager<BundleManager, BundleDefinition>
    {
    }
}
