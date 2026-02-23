using System;
using Cysharp.Threading.Tasks;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public abstract  class BaseTimeStrategy
    {
        public abstract UniTask<bool> InitAsync();
        public abstract DateTime GetCurrentTime();
        public abstract  bool IsTimeValid();
        public abstract string GetTimeSource();
    }
}