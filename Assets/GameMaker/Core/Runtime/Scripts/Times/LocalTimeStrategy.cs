using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class LocalTimeStrategy:BaseTimeStrategy
    {
        [UnityEngine.SerializeField]
        private DateTime _lastKnownTime;
        [UnityEngine.SerializeField]
        private float _sessionStartTime;
        private const string LAST_TIME_KEY = "LastKnownTime";

        public override async UniTask<bool> InitAsync()
        {
            string savedTime = PlayerPrefs.GetString(LAST_TIME_KEY, "");
            
            if (DateTime.TryParse(savedTime, out DateTime parsedTime))
            {
                _lastKnownTime = parsedTime;
            }
            else
            {
                _lastKnownTime = DateTime.UtcNow;
            }

            _sessionStartTime = Time.realtimeSinceStartup;
            return true;
        }

        public override DateTime GetCurrentTime()
        {
            DateTime systemTime = DateTime.UtcNow;
            if (systemTime < _lastKnownTime)
            {
                Logger.LogWarning("[LocalTime] Time went backwards! Using cached time.");
                float sessionElapsed = Time.realtimeSinceStartup - _sessionStartTime;
                return _lastKnownTime.AddSeconds(sessionElapsed);
            }

            _lastKnownTime = systemTime;
            SaveTime();
            
            return systemTime;
        }

        public override bool IsTimeValid()
        {
            DateTime systemTime = DateTime.UtcNow;
            TimeSpan diff = systemTime - _lastKnownTime;
            if (diff.TotalDays > 1)
            {
                Logger.LogWarning($"[LocalTime] Suspicious time jump: {diff.TotalDays} days");
                return false;
            }

            return true;
        }

        public override string GetTimeSource()
        {
            return "Local System Time (UTC)";
        }

        private void SaveTime()
        {
            PlayerPrefs.SetString(LAST_TIME_KEY, _lastKnownTime.ToString("o"));
            PlayerPrefs.Save();
        }
    }
}