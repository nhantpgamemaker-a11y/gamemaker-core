using System;
using System.Collections.Generic;
using System.Linq;
using GameMaker.Core.Runtime;

namespace GameMaker.Feature.Shop.Runtime
{
    [System.Serializable]
    public abstract class BasePlayerShop : BasePlayerData
    {
        [UnityEngine.SerializeReference]
        private List<BasePlayerShopItem> _playerShopItems;
        protected List<BasePlayerShopItem> PlayerShopItems => _playerShopItems;
        public BasePlayerShop(string id, IDefinition definition, List<BasePlayerShopItem> playerShopItems) : base(id, definition)
        {
            _playerShopItems = playerShopItems;
        }
        
        public void PurchaseItem(string shopItemReferenceId, float amount)
        {
            var playerShopItem = _playerShopItems.FirstOrDefault(x => x.GetReferenceID() == shopItemReferenceId);
            playerShopItem.AddRemain(amount);
            NotifyObserver(this);
        }
    }
}