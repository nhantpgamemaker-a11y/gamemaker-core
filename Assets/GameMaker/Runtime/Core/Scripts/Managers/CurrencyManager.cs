using System.Collections.Generic;
using System.Linq;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    [ScriptableObjectSingletonPathAttribute("Assets/Resources")]
    [CreateAssetMenu(fileName ="CurrencyManager",menuName = "GameMaker/Currency/CurrencyManager")]
    public class CurrencyManager : BaseScriptableObjectDataManager<CurrencyManager, CurrencyDefinition>
    {
    }
}