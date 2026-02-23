using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Feature.Shop.Runtime
{
    [ScriptableObjectSingletonPathAttribute("Assets/Resources")]
    [CreateAssetMenu(fileName = "ShopManager", menuName = "GameMaker/Feature/Shop/ShopManager")]
    public class ShopManager : BaseScriptableObjectDataManager<ShopManager, BaseShopDefinition>
    {
        
    }
}