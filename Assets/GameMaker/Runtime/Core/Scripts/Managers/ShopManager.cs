using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [ScriptableObjectSingletonPathAttribute("Assets/Resources")]
    [CreateAssetMenu(fileName = "PropertyManager", menuName = "GameMaker/Property")]
    public class ShopManager : BaseScriptableObjectDataManager<ShopManager, BaseShopDefinition>
    {
        
    }
}