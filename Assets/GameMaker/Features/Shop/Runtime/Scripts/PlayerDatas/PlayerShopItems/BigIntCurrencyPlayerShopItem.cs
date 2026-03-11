using GameMaker.Core.Runtime;

namespace GameMaker.Feature.Shop.Runtime
{
    [System.Serializable]
    public class BigIntCurrencyPlayerShopItem : BaseCurrencyPlayerShopItem
    {
        public BigIntCurrencyPlayerShopItem(string id, string name, IDefinition definition, bool canPurchase) : base(id, name, definition, canPurchase)
        {
        }

        public override object Clone()
        {
            return new  BigIntCurrencyPlayerShopItem(GetID(), GetName(), GetDefinition(), CanPurchase);
        }
    }
}