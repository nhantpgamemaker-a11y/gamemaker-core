using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Cronos;

namespace GameMaker.Core.Runtime
{
    public enum ResetTimeMode
    {
        UTC,
        Local
    }

    [System.Serializable]
    public class TimeResetConfig:ICloneable
    {
        [SerializeField]
        private string _cronExpression;
        
        [SerializeField]
        private ResetTimeMode _resetTimeMode;

        public string CronExpression => _cronExpression;
        public ResetTimeMode ResetTimeMode => _resetTimeMode;

        public TimeResetConfig()
        {
            _cronExpression = string.Empty;
            _resetTimeMode = ResetTimeMode.UTC;
        }
        public TimeResetConfig(string cronExpression, ResetTimeMode resetTimeMode)
        {
            _cronExpression = cronExpression;
            _resetTimeMode = resetTimeMode;
        }
        public bool IsReset(long currentTimeStamp, long lastTimeUTCTimeStamp)
        {
            if (string.IsNullOrWhiteSpace(_cronExpression)) return false;
            if (lastTimeUTCTimeStamp <= 0) return false;
            return currentTimeStamp >= lastTimeUTCTimeStamp;
        }
        public long GetNextResetUtcTicks(long lastTimeUTCTimeStamp)
        {
            if (string.IsNullOrWhiteSpace(_cronExpression))
            {
                throw new InvalidOperationException("Cron expression is null or empty.");
            }

            var cronExpression = ParseCronExpression();

            DateTime fromUtc = DateTimeOffset.FromUnixTimeSeconds(lastTimeUTCTimeStamp).UtcDateTime;
            TimeZoneInfo timezone = _resetTimeMode == ResetTimeMode.Local ? TimeZoneInfo.Local : TimeZoneInfo.Utc;

            DateTime? nextOccurrenceUtc = cronExpression.GetNextOccurrence(fromUtc, timezone, false);
            if (nextOccurrenceUtc == null)
            {
                throw new InvalidOperationException($"Unable to calculate next reset time from cron expression '{_cronExpression}'.");
            }

            return new DateTimeOffset(DateTime.SpecifyKind(nextOccurrenceUtc.Value, DateTimeKind.Utc)).ToUnixTimeSeconds();
        }

        private global::Cronos.CronExpression ParseCronExpression()
        {
            try
            {
                return global::Cronos.CronExpression.Parse(_cronExpression, CronFormat.IncludeSeconds);
            }
            catch
            {
                return global::Cronos.CronExpression.Parse(_cronExpression, CronFormat.Standard);
            }
        }

        public object Clone()
        {
            return new TimeResetConfig(_cronExpression, _resetTimeMode);
        }
    }
}