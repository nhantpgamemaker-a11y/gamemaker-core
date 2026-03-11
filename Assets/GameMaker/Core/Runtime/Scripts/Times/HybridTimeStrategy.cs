using System;
using Cysharp.Threading.Tasks;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class HybridTimeStrategy : BaseTimeStrategy
    {
        [UnityEngine.SerializeField]
        private ServerTimeStrategy _serverStrategy;
        [UnityEngine.SerializeField]
        private LocalTimeStrategy _localStrategy;
        [UnityEngine.SerializeField]
        private bool _preferServer = true;

        public HybridTimeStrategy()
        {
            _serverStrategy = new ServerTimeStrategy();
            _localStrategy = new LocalTimeStrategy();
        }

        public override async UniTask<bool> InitAsync()
        {
            bool status = await _serverStrategy.InitAsync();
                if (!status) return status;

                if (!_serverStrategy.IsTimeValid())
                {
                    Logger.LogWarning("[HybridTime] Server unavailable, using local time");
                    _preferServer = false;
                }
                return true;
        }

        public override DateTime GetCurrentUTCTime()
        {
            if (_preferServer && _serverStrategy.IsTimeValid())
            {
                return _serverStrategy.GetCurrentUTCTime();
            }

            Logger.LogWarning("[HybridTime] Using local time as fallback");
            return _localStrategy.GetCurrentUTCTime();
        }

        public override bool IsTimeValid()
        {
            if (_preferServer)
            {
                return _serverStrategy.IsTimeValid();
            }

            return _localStrategy.IsTimeValid();
        }

        public override string GetTimeSource()
        {
            if (_preferServer && _serverStrategy.IsTimeValid())
            {
                return _serverStrategy.GetTimeSource();
            }

            return $"{_localStrategy.GetTimeSource()} (Fallback)";
        }

        public void ForceServerSync()
        {
            _serverStrategy.InitAsync().Forget();
            _preferServer = true;
        }
    }
}