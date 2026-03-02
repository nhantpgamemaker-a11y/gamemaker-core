using GameMaker.Core.Runtime;
using UnityEngine.UIElements;

namespace GameMaker.Feature.Shop.Editor
{
    [TypeCache]
    public abstract class BaseCurrencyShopItemDefinitionHolder : BaseShopItemDefinitionHolder
    {
        protected BaseCurrencyShopItemDefinitionHolder(VisualElement root) : base(root)
        {
        }
    }
}