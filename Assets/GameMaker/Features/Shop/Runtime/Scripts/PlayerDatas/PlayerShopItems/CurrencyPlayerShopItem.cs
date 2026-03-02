using GameMaker.Core.Runtime;
using UnityEditor.Experimental;

namespace GameMaker.Feature.Shop.Runtime
{
    [System.Serializable]
    [TypeCache]
    public abstract class BaseCurrencyPlayerShopItem : BasePlayerShopItem
    {
        public BaseCurrencyPlayerShopItem(string id, string name ,IDefinition definition, bool canPurchase) : base(id, name,definition, canPurchase)
        {
        }
    }
}