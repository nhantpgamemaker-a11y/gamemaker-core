using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class ServerTimeStrategy : BaseTimeStrategy
    {
        [UnityEngine.SerializeField]
        private DateTime _serverTime;
        [UnityEngine.SerializeField]
        private float _lastSyncRealtimeSinceStartup;
        [UnityEngine.SerializeField]
        private bool _isSynced;
        [UnityEngine.SerializeField]
        private float _syncInterval = 300f; 

        public override async UniTask<bool> InitAsync()
        {
            try
            {
                ServerTimeResponse response = await TimeApiClient.GetServerTime();

                if (response.Success)
                {
                    _serverTime = response.ServerTime;
                    _lastSyncRealtimeSinceStartup = Time.realtimeSinceStartup;
                    _isSynced = true;

                    Logger.Log($"[ServerTime] Synced: {_serverTime:yyyy-MM-dd HH:mm:ss}");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"[ServerTime] Sync failed: {ex.Message}");
                _isSynced = false;
                return false;
            }
            return false;
        }

        public override DateTime GetCurrentTime()
        {
            if (!_isSynced)
            {
                Logger.LogWarning("[ServerTime] Not synced yet, using fallback");
                return DateTime.UtcNow;
            }

            float elapsed = Time.realtimeSinceStartup - _lastSyncRealtimeSinceStartup;
            DateTime currentTime = _serverTime.AddSeconds(elapsed);

            // Auto re-sync if needed
            if (elapsed >= _syncInterval)
            {
                InitAsync().Forget();
            }

            return currentTime;
        }

        public override bool IsTimeValid()
        {
            return _isSynced;
        }

        public override string GetTimeSource()
        {
            return _isSynced ? "Server Time (Synced)" : "Server Time (Not Synced)";
        }
    }
}