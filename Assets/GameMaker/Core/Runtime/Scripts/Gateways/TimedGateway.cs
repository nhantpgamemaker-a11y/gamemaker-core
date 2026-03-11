namespace GameMaker.Core.Runtime
{
    public class TimedGateway
    {
        private static PlayerTimedRuntimeDataManager _manager;
        internal static PlayerTimedRuntimeDataManager Manager => _manager;
        internal static void Initialize(PlayerTimedRuntimeDataManager manager)
        {
            _manager = manager;
        }
    }
}