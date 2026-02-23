using System;
using Cysharp.Threading.Tasks;

namespace GameMaker.Core.Runtime
{
    public  class TimeManager : AutomaticMonoSingleton<TimeManager>
    {
        [UnityEngine.SerializeReference]
        private BaseTimeStrategy _timeStrategy;
        [UnityEngine.SerializeField]
        private bool _isInitialized;
        [UnityEngine.SerializeField]
        private  TimeMode _currentMode = TimeMode.Hybrid;

        public  async UniTask<bool> InitializeAsync(TimeMode mode = TimeMode.Hybrid)
        {
            if (_isInitialized)
            {
                Logger.Log("Already initialized!");
                return false;
            }

            _currentMode = mode;
            Logger.Log($"Initializing with mode: {mode}");

            try
            {
                _timeStrategy = await CreateStrategy(mode);
                bool status = await _timeStrategy.InitAsync();
                if (!status) return false;
                _isInitialized = true;
                Logger.Log($"✓ Initialized - Source: {TimeSource}");
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError($"✗ Initialization failed: {ex.Message}");
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
            Logger.Log("Shutting down...");
            _isInitialized = false;
            _timeStrategy = null;
        }

        public  DateTime Now
        {
            get
            {
                if (!_isInitialized)
                {
                    Logger.LogWarning("Not initialized, using DateTime.UtcNow");
                    return DateTime.UtcNow;
                }

                return _timeStrategy.GetCurrentTime();
            }
        }

        public  DateTime LocalNow => Now.ToLocalTime();

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

        public  long UnixTimestamp => ((DateTimeOffset)Now).ToUnixTimeSeconds();

        public long UnixTimestampMs => ((DateTimeOffset)Now).ToUnixTimeMilliseconds();
        
        public DateTime FromUnixTimestamp(long timestamp)
        {
            return DateTimeOffset.FromUnixTimeSeconds(timestamp).UtcDateTime;
        }
        
        public bool IsToday(DateTime date)
        {
            return date.Date == Now.Date;
        }
        
        public  TimeSpan TimeUntilMidnight
        {
            get
            {
                DateTime now = Now;
                DateTime nextMidnight = now.Date.AddDays(1);
                return nextMidnight - now;
            }
        }

        public  bool IsInitialized => _isInitialized;
    }
}