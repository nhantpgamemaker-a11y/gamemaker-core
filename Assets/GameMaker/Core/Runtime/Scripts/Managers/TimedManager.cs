using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [ScriptableObjectSingletonPathAttribute("Assets/Resources")]
    [CreateAssetMenu(fileName = "TimedManager", menuName = "GameMaker/Timed/TimedManager")]
    public class TimedManager : BaseScriptableObjectDataManager<TimedManager, TimedDefinition>
    {
#if UNITY_EDITOR
        [ContextMenu("Add Timed")]
        private void AddTimed()
        {
            AddDefinition(new TimedDefinition());
        }
#endif
    }
}