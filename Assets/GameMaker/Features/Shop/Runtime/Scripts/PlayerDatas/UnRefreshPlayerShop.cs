using System;
using System.Collections.Generic;
using System.Linq;
using GameMaker.Core.Runtime;

namespace GameMaker.Feature.Shop.Runtime
{
    [System.Serializable]
    public class UnRefreshPlayerShop : BasePlayerShop
    {
        public UnRefreshPlayerShop(string id, IDefinition definition, List<BasePlayerShopItem> playerShopItems) : base(id, definition, playerShopItems)
        {
        }

        public override object Clone()
        {
            return new UnRefreshPlayerShop(GetID(), GetDefinition(), PlayerShopItems.Select(x => x.Clone() as BasePlayerShopItem).ToList());
        }

        public override void CopyFrom(BasePlayerData basePlayerData)
        {
            throw new NotImplementedException();
        }
    }
}