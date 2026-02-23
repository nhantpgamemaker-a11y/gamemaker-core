using GameMaker.Core.Runtime;
using UnityEditor.Experimental;

namespace GameMaker.Feature.Shop.Runtime
{
    [System.Serializable]
    public class CurrencyPlayerShopItem : BasePlayerShopItem
    {
        public CurrencyPlayerShopItem(string id, string name ,IDefinition definition, float remain) : base(id, name,definition, remain)
        {
        }

        public override object Clone()
        {
            return new CurrencyPlayerShopItem(GetID(), GetName(), GetDefinition(), Remain);
        }

        public override void CopyFrom(BasePlayerData basePlayerData)
        {
            throw new System.NotImplementedException();
        }
    }
}