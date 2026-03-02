using System;
using GameMaker.Core.Runtime;
using TMPro;
using UnityEngine;

namespace GameMaker.Extension.Runtime
{
    public class UICountDown : MonoBehaviour
    {
        [SerializeField] private TMP_Text _txtTime;
        [SerializeField] private GameObject _countDownObject;
        [SerializeField] private GameObject _notCountDownObject;
        private long _endUTCTime;
        private long _compareUTCTime = long.MaxValue;
        public void Init(long endUTCTime)
        {
            _endUTCTime = endUTCTime;
        }
        private void OnEnable()
        {
            TimeManager.Instance.OnTimeTickEventAction += OnTimeTick;
            OnTimeTick(TimeManager.Instance.UnixTimestamp);
        }
        public void OnDisable()
        {
            TimeManager.Instance.OnTimeTickEventAction -= OnTimeTick;
        }

        private void OnTimeTick(long currentUTCTick)
        {
            if (_compareUTCTime != currentUTCTick)
            {
                _countDownObject?.gameObject.SetActive(_endUTCTime >= currentUTCTick);
                _notCountDownObject?.gameObject.SetActive(_endUTCTime < currentUTCTick);
                _compareUTCTime = currentUTCTick;
            }
            if (_endUTCTime < currentUTCTick)
            {
                return;
            }
            var timeSpan = TimeSpan.FromSeconds(_endUTCTime - currentUTCTick);
            _txtTime.text = FormatSmart(timeSpan);
        }
        string FormatSmart(TimeSpan ts)
        {
            if (ts.TotalDays >= 1)
            {
                return $"{(int)ts.TotalDays:D2}:{ts.Hours:D2}";
            }

            if (ts.TotalHours >= 1)
            {
                return $"{(int)ts.TotalHours:D2}:{ts.Minutes:D2}";
            }

            if (ts.TotalMinutes >= 1)
            {
                return $"{(int)ts.TotalMinutes:D2}:{ts.Seconds:D2}";
            }

            return $"00:{ts.Seconds:D2}";
        }
    }
}
