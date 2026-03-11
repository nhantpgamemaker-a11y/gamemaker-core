using System;
using GameMaker.Core.Runtime;
using JetBrains.Annotations;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class PlayerTimed : BasePlayerData
    {
        [UnityEngine.SerializeField]
        private long _remain;
        [UnityEngine.SerializeField]
        private long _startTime;
        public long Remain { get => _remain; }
        public long StartTime { get => _startTime; }

        public long GetEndTime()
        {
            return _startTime + _remain;
        }
        public PlayerTimed(string id, IDefinition definition, long remain, long startTime) : base(id, definition)
        {
            _remain = remain;
            _startTime = startTime;
        }

        public override object Clone()
        {
            return new PlayerTimed(GetID(), GetDefinition(), _remain, _startTime);
        }

        public void From(PlayerTimed playerTimed)
        {
            _remain = playerTimed.Remain;
            _startTime = playerTimed.StartTime;
            NotifyObserver(this);
        }
    }
}