using GameMaker.Core.Runtime;
using UnityEditor.Experimental;

namespace GameMaker.Feature.Shop.Runtime
{
    [System.Serializable]
    public class ItemPlayerShopItem : BasePlayerShopItem
    {
        public ItemPlayerShopItem(string id, string name ,IDefinition definition, bool canPurchase) : base(id, name,definition, canPurchase)
        {
        }

        public override object Clone()
        {
            return new ItemPlayerShopItem(GetID(), GetName(), GetDefinition(), CanPurchase);
        }

        public override void CopyFrom(BasePlayerData basePlayerData)
        {
            throw new System.NotImplementedException();
        }
    }
}