namespace GameMaker.Core.Runtime
{
    public enum TimeMode
    {
        Local,      // Offline games
        Server,     // Purely online games
        Hybrid      // Online with offline support
    }
    [System.Serializable]
    public class TimePolicyConfig
    {
        [UnityEngine.SerializeField] private TimeMode _timeMode;
        [UnityEngine.SerializeField] private int  _autoSyncInterval;
        public TimeMode TimeMode => _timeMode;
        public int AutoSyncInterval =>_autoSyncInterval;
    }
}