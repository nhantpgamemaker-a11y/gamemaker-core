using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
     [ScriptableObjectSingletonPathAttribute("Assets/Resources")]
    [CreateAssetMenu(fileName ="ItemManager",menuName = "GameMaker/Items/ItemManager")]
    public class ItemManager : BaseScriptableObjectDataManager<ItemManager, ItemDefinition>
    {
    }
}