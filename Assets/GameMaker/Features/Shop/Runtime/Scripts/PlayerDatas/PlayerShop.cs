using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;

namespace GameMaker.Feature.Shop.Runtime
{
    [System.Serializable]
    public class PlayerShop : BasePlayerData
    {
        [UnityEngine.SerializeReference]
        private List<BasePlayerShopItem> _playerShopItems;
        
        private long _nextRefreshUTCTime;
        private CancellationTokenSource _cts;
        public long NextRefreshUTCTime => _nextRefreshUTCTime;
        private Action<long> _onResetAction = null;
        private ShopDefinition RefreshShopDefinition => GetDefinition() as ShopDefinition;
        private bool _isShopResetting = false;
        public bool IsShopResetting => _isShopResetting;
        public List<BasePlayerShopItem> PlayerShopItems => _playerShopItems;
        public PlayerShop(string id,
            IDefinition definition,
            List<BasePlayerShopItem> playerShopItems,
            long nextRefreshUTCTime) : base(id, definition)
        {
            _playerShopItems = playerShopItems;
            _nextRefreshUTCTime = nextRefreshUTCTime;
        }

        public void OnInit(Action<long> onResetAction = null)
        {
            _onResetAction = onResetAction;
            TimeManager.Instance.OnTimeTickEventAction += OnTimeTick;
        }

        public void PurchaseItem(string shopItemReferenceId, bool canPurchase)
        {
            var playerShopItem = _playerShopItems.FirstOrDefault(x => x.GetReferenceID() == shopItemReferenceId);
            playerShopItem.SetCanPurchase(canPurchase);
            NotifyObserver(this);
        }

        public override object Clone()
        {
            return new PlayerShop(GetID(), GetDefinition(), PlayerShopItems.Select(x => x.Clone() as BasePlayerShopItem).ToList(), _nextRefreshUTCTime);
        }

        public override void CopyFrom(BasePlayerData basePlayerData)
        {
            var newPlayerShop = basePlayerData as PlayerShop;
            if (newPlayerShop == null) return;
            newPlayerShop.PlayerShopItems.ForEach(x =>
            {
                var currentItem = PlayerShopItems.FirstOrDefault(item => item.GetReferenceID() == x.GetReferenceID());
                currentItem.SetCanPurchase(x.CanPurchase);
            });

            _nextRefreshUTCTime = newPlayerShop.NextRefreshUTCTime;
            NotifyObserver(this);
        }
        private void OnTimeTick(long currentTimeStamp)
        {
            var config = RefreshShopDefinition.TimeResetConfig;
            if (config.IsReset(currentTimeStamp,_nextRefreshUTCTime))
            {
                OnReset();
            }
        }

        private void OnReset()
        {
            Logger.Log($"[PlayerShop] Resetting shop {GetID()} at {TimeManager.Instance.UTCNow} UTC, {TimeManager.Instance.LocalNow} Local.");
            _nextRefreshUTCTime = RefreshShopDefinition.TimeResetConfig.GetNextResetUtcTicks(_nextRefreshUTCTime);
            _onResetAction?.Invoke(_nextRefreshUTCTime);
        }
        public void SetIsShopResetting(bool isResetting)
        {
            _isShopResetting = isResetting;
        }
        public long GetNextRefreshUTCTime()
        {
            return _nextRefreshUTCTime;
        }
    }
}