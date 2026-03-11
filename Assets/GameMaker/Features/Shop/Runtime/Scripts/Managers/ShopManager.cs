using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Feature.Shop.Runtime
{
    [ScriptableObjectSingletonPathAttribute("Assets/Resources")]
    [CreateAssetMenu(fileName = "ShopManager", menuName = "GameMaker/Feature/Shop/ShopManager")]
    public class ShopManager : BaseScriptableObjectDataManager<ShopManager, ShopDefinition>
    {
#if UNITY_EDITOR
        [ContextMenu("Add Shop Definition")]
        private void AddShopDefinition()
        {
            AddDefinition(new ShopDefinition());
        }
#endif
    }
}