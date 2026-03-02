using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    public  class TimeManager : AutomaticMonoSingleton<TimeManager>
    {
        [UnityEngine.SerializeField] private long _cheatAddTimeSeconds = 0;
        [ContextMenu("Cheat Add Time")]
        public void CheatAddTime()
        {
            AddTimeOffset(_cheatAddTimeSeconds);
            Logger.Log($"[TimeManager] Cheat added time: {_cheatAddTimeSeconds} seconds. Total cheat offset: {_cheatAddTimeSeconds} seconds.");
        }
        [UnityEngine.SerializeReference]
        private BaseTimeStrategy _timeStrategy;
        [UnityEngine.SerializeField]
        private bool _isInitialized;
        [UnityEngine.SerializeField]
        private TimeMode _currentMode = TimeMode.Hybrid;
        private long _addTimeOffsetSeconds = 0;
        private bool _isPaused = false;
        public event Action<long> OnTimeTickEventAction;
        public bool IsInitialized => _isInitialized;
        public TimeMode CurrentMode => _currentMode;
        public long AddTimeOffsetSeconds { get => _addTimeOffsetSeconds; }
        public bool IsPaused { get => _isPaused; set => _isPaused = value; }
        public  TimeSpan TimeUntilMidnight
        {
            get
            {
                DateTime now = UTCNow;
                DateTime nextMidnight = now.Date.AddDays(1);
                return nextMidnight - now;
            }
        }

        public  async UniTask<bool> InitializeAsync(TimeMode mode = TimeMode.Hybrid)
        {
            if (_isInitialized)
            {
                Logger.Log("[TimeManager] Already initialized!");
                return false;
            }

            _currentMode = mode;
            Logger.Log($"[TimeManager] Initializing with mode: {mode}");

            try
            {
                _timeStrategy = await CreateStrategy(mode);
                bool status = await _timeStrategy.InitAsync();
                if (!status) return false;
                _isInitialized = true;
                Logger.Log($"[TimeManager] ✓ Initialized - Source: {TimeSource}");
                StartTick(this.GetCancellationTokenOnDestroy()).Forget();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError($"[TimeManager] ✗ Initialization failed: {ex.Message}");
                _timeStrategy = new LocalTimeStrategy();
                await _timeStrategy.InitAsync();
                _isInitialized = true;
            }
            return true;
        }

        private  async UniTask<BaseTimeStrategy> CreateStrategy(TimeMode mode)
        {
            switch (mode)
            {
                case TimeMode.Local:
                    return new LocalTimeStrategy();

                case TimeMode.Server:
                    var serverStrategy = new ServerTimeStrategy();
                    return serverStrategy;

                case TimeMode.Hybrid:
                    var hybridStrategy = new HybridTimeStrategy();
                    return hybridStrategy;

                default:
                    return new LocalTimeStrategy();
            }
        }

        public  void Shutdown()
        {
            Logger.Log("[TimeManager] Shutting down...");
            _isInitialized = false;
            _timeStrategy = null;
        }

        public  DateTime UTCNow
        {
            get
            {
                if (!_isInitialized)
                {
                    Logger.LogWarning("[TimeManager] Not initialized, using DateTime.UtcNow");
                    return DateTime.UtcNow;
                }

                return _timeStrategy.GetCurrentUTCTime() + TimeSpan.FromSeconds(_addTimeOffsetSeconds);
            }
        }

        public  DateTime LocalNow => UTCNow.ToLocalTime();

        public  bool IsTimeValid
        {
            get
            {
                if (!_isInitialized) return false;
                return _timeStrategy.IsTimeValid();
            }
        }

        public  string TimeSource
        {
            get
            {
                if (!_isInitialized) return "Not Initialized";
                return _timeStrategy.GetTimeSource();
            }
        }

        public  long UnixTimestamp => ((DateTimeOffset)UTCNow).ToUnixTimeSeconds();

        public long UnixTimestampMs => ((DateTimeOffset)UTCNow).ToUnixTimeMilliseconds();
        
        public DateTime FromUnixTimestamp(long timestamp)
        {
            return DateTimeOffset.FromUnixTimeSeconds(timestamp).UtcDateTime;
        }

        public bool IsToday(DateTime date)
        {
            return date.Date == UTCNow.Date;
        }

        private async UniTaskVoid StartTick(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                await UniTask.WaitUntil(() => !_isPaused, cancellationToken: ct);
                await UniTask.Delay(1000, DelayType.Realtime, cancellationToken: ct);
                try
                {
                    Logger.Log($"[TimeManager] Tick - UTCNow: {UTCNow:HH:mm:ss}, LocalNow: {LocalNow:HH:mm:ss}, UnixTimestamp: {UnixTimestamp}");
                    OnTimeTickEventAction?.Invoke(UnixTimestamp);
                }
                catch (Exception ex)
                {
                    Logger.LogError($"[TimeManager] Error in time tick event: {ex.Message}");
                }
            }
        }
        public void AddTimeOffset(long seconds)
        {
            _addTimeOffsetSeconds += seconds;
            Logger.Log($"[TimeManager] Time offset added: {seconds} seconds. Total offset: {_addTimeOffsetSeconds} seconds.");
        }
        
        public void PauseTick(bool status)
        {
            _isPaused = status;
            Logger.Log($"[TimeManager] Time tick paused : {status}.");
        }
    }
}