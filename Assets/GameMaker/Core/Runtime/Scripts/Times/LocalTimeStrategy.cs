using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class LocalTimeStrategy:BaseTimeStrategy
    {
        [UnityEngine.SerializeField]
        private float _sessionStartTime;

        public override async UniTask<bool> InitAsync()
        {
            _sessionStartTime = Time.realtimeSinceStartup;
            return true;
        }

        public override DateTime GetCurrentUTCTime()
        {
            DateTime systemTime = DateTime.UtcNow;
            return systemTime;
        }

        public override bool IsTimeValid()
        {
            return true;
        }

        public override string GetTimeSource()
        {
            return "Local System Time (UTC)";
        }
    }
}