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
        protected List<BasePlayerShopItem> PlayerShopItems => _playerShopItems;
        private long _lastRefreshUTCTime;
        private CancellationTokenSource _cts;
        public long LastRefreshUTCTime => _lastRefreshUTCTime;
        private Action<long> _onResetAction = null;
        private ShopDefinition RefreshShopDefinition => GetDefinition() as ShopDefinition;
        private bool _isShopResetting = false;
        public bool IsShopResetting => _isShopResetting;
        public PlayerShop(string id,
            IDefinition definition,
            List<BasePlayerShopItem> playerShopItems,
            long lastRefreshUTCTime) : base(id, definition)
        {
            _playerShopItems = playerShopItems;
            _lastRefreshUTCTime = lastRefreshUTCTime;
        }

        public void OnInit(Action<long> onResetAction = null)
        {
            _onResetAction = onResetAction;
            if (RefreshShopDefinition.TimeResetConfig.ResetType == ResetType.None) return;
            TimeManager.Instance.OnTimeTickEventAction += OnTimeTick;
            //StartSchedule();
        }

        public void PurchaseItem(string shopItemReferenceId, float amount)
        {
            var playerShopItem = _playerShopItems.FirstOrDefault(x => x.GetReferenceID() == shopItemReferenceId);
            playerShopItem.AddRemain(amount);
            NotifyObserver(this);
        }

        public override object Clone()
        {
            return new PlayerShop(GetID(), GetDefinition(), PlayerShopItems.Select(x => x.Clone() as BasePlayerShopItem).ToList(), _lastRefreshUTCTime);
        }

        public override void CopyFrom(BasePlayerData basePlayerData)
        {
            var newPlayerShop = basePlayerData as PlayerShop;
            if (newPlayerShop == null) return;
            _playerShopItems = newPlayerShop.PlayerShopItems;
            _lastRefreshUTCTime = newPlayerShop.LastRefreshUTCTime;
            NotifyObserver(this);
        }
        private void OnTimeTick(long currentTimeStamp)
        {
            var config = RefreshShopDefinition.TimeResetConfig;
            if (config.IsReset(currentTimeStamp))
            {
                OnReset();
            }
        }

        private void StartSchedule()
        {
            var config = RefreshShopDefinition.TimeResetConfig;
            if (config.ResetType == ResetType.None) return;
            
            _cts = new CancellationTokenSource();
            if (config.IsReset(_lastRefreshUTCTime))
                OnReset();

            long next = config.GetNextResetUtcTicks(_lastRefreshUTCTime);
            ScheduleReset(next, _cts.Token).Forget();
        }
        
        private async UniTaskVoid ScheduleReset(long nextResetUtcTicks, CancellationToken ct)
        {
            DateTime nextReset = new DateTime(nextResetUtcTicks, DateTimeKind.Utc);
            TimeSpan delay = nextReset - TimeManager.Instance.UTCNow;

            if (delay > TimeSpan.Zero)
                await UniTask.Delay(delay, cancellationToken: ct);

            if (ct.IsCancellationRequested) return;

            OnReset();

            long next = RefreshShopDefinition.TimeResetConfig.GetNextResetUtcTicks(_lastRefreshUTCTime);
            ScheduleReset(next, ct).Forget();
        }

        private void OnReset()
        {
            Logger.Log($"[PlayerShop] Resetting shop {GetID()} at {TimeManager.Instance.UTCNow} UTC, {TimeManager.Instance.LocalNow} Local.");
            _lastRefreshUTCTime = TimeManager.Instance.UTCNow.Ticks;
            _onResetAction?.Invoke(_lastRefreshUTCTime);
        }
        public void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }
        public void SetIsShopResetting(bool isResetting)
        {
            _isShopResetting = isResetting;
            NotifyObserver(this);
        }
    }
}