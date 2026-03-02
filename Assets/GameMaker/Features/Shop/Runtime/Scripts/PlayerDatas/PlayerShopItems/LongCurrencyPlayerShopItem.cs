using GameMaker.Core.Runtime;

namespace GameMaker.Feature.Shop.Runtime
{
    [System.Serializable]
    public class LongCurrencyPlayerShopItem : BaseCurrencyPlayerShopItem
    {
        public LongCurrencyPlayerShopItem(string id, string name, IDefinition definition, bool canPurchase) : base(id, name, definition, canPurchase)
        {
        }

        public override object Clone()
        {
            return new LongCurrencyPlayerShopItem(GetID(), GetName(), GetDefinition(), CanPurchase);
        }
    }
}