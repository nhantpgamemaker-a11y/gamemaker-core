using System.Diagnostics;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    public static class Logger
    {
        [Conditional("ENABLE_DEBUG_LOG")]
        public static void Log(object data)
        {
            UnityEngine.Debug.Log(data);
        }
        [Conditional("ENABLE_DEBUG_LOG")]
        public static void Log(Color color, object data)
        {
            UnityEngine.Debug.Log($"<color={color}>{data}</color>");
        }
        [Conditional("ENABLE_DEBUG_LOG")]
        public static void LogWarning(object data)
        {
            UnityEngine.Debug.LogWarning(data);
        }
        [Conditional("ENABLE_DEBUG_LOG")]
        public static void LogError(object data)
        {
            UnityEngine.Debug.LogError(data);
        }
    }
}