using System.Collections.Generic;
using System.Linq;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Feature.Shop.Runtime
{
    [System.Serializable]
    public class ShopID : BaseID
    {
        public override List<IDefinition> GetDefinitions()
        {
            return ShopManager.Instance.GetDefinitions().Cast<IDefinition>().ToList();
        }
        public ShopDefinition GetShopDefinition()
        {
            return ShopManager.Instance.GetDefinition(ID);
        }
        public PlayerShop GetPlayerShop()
        {
            return ShopGateway.Manager.GetPlayerShop(ID);
        }
    }
}
